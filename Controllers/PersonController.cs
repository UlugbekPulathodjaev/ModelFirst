using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModelFirst.DataAccess;
using ModelFirst.Models;
using ModelFirst.Models.Dtos;

namespace ModelFirst.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private AppDbContext _context;

        public PersonController(AppDbContext applicationDbContext)
        {
            _context = applicationDbContext;
        }


        [HttpPost]
        public async ValueTask<IActionResult> CreatePersonAsync()
        {

            Person person = new Person();

            var personCar = await _context.Persons.FirstOrDefaultAsync(x => x.Name == "Ali");
            var myCar = await _context.Cars.FirstOrDefaultAsync(x => x.Id == 2);

            await _context.PersonCars.AddAsync(new PersonCars()
            {
                PersonId = personCar.Id,
                CarId = myCar.Id
            });

            await _context.SaveChangesAsync();


            return Ok("Let Check");
        }
        [HttpPost]
        public async ValueTask<IActionResult> PostPersonAsync([FromBody] PersonDto personDto)
        {
            if (personDto == null)
            {
                return BadRequest("Invalid input");
            }

            var newPerson = new Person
            {
                Name = personDto.Name,
            };

            _context.Persons.Add(newPerson);

            foreach (var carId in personDto.CarIds)
            {
                var car = await _context.Cars.FindAsync(carId);
                if (car != null)
                {
                    newPerson.PersonCarss.Add(new PersonCars
                    {
                        Car = car
                    });
                }
            }

            await _context.SaveChangesAsync();

            var result = await _context.Persons
                .Where(p => p.Id == newPerson.Id)
                .Include(x => x.PersonCarss)
                .ThenInclude(x => x.Car)
                .ToListAsync();

            return Ok(result);
        }

        [HttpGet]
        public async ValueTask<IActionResult> GetPersonAsync()
        {
            var result = await _context.Persons
                .Include(x => x.PersonCarss)
                .ThenInclude(x => x.Car)
                .ToListAsync();

            return Ok(result);
        }
    }
}
