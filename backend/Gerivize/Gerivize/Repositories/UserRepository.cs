using Gerivize.Interfaces;
using Gerivize.Models;

namespace Gerivize.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly GearivizeLocalContext _localContext;

        public UserRepository()
        {
            _localContext = new GearivizeLocalContext();
        }

        public List<User> getAll()
        {
            return _localContext.Users.ToList();
        }

        public User? getById(Guid id)
        {
            return _localContext.Users.ToList().Find(x => x.Id == id);
        }

        public User? getByEmailAndPassword(string email, string password)
        {
            return _localContext.Users.ToList().Find( u => u.Email == email && u.Password == password);
        }

        public User createUser(User user)
        {
            user.Id = Guid.NewGuid();
            _localContext.Users.Add(user);
            _localContext.SaveChanges();
            return user;
        }

        public User updateUser(User user)
        {
            _localContext.Update(user);
            _localContext.SaveChanges();
            return user;
        }

        public User deleteUser(Guid id)
        {
            User user = getById(id);
            _localContext.Remove(id);
            _localContext.SaveChanges();
            return user;
        }
    }
}
