using Microsoft.Extensions.Logging;
using Mirror.Application.Common.Interfaces.Persistance;
using Mirror.Application.DatabaseContext;

namespace Mirror.Infrastructure.Persistance.Repository.User
{
    public class UserRepository : IUserRepository
    {
        private readonly MirrorContext _context;
        private readonly ILogger<UserRepository> _logger;

        public UserRepository(MirrorContext context, ILogger<UserRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public void Add(Domain.Entities.User user)
        {
            _logger.LogInformation("Adding user with Email: {Email}", user.Email);

            _context.Users.Add(user);
            _context.SaveChanges();

            _logger.LogInformation("User with Email: {Email} successfully added.", user.Email);
        }

        public Domain.Entities.User? GetUserByEmail(string email)
        {
            _logger.LogInformation("Fetching user by Email: {Email}", email);

            var user = _context.Users.SingleOrDefault(u => u.Email == email);

            if (user is null)
            {
                _logger.LogWarning("No user found with Email: {Email}.", email);
            }
            else
            {
                _logger.LogInformation("User with Email: {Email} successfully retrieved.", email);
            }

            return user;
        }
    }
}
