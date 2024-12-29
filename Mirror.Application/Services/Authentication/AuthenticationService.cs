using Mirror.Application.Common.Interfaces;
using Mirror.Application.Common.Interfaces.Persistance;
using Mirror.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Mirror.Application.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUserRepository _userRepository;
        private readonly ILogger<AuthenticationService> _logger;

        public AuthenticationService(
            IJwtTokenGenerator jwtTokenGenerator,
            IUserRepository userRepository,
            ILogger<AuthenticationService> logger)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userRepository;
            _logger = logger;
        }

        public AuthenticationResult Register(string firstName, string lastName, string email, string password)
        {
            _logger.LogInformation("Starting registration for email: {Email}", email);

            if (_userRepository.GetUserByEmail(email) is not null)
            {
                _logger.LogWarning("Registration failed. User with email {Email} already exists.", email);
                throw new Exception("User with given email already exists");
            }

            var user = new User
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Password = password
            };

            _userRepository.Add(user);
            _logger.LogInformation("User {Email} registered successfully.", email);

            var token = _jwtTokenGenerator.GenerateToken(user);
            _logger.LogInformation("JWT token generated for user {Email}.", email);

            return new(user, token);
        }

        public AuthenticationResult Login(string email, string password)
        {
            _logger.LogInformation("Starting login for email: {Email}", email);

            if (_userRepository.GetUserByEmail(email) is not User user)
            {
                _logger.LogWarning("Login failed. User with email {Email} does not exist.", email);
                throw new Exception("User with given email does not exist.");
            }

            if (user.Password != password)
            {
                _logger.LogWarning("Login failed. Invalid password for email {Email}.", email);
                throw new Exception("Invalid password");
            }

            var token = _jwtTokenGenerator.GenerateToken(user);
            _logger.LogInformation("User {Email} logged in successfully. JWT token generated.", email);

            return new(user, token);
        }
    }
}