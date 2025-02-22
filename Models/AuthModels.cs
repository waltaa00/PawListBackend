namespace PawListBackend.Models
{
    // Request model for user registration.
    public class RegisterRequest
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    // Request model for user login.
    public class LoginRequest
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    // Response model for authentication operations.
    public class AuthResult
    {
        // Indicates if the operation was successful.
        public bool Success { get; set; }
        // A message explaining the result.
        public string Message { get; set; } = string.Empty;
        // The JWT token (if login is successful).
        public string? Token { get; set; }
    }
}