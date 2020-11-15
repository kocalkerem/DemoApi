using Demo.Core.Entities;
using Demo.Core.Repositories;
using Demo.Infrastructure.Data;
using Demo.Infrastructure.Repository.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Demo.Infrastructure.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository 
    {
        public ProductRepository(ProductContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Product>> GetProductByCategory(string categoryName)
        {
            var productList = await _dbContext.Products
                      .Where(o => o.Category == categoryName)
                      .ToListAsync();

            return productList;
        }

        public async Task<IEnumerable<Product>> GetProductByName(string name)
        {
            var productList = await _dbContext.Products
                      .Where(o => o.Name == name)
                      .ToListAsync();

            return productList;
        }
    }
}
