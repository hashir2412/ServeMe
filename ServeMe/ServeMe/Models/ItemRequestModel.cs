using System;

namespace ServeMe.Models
{
    public class ItemRequestModel
    {
        public ServiceDto Service { get; set; }

        public int Quantity { get; set; }

        public DateTime Date { get; set; }

        public double Rate { get; set; }
    }
}
