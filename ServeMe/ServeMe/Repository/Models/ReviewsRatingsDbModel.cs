using System;

namespace ServeMe.Repository.Models
{
    public class ReviewsRatingsDbModel
    {
        public int RatingID { get; set; }

        public int ServiceID { get; set; }

        public int OrderID { get; set; }

        public int UserID { get; set; }

        public string Comment { get; set; }

        public int Stars { get; set; }

        public DateTime Date { get; set; }
    }
}
