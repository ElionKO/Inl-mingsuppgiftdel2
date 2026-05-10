using System;
using System.Collections.Generic;

namespace BakeryAPI.Entities
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public decimal PricePerPiece { get; set; }
        public double Weight { get; set; }
        public int PackageSize { get; set; }

        public DateTime BestBefore { get; set; }
        public DateTime ManufactureDate { get; set; }

        public List<Order> Orders { get; set; }
    }
}