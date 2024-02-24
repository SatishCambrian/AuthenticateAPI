﻿namespace AuthenticateApiClassLibrary.Data
{
    public class Product
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }

        // Add a Category property
        public required Category ProductCategory { get; set; }
    }
}