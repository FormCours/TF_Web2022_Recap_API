using RecapNet.DAL.Entities;
using RecapNet.DAL.Repositories;
using RecapNet.DAL.Utils;
using System;
using System.Collections.Generic;

namespace RecapNet.ConsoleTest
{
   class Program
   {
      static void Main(string[] args)
      {
         ConnectionString connectionString = new ConnectionString("Data Source=TFNSDOTDE0500A;Integrated Security=True;Database=RecapNet;");
            
         PizzaRepository pizzaRepository = new PizzaRepository(connectionString);

         /*** GET ALL *********************************************************************/
         IEnumerable<PizzaEntity> pizzas = pizzaRepository.GetAll();
         foreach(PizzaEntity pizza in pizzas)
         {
            Console.WriteLine(pizza.Id + " / " + pizza.Name + " / " + pizza.Price);
         }
         Console.WriteLine();

         /*** GET ONE *********************************************************************/
         int idTarget = 2;
         PizzaEntity pizza1 = pizzaRepository.GetOne(idTarget);
         if(pizza1 != null)
         {
            Console.WriteLine(pizza1.Id + " / " + pizza1.Name + " / " + pizza1.Price);
         }
         else
         {
            Console.WriteLine($"Pas de pizza à l'id {idTarget} :o");
         }
         Console.WriteLine();

         /*** Create ***********************************************************************/
         /*
         PizzaEntity pizza2 = new PizzaEntity()
         {
            Name = "Diavolo",
            Price = 13
         };
         int idPizza = pizzaRepository.Create(pizza2);
         Console.WriteLine($"Nouvelle pizza créer avec l'id {idPizza}");
         Console.WriteLine();
         */
      }
   }
}
