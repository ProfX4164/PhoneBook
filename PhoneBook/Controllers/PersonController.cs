using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PhoneBook.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBook.Controllers
{
    public class PersonController : Controller
    {
        private readonly IMapper _mapper;

        public PersonController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get(int id)
        {
            using (var context = new PhoneBookContext())
            {
                var person = context.Persons.FirstOrDefault(p => p.PersonId == id);

                if (person != null)
                {
                    var modelPerson = _mapper.Map<DTO.Person>(person);
                    var jsonResult = JsonConvert.SerializeObject(modelPerson);

                    return Json(new { result = jsonResult });
                }                
            }

            return NotFound();
        }

        [HttpPost]
        public IActionResult Add(DTO.Person person)
        {
            using (var context = new PhoneBookContext())
            {
                var modelPerson = _mapper.Map<Person>(person);

                if (modelPerson != null)
                {
                    var newPerson = context.Add(modelPerson);
                    context.SaveChanges();

                    return Json(new { Success = true, newPerson.Entity.PersonId }) ;
                }
            }

            return NotFound();
        }

        [HttpPost]
        public IActionResult Update(DTO.Person person)
        {
            using (var context = new PhoneBookContext())
            {
                var modelPerson = _mapper.Map<Person>(person);

                if (modelPerson != null)
                {
                    context.Update(modelPerson);
                    context.SaveChanges();

                    return Ok();
                }
            }

            return NotFound();
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            using (var context = new PhoneBookContext())
            {
                var person = context.Persons.FirstOrDefault(p => p.PersonId == id);

                if (person != null)
                {
                    context.Remove(person);
                    context.SaveChanges();

                    return Ok();
                }
            }

            return NotFound();
        }

        public async Task<IActionResult> Search(string pattern)
        {
            var people = await SearchPeople(pattern);

            return View("_People", people);
        }

        public async Task<ICollection<DTO.Person>> SearchPeople(string pattern)
        {
            using (var context = new PhoneBookContext())
            {
                var people = context.Persons.Include(p => p.Phones).Where(p => p.PersonId > 0);
                
                if (!string.IsNullOrWhiteSpace(pattern))
                    people = people.Where(p => p.Name.IndexOf(pattern) == 0);

                var peopleDto = new List<DTO.Person>();

                foreach (var person in await people.ToListAsync())
                {
                    var personDto = _mapper.Map<DTO.Person>(person);
                    peopleDto.Add(personDto);
                }

                return peopleDto;
            }
        }
    }
}
