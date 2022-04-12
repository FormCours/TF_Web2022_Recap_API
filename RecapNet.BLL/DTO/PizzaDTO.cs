using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecapNet.BLL.DTO
{
   public class PizzaDTO
   {
      public int Id { get; set; }
      public string Name { get; set; }
      public decimal Price { get; set; }
      public string FileName { get; set; }
   }
}
