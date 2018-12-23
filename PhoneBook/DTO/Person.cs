using System.Collections.Generic;

namespace PhoneBook.DTO
{
    public class Person
    {
        public int PersonId { get; set; }

        public string Name { get; set; }

        public List<Phone> Phones { get; set; }
    }
}
