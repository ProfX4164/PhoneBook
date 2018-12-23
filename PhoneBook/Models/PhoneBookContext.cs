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
                @"Server=tcp:nifon.database.windows.net,1433;Initial Catalog=PhoneBookDataBase;Persist Security Info=False;User ID=login;Password=password;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

            optionsBuilder
                .UseSqlServer(stringConnection);
        }
    }
}
