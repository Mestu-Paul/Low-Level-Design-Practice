using Core.Utility;
using EventCalender.Interfaces;
using EventCalender.Services;

namespace EventCalender.Models
{
    public class Team : CommonProperty,IEventAssign
    {
        private IDataProvider<User> _userProvider { get; set; }
        public Team(string name) : base(name)
        {
            UserIds = new List<string>();
            AssignedEventIds = new List<string>();
            _userProvider = UserService.GetProviderInstance();
        }

        public Team(string name, List<string> userIds) : this(name)
        {
            foreach (var userId in userIds)
            {
                if (AddMember(userId))
                {
                    UserIds.Add(userId);
                }
            }
        }
        public List<string> UserIds { get; private set; }
        public List<string> AssignedEventIds { get; set; }

        public bool AddMember(string userId)
        {
            if (UserIds.Exists(x => x==userId))
            {
                Logger.Info($"Already exist in the team");
                return true;
            }
            var user = _userProvider.GetItemById(userId);
            if (user == null)
            {
                Logger.Warn($"No user found with id:{userId}");
                return false;
            }
            user.AssignToTeam(Id);
            UserIds.Add(user.Id);
            return true;
        }

        public void RemoveMember(string userId)
        {
            if (!UserIds.Exists(x => x == userId))
            {
                throw new Exception($"No user found to remove, userId:{userId}");
            }
            UserIds.Remove(userId);
        }


        public bool AssignToEvent(Event @event)
        {
            if (IsAvailable(@event.StartTime, @event.EndTime))
            {
                foreach (var userId in UserIds)
                {
                    var user = _userProvider.GetItemById(userId);
                    user.AssignToEvent(@event);
                }
                AssignedEventIds.Add(@event.Id);
                return true;
            }
            Logger.Warn($"All members of this team are not available for this schedule");
            return false;
        }

        public bool IsAvailable(DateTime startTime, DateTime endTime)
        {
            var isAvailable = false;
            foreach (var userId in UserIds)
            {
                var user = _userProvider.GetItemById(userId);
                isAvailable &= user.IsAvailable(startTime, endTime);
            }

            return isAvailable;
        }
    }
}
