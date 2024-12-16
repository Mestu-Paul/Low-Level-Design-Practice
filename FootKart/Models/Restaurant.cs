namespace FootKart.Models
{
    public class Restaurant
    {
        public Restaurant(string name, string pincodes, string foodItemName, double foodItemPrice, int quantity)
        {
            Name = name;
            Pincodes = GetPinCodes(pincodes);
            FoodItemName = foodItemName;
            FoodItemPrice = foodItemPrice;
            Quantity = quantity;
            Reviews = new List<Review>();
        }

        private List<string> GetPinCodes(string pincodes)
        {
            return pincodes.Split('/').ToList();
        }

        public string Name { get; set; }
        public List<string> Pincodes { get; set; }
        public string FoodItemName { get; set; }
        public double FoodItemPrice { get; set; }
        public int Quantity { get; set; }

        public List<Review> Reviews { get; set; }

        public double Rating { get; private set; }

        public void AddReview(double rating, string comment, User user)
        {
            if (rating < 1 || rating > 5) throw new Exception("Invalid rating, must be between 1 to 5");
            var totalRating = Rating * Reviews.Count;
            Reviews.Add(new Review(user.PhoneNumber, comment, rating));

            totalRating += rating;
            Rating = totalRating / Reviews.Count;
        }

        public void SetLocations(string pincodes)
        {
            Pincodes = GetPinCodes(pincodes);
        }

        public bool IsCoverArea(string pincode)
        {
            return Pincodes.Contains(pincode);
        }

        public bool HasItem()
        {
            return Quantity > 0;
        }

    }
}
