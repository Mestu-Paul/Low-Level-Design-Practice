namespace FoodKart.Models
{
    public class Review
    {
        public Review(string userId, string comment, double rating)
        {
            Id = Guid.NewGuid().ToString();
            UserId = userId;
            Comment = comment;
            Rating = rating;
        }

        public string Id { get; set; }
        public string UserId { get; set; }
        public string Comment { get; set; }
        public double Rating { get; set; }

    }
}
