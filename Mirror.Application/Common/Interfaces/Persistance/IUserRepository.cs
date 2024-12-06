using Mirror.Domain.Entities;

namespace Mirror.Application.Common.Interfaces.Persistance
{
    public interface IUserRepository
    {
        User? GetUserByEmail(string email);

        void Add(User user);
    }
}
