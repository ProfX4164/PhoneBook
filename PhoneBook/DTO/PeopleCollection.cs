using System.Collections.Generic;

namespace PhoneBook.DTO
{
    public class PeopleCollection
    {
        public ICollection<Person> People { get; set; }

        public int SelectedPersonId { get; set; }
    }
}
