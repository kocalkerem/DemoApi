using Demo.Application.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Application.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductModel>> GetProductList();
        Task<ProductModel> GetProductById(int productId);
        Task<IEnumerable<ProductModel>> GetProductByName(string productName);
        Task<IEnumerable<ProductModel>> GetProductByCategory(string category);
        Task<ProductModel> Create(ProductModel productModel);
        Task<int> CreateProducts(IEnumerable<ProductModel> productModels);
        Task Update(ProductModel productModel);
        Task Delete(ProductModel productModel);
    }
}
