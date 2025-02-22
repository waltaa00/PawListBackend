using Microsoft.AspNetCore.Mvc;
using PawListBackend.Services;
using PawListBackend.Models;

namespace PawListBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DogsController : ControllerBase
    {
        private readonly DogService _service;

        public DogsController(DogService service)
        {
            _service = service;
        }

        // GET: api/dogs
        [HttpGet]
        public async Task<ActionResult<List<Dog>>> GetDogs() 
            => Ok(await _service.GetAllDogsAsync());

        // GET: api/dogs/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Dog>> GetDog(int id)
        {
            var dog = await _service.GetDogByIdAsync(id);
            return dog == null ? NotFound() : Ok(dog);
        }

        // POST: api/dogs
        [HttpPost]
        public async Task<ActionResult> AddDog([FromBody] Dog dog)
        {
            await _service.AddDogAsync(dog);
            return CreatedAtAction(nameof(GetDog), new { id = dog.Id }, dog);
        }

        // PUT: api/dogs/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateDog(int id, [FromBody] Dog dog)
        {
            var existingDog = await _service.GetDogByIdAsync(id);
            if (existingDog == null)
                return NotFound();

            existingDog.Breed = dog.Breed;
            existingDog.ImageUrl = dog.ImageUrl;
            existingDog.BredFor = dog.BredFor;
            existingDog.ReferenceImageId = dog.ReferenceImageId;
            existingDog.Temperament = dog.Temperament;
            existingDog.Lifespan = dog.Lifespan;
            existingDog.CountryOfOrigin = dog.CountryOfOrigin;

            await _service.UpdateDogAsync(existingDog);
            return NoContent();
        }

        // DELETE: api/dogs/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteDog(int id)
        {
            var dog = await _service.GetDogByIdAsync(id);
            if (dog == null)
                return NotFound();

            await _service.DeleteDogAsync(id);
            return NoContent();
        }
    }
}
