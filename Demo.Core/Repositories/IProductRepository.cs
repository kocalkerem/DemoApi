using System;
using System.Collections.Generic;
using System.Text;
using Demo.Core.Repositories.Base;
using System.Threading.Tasks;
using Demo.Core.Entities;

namespace Demo.Core.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IEnumerable<Product>> GetProductByCategory(string categoryName);
        Task<IEnumerable<Product>> GetProductByName(string name);
    }
}
