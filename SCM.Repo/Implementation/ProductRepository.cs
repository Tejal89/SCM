using SCM.Interfaces;
using SCM.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SCM.Implementation
{
    public class ProductRepository : IProductRepository
    {
        public ProductRepository()
        {
        }

        public IEnumerable<Product> GetProducts()
        {
            return new List<Product>
            {
                new PhysicalProduct(){
                    ProductId = 1,
                    Name =  "Physical Product",
                    SKU = "PRO1", //physical product
                    Price = 300,
                    Weight = 300,
                    WeightUnit = "grams"
                },
                new Product(){
                    ProductId = 2,
                    Name =  "Wuthering Heights",
                    SKU = "PRO2", //basic membership product 
                    Price = 1200,
                    Weight = 30,
                    WeightUnit = "grams",
                },
                new DigitalProduct(){
                    ProductId = 3,
                    Name =  "Learning to Ski",
                    SKU = "PRO3", //digital video
                    Price = 200,
                    AccessPath = "https://www.youtube.com/watch?v=_yfFGDuJ2g0"
                },
                new DigitalProduct(){
                    ProductId = 4,
                    Name =  "First Aid",
                    SKU = "PRO3", // free digital video
                    Price = 20,
                    AccessPath = "https://www.youtube.com/watch?v=WNo8brNcVqE"
                },
                new MemberShipProduct(){
                    ProductId = 5,
                    Name =  "Netflix Membership",
                    SKU = "PRO4", //digital video
                    Price = 200,
                    MemberShipType = MemberShipType.Basic
                }
            };
        }

        public Product GetProductById(long ProductId)
        {
            if (GetProducts().Any(x => x.ProductId == ProductId))
                return GetProducts().FirstOrDefault(x => x.ProductId == ProductId);

            return null;
        }
        }
}