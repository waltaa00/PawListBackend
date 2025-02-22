using PawListBackend.Data;
using PawListBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace PawListBackend.Repositories
{
    public class DogRepository
    {
        private readonly AppDbContext _context;

        public DogRepository(AppDbContext context)
        {
            _context = context;
        }

        // Get all dogs
        public async Task<List<Dog>> GetAllDogsAsync() =>
            await _context.Dogs.ToListAsync();

        // Get a dog by ID
        public async Task<Dog?> GetDogByIdAsync(int id) =>
            await _context.Dogs.FirstOrDefaultAsync(d => d.Id == id);

        // Add a new dog
        public async Task AddDogAsync(Dog dog)
        {
            try
            {
                await _context.Dogs.AddAsync(dog);  // Add dog to DbSet
                await _context.SaveChangesAsync(); // Save changes to the database
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding dog to DB: {ex.Message}");
            }
        }

        // Check if dog exists by ID
        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Dogs.AnyAsync(d => d.Id == id);
        }

        // Update an existing dog
        public async Task UpdateDogAsync(Dog dog)
        {
            _context.Dogs.Update(dog);
            await _context.SaveChangesAsync();
        }

        // Delete a dog by ID
        public async Task DeleteDogAsync(int id)
        {
            var dog = await GetDogByIdAsync(id);
            if (dog != null)
            {
                _context.Dogs.Remove(dog);
                await _context.SaveChangesAsync();
            }
        }
    }
}