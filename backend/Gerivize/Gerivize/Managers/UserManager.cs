using Gerivize.Repositories;

namespace Gerivize.Managers
{
    public class UserManager
    {
        private readonly UserRepository _userRepository;

        public UserManager()
        {
            _userRepository = new UserRepository();
        }
    }
}
