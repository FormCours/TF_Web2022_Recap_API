using RecapNet.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecapNet.API.Models.Mappers
{
   public static class PizzaMapper
   {

      public static Pizza FromDtoToModel(this PizzaDTO dto) 
      {
         return new Pizza
         {
            Id = dto.Id,
            Name = dto.Name,
            Price = dto.Price,
            FileName = dto.FileName
         };
      }

      public static PizzaDTO FromModelToDTO(this PizzaData data)
      {
         return new PizzaDTO
         {
            Id = -1,
            Name = data.Name,
            Price = data.Price,
            FileName = null
         };
      }
   }
}
