using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PhoneBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBook.Controllers
{
    public class PhoneController : Controller
    {
        private readonly IMapper _mapper;

        public PhoneController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get(int id)
        {
            using (var context = new PhoneBookContext())
            {
                var phone = context.Phones.Include(p => p.Person).FirstOrDefault(p => p.PhoneId == id);

                if (phone != null)
                {
                    var dtoPhone = _mapper.Map<DTO.Phone>(phone);
                    var jsonResult = JsonConvert.SerializeObject(dtoPhone);

                    return Json(new { result = jsonResult });
                }                
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Phones(DTO.Person person)
        {
            using (var context = new PhoneBookContext())
            {
                var phones = await context.Phones.Include(p => p.Person)
                    .Where(p => p.Person.PersonId == person.PersonId).ToListAsync();

                var phonesDto = new List<DTO.Phone>();

                foreach (var phone in phones)
                {
                    var phoneDto = _mapper.Map<DTO.Phone>(phone);
                    phonesDto.Add(phoneDto);
                }

                return View("_Phones", phonesDto);
            }            
        }

        [HttpPost]
        public IActionResult Add(DTO.Phone phone)
        {
            using (var context = new PhoneBookContext())
            {
                var modelPhone = _mapper.Map<Phone>(phone);

                if (modelPhone != null)
                {
                    var newPhone = context.Add(modelPhone);
                    context.SaveChanges();

                    return Json(new { success = true, personId = newPhone.Entity.PersonId });
                }
            }

            return NotFound();
        }

        [HttpPost]
        public IActionResult Update(DTO.Phone phone)
        {
            using (var context = new PhoneBookContext())
            {
                var modelPhone = _mapper.Map<Phone>(phone);

                if (modelPhone != null)
                {
                    context.Update(modelPhone);
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
                var phone = context.Phones.FirstOrDefault(p => p.PhoneId == id);

                if (phone != null)
                {
                    context.Remove(phone);
                    context.SaveChanges();

                    return Ok();
                }
            }

            return NotFound();
        }
    }
}
