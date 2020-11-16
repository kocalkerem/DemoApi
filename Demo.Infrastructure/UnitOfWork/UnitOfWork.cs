using Demo.Core.Repositories;
using Demo.Core.UnitOfWork;
using Demo.Infrastructure.Data;
using Demo.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ProductContext _context;
        private ProductRepository _productRepository;

        public UnitOfWork(ProductContext context)
        {
           _context = context;  
        }

        public IProductRepository ProductRepository => _productRepository = _productRepository ?? new ProductRepository(_context); 

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
