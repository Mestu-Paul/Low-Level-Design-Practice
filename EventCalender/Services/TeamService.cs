using EventCalender.Interfaces;
using EventCalender.Models;

namespace EventCalender.Services
{
    public class TeamService : ADataProvideService<Team>

    {
        private static IDataProvider<Team> _instance;
        private static readonly object _locker = new object();
        public static IDataProvider<Team> GetProviderInstance()
        {
            if (_instance == null)
            {
                lock (_locker)
                {
                    if (_instance == null)
                    {
                        _instance = new TeamService();
                    }
                }
            }

            return _instance;
        }
    }
}
