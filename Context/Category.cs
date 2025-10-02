﻿namespace test_app.Context
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Url { get; set; } = null!;
        public List<Product> Products { get; set; } = [];

    }
}
