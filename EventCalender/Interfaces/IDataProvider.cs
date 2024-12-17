namespace EventCalender.Interfaces
{
    public interface IDataProvider<T>
    {
        T GetItemById(string id);
        void SaveItem(T item);
        void DeleteItem(T item);
    }
}
