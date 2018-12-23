using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PhoneBook.Models
{
    public class Person
    {
        public Person()
        {
            Phones = new List<Phone>();
        }

        public int PersonId { get; set; }

        [MaxLength(20)]
        public string Name { get; set; }

        public ICollection<Phone> Phones { get; set; }
    }
}
