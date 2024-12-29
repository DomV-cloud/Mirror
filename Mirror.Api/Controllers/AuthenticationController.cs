using Microsoft.AspNetCore.Mvc;
using Mirror.Api.Filters;
using Mirror.Application.Services.Authentication;
using Mirror.Contracts.Request.Authentication.GET;
using Mirror.Contracts.Request.Authentication.POST;
using Mirror.Contracts.Response.Authentication;

namespace Mirror.Api.Controllers
{
    [ApiController]
    [Route("auth")]
    [ErrorHandlingFilter]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly ILogger<AuthenticationController> _logger;

        public AuthenticationController(
            IAuthenticationService authenticationService,
            ILogger<AuthenticationController> logger)
        {
            _authenticationService = authenticationService;
            _logger = logger;
        }

        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="request">Registration details.</param>
        /// <returns>Registered user details with token.</returns>
        [HttpPost("register", Name = "register")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(AuthenticationResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Register([FromBody] RegisterRequest request)
        {
            _logger.LogInformation("Starting Register endpoint.");

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for Register endpoint.");
                return BadRequest(ModelState);
            }

            _logger.LogInformation("Processing registration for user {Email}.", request.Email);

            var authResult = _authenticationService.Register(
                request.FirstName,
                request.LastName,
                request.Email,
                request.Password
            );

            var response = new AuthenticationResponse(
                authResult.User.FirstName,
                authResult.User.LastName,
                authResult.User.Email,
                authResult.Token
            );

            _logger.LogInformation("User {Email} registered successfully.", request.Email);

            return CreatedAtRoute("register", response);
        }

        /// <summary>
        /// Authenticates a user with login credentials.
        /// </summary>
        /// <param name="request">Login details.</param>
        /// <returns>User details with token.</returns>
        [HttpPost("login", Name = "login")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AuthenticationResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            _logger.LogInformation("Starting Login endpoint.");

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for Login endpoint.");
                return BadRequest(ModelState);
            }

            _logger.LogInformation("Processing login for user {Email}.", request.Email);

            var authResult = _authenticationService.Login(
                request.Email,
                request.Password
            );

            if (authResult == null)
            {
                _logger.LogWarning("Login failed for user {Email}.", request.Email);
                return Unauthorized("Invalid credentials.");
            }

            var response = new AuthenticationResponse(
                authResult.User.FirstName,
                authResult.User.LastName,
                authResult.User.Email,
                authResult.Token
            );

            _logger.LogInformation("User {Email} logged in successfully.", request.Email);

            return Ok(response);
        }
    }
}