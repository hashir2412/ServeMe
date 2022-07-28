using System;

namespace ServeMe.Models
{
    public class ItemDto
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public int Quantity { get; set; }

        public double Rate { get; set; }

        public DateTime Date { get; set; }

        public int StatusID { get; set; }

        public ServiceDto Service { get; set; }
    }
}
