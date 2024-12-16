using FootKart.Interfaces;
using FootKart.Models;

namespace FootKart.Services
{
    public class UserProvider : IUserProvider
    {
        private User _user { get; set; }
        private static readonly object _lockObject = new object();
        private List<User> _registerUsers = new List<User>();

        private static IUserProvider _instance;
        public void Login(string userId)
        {
            lock (_lockObject)
            {
                _user = _registerUsers.FirstOrDefault(x => x.PhoneNumber == userId);
                if (_user == null)
                {
                    throw new Exception("No registered user with this userId");
                }
            }
        }

        public void RegisterUser(User user)
        {
            if (_registerUsers.Exists(x => x.PhoneNumber == user.PhoneNumber))
                throw new Exception("Already user exist with this phone number");
            _registerUsers.Add(user);
        }

        public bool IsExist(string userId)
        {
            return _registerUsers.Exists(x => x.PhoneNumber == userId);
        }
        public void Logout()
        {
            lock (_lockObject)
            {
                _user = null;
            }
        }

        public User GetCurrentUser()
        {
            lock (_lockObject)
            {
                AssertLoggedIn();
                return _user;
            }
        }

        public string GetUserPincode()
        {
            lock (_lockObject)
            {
                AssertLoggedIn();
                return _user.Pincode;
            }
        }

        private void AssertLoggedIn()
        {
            if (_user == null) throw new Exception("No logged in user");
        }

        public static IUserProvider GetInstance()
        {
            lock (_lockObject)
            {
                if (_instance == null)
                {
                    lock (_lockObject)
                    {
                        _instance = new UserProvider();
                    }
                }
            }
            return _instance;
        }
    }
}
