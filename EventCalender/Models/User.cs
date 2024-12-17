using Core.Utility;
using EventCalender.Interfaces;

namespace EventCalender.Models
{
    public class User : CommonProperty, IEventAssign
    {
        public User(string name, DateTime workStartTime, DateTime workEndTime):base(name)
        {
            AssignedEvents = new List<Event>();
            SetWorkingHour(workStartTime,workEndTime);
        }
        public DateTime WorkStartTime { get; private set; }
        public DateTime WorkEndTime { get; private set; }

        public string TeamId { get; private set; } = string.Empty;

        public List<Event> AssignedEvents { get; set; }

        public void AssignToTeam(string teamId)
        {
            if (!string.IsNullOrEmpty(TeamId))
            {
                throw new Exception($"Already assigned to a team");
            }
            TeamId = teamId;
        }

        public void RemoveFromTeam()
        {
            TeamId = "";
        }

        public bool AssignToEvent(Event @event)
        {
            if (IsAvailable(@event.StartTime, @event.EndTime))
            {
                AssignedEvents.Add(@event);
                Logger.Info($"User {Name} assigned to event {@event.Name}");
                return true;
            }
            Logger.Warn($"User {Name} is not available to assign event {@event.Name}");
            return false;
        }

        public bool IsAvailable(DateTime startTime, DateTime endTime)
        {
            if (AssignedEvents.Count == 0 || AssignedEvents.LastOrDefault().EndTime < startTime)
            {
                return true;
            }
            var last = DateTime.MinValue;
            foreach (var t in AssignedEvents)
            {
                if (last < startTime && endTime < t.StartTime)
                {
                    return true;
                }
                last = t.EndTime;
            }

            return false;
        }

        public void SetWorkingHour(DateTime starTime, DateTime endTime)
        {
            if (endTime < starTime) throw new Exception("Invalid working hour duration");
            WorkEndTime = endTime;
            WorkStartTime = starTime;
        }
    }
}
