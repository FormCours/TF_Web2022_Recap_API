using RecapNet.DAL.Entities;
using RecapNet.DAL.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecapNet.DAL.Repositories
{
   public class PizzaRepository
   {
      private string _ConnectionString;

      public PizzaRepository(ConnectionString connectionString)
      {
         _ConnectionString = connectionString.Value;
      }

      // Utils
      private SqlConnection CreateConnection()
      {
         return new SqlConnection(_ConnectionString);
      }

      // Mappers
      private PizzaEntity ConvertToEntity(IDataRecord record)
      {
         return new PizzaEntity
         {
            Id = (int)record["Id"],   // Id = record.GetInt32(record.GetOrdinal("Id"),
            Name = (string)record["Name"],
            Price = (decimal)record["Price"],
            FileName = (record["FileName"] is DBNull) ? null : (string)record["FileName"]
         };
      }

      // CRUD
      public IEnumerable<PizzaEntity> GetAll()
      {
         using (SqlConnection connection = CreateConnection())
         {
            connection.Open();

            using (SqlCommand command = connection.CreateCommand())
            {
               command.CommandText = "SELECT * FROM Pizza";
               command.CommandType = CommandType.Text;

               using (SqlDataReader reader = command.ExecuteReader())
               {
                  while (reader.Read())
                  {
                     PizzaEntity pizza = ConvertToEntity(reader);
                     yield return pizza;
                  }
               }
            }
         }
      }

      public PizzaEntity GetOne(int id)
      {
         using (SqlConnection connection = CreateConnection())
         {
            connection.Open();

            using (SqlCommand command = connection.CreateCommand())
            {
               command.CommandText = "SELECT * FROM Pizza WHERE Id = @Id";
               command.Parameters.AddWithValue("Id", id);

               using (SqlDataReader reader = command.ExecuteReader())
               {
                  if (reader.Read())
                  {
                     return ConvertToEntity(reader);
                  }
                  return null;
               }
            }
         }
      }

      public int Create(PizzaEntity pizza)
      {
         using(SqlConnection connection = CreateConnection())
         {
            connection.Open();

            using(SqlCommand command = connection.CreateCommand())
            {
               command.CommandText = "INSERT INTO Pizza(Name, Price)" +
                                     " OUTPUT inserted.Id" +
                                     " VALUES (@Name, @Price)";

               command.Parameters.AddWithValue("Name", pizza.Name);
               command.Parameters.AddWithValue("Price", pizza.Price);

               int pizzaId = (int)command.ExecuteScalar();
               return pizzaId;
            }
         }
      }

      public bool AddFileNameImage(int pizzaId, string fileName)
      {
         using (SqlConnection connection = CreateConnection())
         {
            connection.Open();

            using (SqlCommand command = connection.CreateCommand())
            {
               command.CommandText = "UPDATE Pizza" +
                                     " SET FileName = @FileName" +
                                     " WHERE Id = @Id";

               command.Parameters.AddWithValue("Id", pizzaId);
               command.Parameters.AddWithValue("FileName", fileName);

               return command.ExecuteNonQuery() == 1;
            }
         }
      }
   }
}
