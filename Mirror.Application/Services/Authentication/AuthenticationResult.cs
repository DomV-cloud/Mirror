using Mirror.Domain.Entities;

namespace Mirror.Application.Services.Authentication
{
    public record AuthenticationResult
    (
        User User,
        string Token
    );
}
