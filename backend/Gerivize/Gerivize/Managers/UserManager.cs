using Gerivize.Interfaces;

namespace Gerivize.Managers
{
    public class UserManager : IUserManager
    {
        private readonly IUserRepository _repository;

        public UserManager(IUserRepository repository)
        {
            _repository = repository;
        }
    }
}
