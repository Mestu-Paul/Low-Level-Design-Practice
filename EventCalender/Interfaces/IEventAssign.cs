using EventCalender.Models;

namespace EventCalender.Interfaces
{
    public interface IEventAssign
    {
        bool AssignToEvent(Event @event);
        bool IsAvailable(DateTime startTime, DateTime endTime);

    }
}
