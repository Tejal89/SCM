using SCM.Interfaces;
using SCM.Models;
using System;
using System.Collections.Generic;

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
                    SKU = "P_PRO1", //physical product
                    Price = 300,
                    Weight = 300,
                    WeightUnit = "grams",
                    IsDigital = false,
                    IsMembership = false
            },
                new MembershipProduct(){
                    ProductId = 2,
                    Name =  "Wuthering Heights",
                    SKU = "PMB_ISBN_PRO1", //basic membership product , ISBN indicates product is a book
                    Price = 1200,
                    Weight = 30,
                    WeightUnit = "grams",
                    MemberShipType = MemberShipType.Basic,
                    IsMembership = true,
                    IsDigital = false
                },
                new DigitalProduct(){
                    ProductId = 3,
                    Name =  "Learning to Ski",
                    SKU = "PD__PRO1", //digital video
                    Price = 200,
                    IsMembership = false,
                    IsDigital = true,
                    AccessPath = "https://www.youtube.com/watch?v=_yfFGDuJ2g0"
                },
                new DigitalProduct(){
                    ProductId = 4,
                    Name =  "First Aid",
                    SKU = "PD__PRO2", // free digital video
                    Price = 20,
                    IsMembership = false,
                    IsDigital = true,
                    AccessPath = "https://www.youtube.com/watch?v=WNo8brNcVqE"
                },
            };
        }
    }
}