using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecapNet.API.Models;
using RecapNet.API.Models.Mappers;
using RecapNet.BLL.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RecapNet.API.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
   public class PizzaController : ControllerBase
   {
      PizzaService _PizzaService;

      public PizzaController(PizzaService pizzaService)
      {
         _PizzaService = pizzaService;
      }

      [HttpGet]
      [ProducesResponseType(200, Type = typeof(IEnumerable<Pizza>))]
      public IActionResult GetAll()
      {
         IEnumerable<Pizza> pizzas = _PizzaService.GetAll().Select(p => p.FromDtoToModel());
         return Ok(pizzas);
      }

      [Route("{id}")]
      [HttpGet]
      [ProducesResponseType(200, Type = typeof(Pizza))]
      [ProducesResponseType(404)]
      public IActionResult GetOne([FromRoute] int id)
      {
         Pizza pizza = _PizzaService.GetOne(id)?.FromDtoToModel();

         if (pizza is null)
         {
            return NotFound();
         }
         return Ok(pizza);
      }

      [HttpPost]
      [ProducesResponseType(200, Type = typeof(string))]
      [ProducesResponseType(422)]
      public IActionResult Add([FromBody] PizzaData data)
      {
         int pizzaId;
         try
         {
            pizzaId = _PizzaService.Create(data.FromModelToDTO());
         }
         catch (Exception)
         {
            return new StatusCodeResult(422);
         }

         return Ok($"La pizza a été ajouter dans la DB avec l'id {pizzaId}");
      }

      [Route("{id}/Image")]
      [HttpPost]
      [ProducesResponseType(204)]
      public IActionResult AddImage([FromRoute] int id, IFormFile file)
      {
         if (file.Length <= 0)
         {
            return BadRequest("Empty file :o");
         }

         // Génération d'un nom unique avec l'extention
         string extFileName = Path.GetExtension(file.FileName);
         string newFileName = DateTime.Now.ToString("yyyyMMddhhmmss") + "-" + Path.GetRandomFileName() + extFileName;

         // Définition du chemin pour créer le fichier
         string pathFileName = Path.Combine(".", "Images", newFileName);

         // Sauvegarder le fichier sur le disque
         using (FileStream stream = System.IO.File.Create(pathFileName))
         {
            file.CopyTo(stream);
         }

         // Sauvegarde en DB 
         _PizzaService.AddImage(id, newFileName);

         return NoContent();
      }
   }
}
