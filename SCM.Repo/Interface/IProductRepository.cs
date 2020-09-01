﻿using SCM.Models;
using System;
using System.Collections;
using System.Collections.Generic;

namespace SCM.Interfaces
{
    public interface IProductRepository
    {
        public IEnumerable<Product> GetProducts();       
    }
}
