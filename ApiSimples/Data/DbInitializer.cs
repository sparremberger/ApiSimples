using ApiSimples.Models; //
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiSimples.Data
{
    public class DbInitializer
    {
        public static void Initialize(UserContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Users.Any())
            {
                return;   // DB has been seeded
            }

            var users = new User[]
            {
            new User{Email="marcos.soares@pop.com.br",Card=CreditCardGenerator.GenerateMasterCardNumber().ToString()},
            new User{Email="lucas.souto@ig.com.br",Card=CreditCardGenerator.GenerateMasterCardNumber().ToString()},
            new User{Email="fernando.sampaio@ibest.com.br",Card=CreditCardGenerator.GenerateMasterCardNumber().ToString()},
            new User{Email="tony.santana@bol.com.br",Card=CreditCardGenerator.GenerateMasterCardNumber().ToString()},
            };
            foreach (User u in users)
            {
                context.Users.Add(u);
            }
            context.SaveChanges();
        }
    }
}
