using BCryptNet = BCrypt.Net.BCrypt;

namespace LinkUpWorld.UsersMicroservice.Application.Helpers
{
    public class PasswordHasher
    {
        private const int WorkFactor = 12;

        public string GenerateSalt()
        {
            return BCryptNet.GenerateSalt(WorkFactor);
        }

        public string HashPassword(string password, string salt)
        {
            return BCryptNet.HashPassword(password, salt);
        }

        public bool VerifyPassword(string password, string hashedPassword)
        {
            return BCryptNet.Verify(password, hashedPassword);
        }
    }
}
