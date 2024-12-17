namespace EventCalender.Models
{
    public class CommonProperty
    {
        public CommonProperty(string name)
        {
            Id = name;
            Name = name;
        }

        public string Id { get; set; }
        public string Name { get; set; }
    }
}
