using EventCalender.Interfaces;
using EventCalender.Models;

namespace EventCalender.Services
{
    public class UserService : ADataProvideService<User>
    {
        private static IDataProvider<User> _instance;
        private static readonly object _locker = new object();
        public static IDataProvider<User> GetProviderInstance()
        {
            if (_instance == null)
            {
                lock (_locker)
                {
                    if (_instance == null)
                    {
                        _instance = new UserService();
                    }
                }
            }

            return _instance;
        }
    }
}
