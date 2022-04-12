using RecapNet.BLL.DTO;
using RecapNet.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecapNet.BLL.Mappers
{
   internal static class PizzaMapper
   {
      public static PizzaDTO FromEntityToDTO(this PizzaEntity entity)
      {
         return new PizzaDTO
         {
            Id = entity.Id,
            Name = entity.Name,
            Price = entity.Price,
            FileName = entity.FileName
         };
      }

      public static PizzaEntity FromDtoToEntity(this PizzaDTO dto)
      {
         return new PizzaEntity
         {
            Id = dto.Id,
            Name = dto.Name,
            Price = dto.Price,
            FileName = dto.FileName
         };
      }
   }
}
