using EventCalender.Interfaces;
using EventCalender.Models;

namespace EventCalender.Services
{
    public class EventService:ADataProvideService<Event>
    {
        private static IDataProvider<Event> _instance;
        private static readonly object _locker = new object();
        public static IDataProvider<Event> GetProviderInstance()
        {
            if (_instance == null)
            {
                lock (_locker)
                {
                    if (_instance == null)
                    {
                        _instance = new EventService();
                    }
                }
            }

            return _instance;
        }
    }
}
