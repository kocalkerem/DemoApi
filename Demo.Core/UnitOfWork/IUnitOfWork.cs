using Demo.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Core.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository  ProductRepository { get; } 
        Task<int> CommitAsync();
    }
}
