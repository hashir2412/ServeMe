using System;

namespace ServeMe.Models
{
    public class ItemDto
    {
        public int CartId { get; set; }
        public string Name { get; set; }

        public int Quantity { get; set; }

        public double Rate { get; set; }

        public DateTime Date { get; set; }

        public int StatusId { get; set; }

        public ServiceDto Service { get; set; }
    }
}
