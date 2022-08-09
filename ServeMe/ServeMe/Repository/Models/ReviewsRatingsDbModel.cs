using System;

namespace ServeMe.Repository.Models
{
    public class ReviewsRatingsDbModel
    {
        public int RatingID { get; set; }

        public int VendorID { get; set; }

        public int CartID { get; set; }

        public int UserID { get; set; }

        public string Comment { get; set; }

        public int Stars { get; set; }

        public DateTime Date { get; set; }

        public ServiceDbModel Service { get; set; }

        public OrderDbModel Order { get; set; }

        public UserDbModel User { get; set; }
    }
}
