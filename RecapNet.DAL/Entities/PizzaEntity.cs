using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecapNet.DAL.Entities
{
   public class PizzaEntity
   {
      public int Id { get; set; }
      public string Name { get; set; }
      public decimal Price { get; set; }
      public string FileName { get; set; }
   }
}
