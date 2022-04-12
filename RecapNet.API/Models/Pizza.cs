using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RecapNet.API.Models
{
   public class Pizza
   {
      // Model utilisé pour envoyer les données au client (GET)

      public int Id { get; set; }
      public string Name { get; set; }
      public decimal Price { get; set; }
      public string FileName { get; set; }
   }

   public class PizzaData
   {
      // Model utilisé pour obtenir des données (Requete POST et PUT)

      [Required]
      public string Name { get; set; }

      [Required]
      public decimal Price { get; set; }
   }
}
