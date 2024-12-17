using EventCalender.Models;

namespace EventCalender.Services
{
    public class DataProviderHelper
    {
        public int GetIndexById<T>(List<T> items, T item) where T : CommonProperty
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].Id == item.Id) return i;
            }

            return -1;
        }
    }
}
