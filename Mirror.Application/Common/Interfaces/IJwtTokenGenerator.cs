using Mirror.Domain.Entities;

namespace Mirror.Application.Common.Interfaces
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(User user);
    }
}
