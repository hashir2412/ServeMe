using System;
using System.Collections.Generic;

namespace ServeMe.Models
{
    public class OrderDto
    {
        public int Id { get; set; }
        public string Address { get; set; }

        public DateTime Date { get; set; }

        public double Total { get; set; }

        public List<ItemDto> Items { get; set; }
    }
}
