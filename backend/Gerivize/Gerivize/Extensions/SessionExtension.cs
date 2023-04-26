using Gerivize.Models;
using Newtonsoft.Json;

namespace Gerivize.Extensions
{
    public static class SessionExtension
    {
        private const string _usrKey = "User";
        public static void SetUser(this ISession session, User user)
        {
            session.SetString(_usrKey, JsonConvert.SerializeObject(user));
        }

        public static User GetUser(this ISession session)
        {
            var userString = session.GetString(_usrKey);
            return userString == null ? null : JsonConvert.DeserializeObject<User>(userString);
        }
    }
}
