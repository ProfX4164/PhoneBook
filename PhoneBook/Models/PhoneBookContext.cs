using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBook.Models
{
    public class PhoneBookContext : DbContext
    {
        public DbSet<Person> Persons { get; set; }

        public DbSet<Phone> Phones { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var stringConnection =
                @"Server=tcp:nifon.database.windows.net,1433;Initial Catalog=PhoneBookDataBase;Persist Security Info=False;User ID=vnif;Password=V.Nif1234;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

            optionsBuilder
                .UseSqlServer(stringConnection);
        }
    }
}
