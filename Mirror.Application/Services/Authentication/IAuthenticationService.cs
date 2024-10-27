using Mirror.Application.Services.Authentication;

namespace Mirror.Application.Services.Authentication

{
    public interface IAuthenticationService
    {
        AuthenticationResult Register(string firstName, string lastName, string email, string password);
        AuthenticationResult Login(string email, string password);
    }
}
