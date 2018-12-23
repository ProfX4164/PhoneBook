using System.ComponentModel.DataAnnotations;

namespace PhoneBook.Models
{
    public class Phone
    {
        public int PhoneId { get; set; }

        [MaxLength(20)]
        public string Number { get; set; }

        public int PersonId { get; set; }

        public Person Person { get; set; }
    }
}
