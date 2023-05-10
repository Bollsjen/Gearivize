using Gerivize.Models;

namespace Gerivize.Repositories
{
    public class UserRepository
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

        public User updateUser(User user, Guid id)
        {
            User dims = getById(id);
            dims.Email = user.Email;
            dims.Name = user.Name;
            dims.Responsible = user.Responsible;
            dims.SuperUser = user.SuperUser;
            _localContext.SaveChanges();
            return user;
        }

        public User deleteUser(Guid id)
        {
            User user = getById(id);
            _localContext.Remove(user);
            _localContext.SaveChanges();
            return user;
        }
    }
}
