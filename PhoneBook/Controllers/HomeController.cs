using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhoneBook.DTO;
using PhoneBook.Models;

namespace PhoneBook.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMapper _mapper;

        public HomeController(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var people = await GetPeopleAsync();

            return View(people);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private async Task<ICollection<DTO.Person>> GetPeopleAsync()
        {
            using (var context = new PhoneBookContext())
            {
                var people = await context.Persons.Include(p => p.Phones).ToListAsync();

                var peopleDto = new List<DTO.Person>();

                foreach (var person in people)
                {
                    var personDto = _mapper.Map<DTO.Person>(person);
                    peopleDto.Add(personDto);
                }

                return peopleDto;
            }
        }
    }
}
