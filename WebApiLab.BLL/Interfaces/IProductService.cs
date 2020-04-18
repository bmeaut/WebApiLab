﻿using System.Collections.Generic;
using WebApiLab.Entities;

namespace WebApiLab.BLL
{
    public interface IProductService
    {
        Product GetProduct(int productId);
        IEnumerable<Product> GetProducts();
        Product InsertProduct(Product newProduct);
        void UpdateProduct(int productId, Product updatedProduct);
        void DeleteProduct(int productId);
    }
}
