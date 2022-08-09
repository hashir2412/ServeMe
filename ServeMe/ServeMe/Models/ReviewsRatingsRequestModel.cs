namespace ServeMe.Models
{
    public class ReviewsRatingsRequestModel
    {
        public int VendorId { get; set; }

        public int CartId { get; set; }

        public int UserId { get; set; }

        public string Comment { get; set; }

        public int Stars { get; set; }
    }
}
