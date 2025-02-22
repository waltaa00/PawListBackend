using System.ComponentModel.DataAnnotations;

namespace PawListBackend.Models
{
    // Represents a user in the application.
    public class User
    {
        // Auto-generated primary key.
        public int Id { get; set; }

        // The unique username for the user.
        [Required]
        public string Username { get; set; } = string.Empty;

        // The hashed password for the user.
        [Required]
        public string PasswordHash { get; set; } = string.Empty;
        
        // Additional properties can be added here (e.g., Email, Role, etc.)
    }
}