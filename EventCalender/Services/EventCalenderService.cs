using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventCalender.Interfaces;
using EventCalender.Models;

namespace EventCalender.Services
{
    public class EventCalenderService :IEventCalenderService
    {
        private IDataProvider<User> _userProviderService;
        private IDataProvider<Team> _teamProviderService;
        private IDataProvider<Event> _eventProviderService;

        public EventCalenderService()
        {
            _userProviderService = UserService.GetProviderInstance();
            _eventProviderService = EventService.GetProviderInstance();
            _teamProviderService = TeamService.GetProviderInstance();
        }

        public void CreateUser(string name, DateTime startTime, DateTime endTime)
        {
            var user = new User(name, startTime, endTime);
            _userProviderService.SaveItem(user);
        }

        public void CreateTeam(string name, List<string> userIds)
        {
            var team = new Team(name, userIds);
            _teamProviderService.SaveItem(team);
        }

        public void CreateEvent(string name, DateTime startTime, DateTime endTime, List<string> userIds,
            List<string> teamIds)
        {
            var evnt = new Event(name, startTime, endTime,userIds, teamIds);
            _eventProviderService.SaveItem(evnt);
        }
    }
}
