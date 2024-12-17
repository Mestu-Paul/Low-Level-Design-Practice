using EventCalender.Interfaces;
using EventCalender.Services;

namespace EventCalender.Models
{
    public class Event : CommonProperty
    {
        private IDataProvider<Team> _teamdDataProvider;

        private IDataProvider<User> _userdDataProvider;
        public Event(string name, DateTime startTime, DateTime endTime) : base(name)
        {
            Members = new List<Tuple<string, string>>();
            SetEventTime(startTime,endTime);
            _teamdDataProvider = TeamService.GetProviderInstance();
            _userdDataProvider = UserService.GetProviderInstance();
        }

        public Event(string name, DateTime startTime, DateTime endTime, List<string> userIds, List<string> teamIds) :
            this(name, startTime, endTime)
        {
            foreach (var teamId in teamIds)
            {
                var team = _teamdDataProvider.GetItemById(teamId);
                if (!AddMember<Team>(team))
                {
                    throw new Exception($"Can not create event with team {team.Id}");
                }
            }

            foreach (var userId in userIds)
            {
                var user = _userdDataProvider.GetItemById(userId);
                if (!AddMember<User>(user))
                {
                    throw new Exception($"Can not create event with user {user.Id}");
                }
            }
        }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public List<Tuple<string,string>> Members { get; set; }

        public void SetEventTime(DateTime starTime, DateTime endTime)
        {
            if(endTime<=starTime) throw new Exception("Invalid event hour duration");
            if(starTime<DateTime.UtcNow) throw new Exception("Invalid event start time");

            StartTime = starTime;
            EndTime = endTime;
        }

        public bool AddMember<T>(T member) where T : CommonProperty,IEventAssign
        {
            if (member.AssignToEvent(this))
            {
                var memberType = typeof(T).Name;
                Members.Add(new Tuple<string, string>(member.Id,memberType));
                return true;
            }

            return false;
        }

    }
}
