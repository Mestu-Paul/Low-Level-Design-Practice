namespace FootKart.Models
{
    public class User
    {
        public User(string name, string gender, string phoneNumber, string pincode)
        {
            Name = name;
            Gender = gender;
            PhoneNumber = phoneNumber;
            Pincode = pincode;
        }

        public string Name { get; set; }
        public string Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string Pincode { get; set; }
    }
}
