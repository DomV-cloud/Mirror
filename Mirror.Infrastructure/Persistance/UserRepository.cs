using Mirror.Application.Common.Interfaces.Persistance;
using Mirror.Domain.Entities;
using Mirror.Application.DatabaseContext;
namespace Mirror.Infrastructure.Persistance
{
    public class UserRepository : IUserRepository
    {
        private readonly MirrorContext _context;

        public UserRepository(MirrorContext context)
        {
            _context = context;
        }

        public void Add(User user)
        {
           using (var context = _context)
           {
                context.Users.Add(user);

                context.SaveChanges();
            }
        }

        public User? GetUserByEmail(string email)
        {
            return _context.Users.SingleOrDefault(u => u.Email == email);
        }
    }
}
