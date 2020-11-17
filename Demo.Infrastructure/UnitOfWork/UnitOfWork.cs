using Demo.Core.Repositories;
using Demo.Core.UnitOfWork;
using Demo.Infrastructure.Data;
using Demo.Infrastructure.Repository;
using System;
using System.Threading.Tasks;

namespace Demo.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        bool disposed; 
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
         
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    //dispose managed resources
                }
            }
            //dispose unmanaged resources
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
