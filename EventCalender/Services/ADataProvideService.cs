using EventCalender.Interfaces;
using EventCalender.Models;

namespace EventCalender.Services
{
    public abstract class ADataProvideService<T>:IDataProvider<T> where T : CommonProperty
    {
        private List<T> _items;

        private DataProviderHelper _dataProviderHelper;
        protected ADataProvideService()
        {
            _items = new List<T>();
            _dataProviderHelper= new DataProviderHelper();
        }

        public virtual T GetItemById(string id)
        {
            return _items.FirstOrDefault(x => x.Id == id);
        }

        public virtual void SaveItem(T item)
        {
            var index = _dataProviderHelper.GetIndexById<T>(_items, item);
            if (index == -1) _items.Add(item);
            else _items[index] = item;
        }

        public virtual void DeleteItem(T item)
        {
            _items.Remove(item);
        }
    }
}
