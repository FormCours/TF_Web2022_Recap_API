using RecapNet.BLL.DTO;
using RecapNet.BLL.Mappers;
using RecapNet.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecapNet.BLL.Services
{
   public class PizzaService
   {
      private PizzaRepository _PizzaRepository;
      private const decimal priceTuesday = 5.99m;

      public PizzaService(PizzaRepository pizzaRepository)
      {
         _PizzaRepository = pizzaRepository;
      }


      public IEnumerable<PizzaDTO> GetAll()
      {
         IEnumerable<PizzaDTO> pizzas = _PizzaRepository.GetAll().Select(p => p.FromEntityToDTO());

         if (DateTime.Today.DayOfWeek == DayOfWeek.Tuesday)
         {
            return pizzas.Select(p =>
            {
               p.Price = priceTuesday;
               return p;
            });
         }
         return pizzas;
      }

      public PizzaDTO GetOne(int id)
      {
         PizzaDTO pizza = _PizzaRepository.GetOne(id)?.FromEntityToDTO();

         if(DateTime.Today.DayOfWeek == DayOfWeek.Tuesday)
         {
            pizza.Price = priceTuesday;
         }

         return pizza;
      } 

      public int Create(PizzaDTO pizza)
      {
         return _PizzaRepository.Create(pizza.FromDtoToEntity());
      }


      public bool AddImage(int pizzaId, string fileName)
      {
         return _PizzaRepository.AddFileNameImage(pizzaId, fileName);
      }
   }
}
