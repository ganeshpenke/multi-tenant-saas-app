﻿namespace MultiTenantSaaS.Domain.Entities
{
    public class Product
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string Name { get; private set; } = string.Empty;
        public decimal Price { get; private set; }

        private Product() { }

        public Product(string name, decimal price)
        {
            Name = name;
            Price = price;
        }
    }
}
