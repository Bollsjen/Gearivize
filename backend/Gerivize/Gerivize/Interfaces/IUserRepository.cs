using Gerivize.Models;

namespace Gerivize.Interfaces
{
    public interface IUserRepository
    {
        public List<User> getAll();
        public User getById(Guid id);
        public User getByEmailAndPassword(string email, string password);
        public User createUser(User user);
        public User updateUser(User user);
        public User deleteUser(Guid id);
    }
}
