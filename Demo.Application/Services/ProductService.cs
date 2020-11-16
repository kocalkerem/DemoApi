using Demo.Application.Interfaces;
using Demo.Application.Mapper;
using Demo.Application.Models;
using Demo.Core.Entities;
using Demo.Core.Repositories;
using Demo.Core.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demo.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        ////tran sız işlemler için tercih edilecek ctor
        ////şuanki yapıda ihtiyaç yok.
        //public ProductService(IProductRepository productRepository)
        //{
        //    _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository)); 
        //    //loglama interface i inject edilip ilgili business logic lerde log atılabilir.
        //}

        public ProductService(IUnitOfWork unitOfWork)//tran lı işlemler için tercih edilecek ctor
        {
            _unitOfWork = unitOfWork;       
        }

        public async Task<IEnumerable<ProductModel>> GetProductList()
        {
            var productList = await _unitOfWork.ProductRepository.GetAllAsync();
            var mapped = ObjectMapper.Mapper.Map<IEnumerable<ProductModel>>(productList);
            return mapped;
        }

        public async Task<ProductModel> GetProductById(int productId)
        {
            var product = await _unitOfWork.ProductRepository.GetByIdAsync(productId);
            var mapped = ObjectMapper.Mapper.Map<ProductModel>(product);
            return mapped;
        }


        public async Task<IEnumerable<ProductModel>> GetProductByName(string productName)
        {
            var productList = await _unitOfWork.ProductRepository.GetProductByName(productName);
            var mapped = ObjectMapper.Mapper.Map<IEnumerable<ProductModel>>(productList);
            return mapped;
        }

        public async Task<IEnumerable<ProductModel>> GetProductByCategory(string category)
        {
            var productList = await _unitOfWork.ProductRepository.GetProductByCategory(category);
            var mapped = ObjectMapper.Mapper.Map<IEnumerable<ProductModel>>(productList);
            return mapped;
        }

        //bu metod IRepository AddRangeAsync metodu olarak implemente edilmesi daha doğru olur. 
        //commit örneği vermek amaçlı metod eklendi.
        public async Task<int> CreateProducts(IEnumerable<ProductModel> productModels)
        {
            var mapped = ObjectMapper.Mapper.Map<IEnumerable<Product>>(productModels);

            foreach (var product in mapped)
            {
                await _unitOfWork.ProductRepository.AddAsync(product);
            }

           return await _unitOfWork.CommitAsync(); 
        }

        public async Task<ProductModel> Create(ProductModel productModel)
        {
            await ValidateProductIfExist(productModel);

            var mappedEntity = ObjectMapper.Mapper.Map<Product>(productModel);
            if (mappedEntity == null)
                throw new ApplicationException($"Entity could not be mapped.");

            var newEntity = await _unitOfWork.ProductRepository.AddAsync(mappedEntity); 

            var newMappedEntity = ObjectMapper.Mapper.Map<ProductModel>(newEntity);
            return newMappedEntity;
        } 

        public async Task Update(ProductModel productModel)
        {
            ValidateProductIfNotExist(productModel);

            var editProduct = await _unitOfWork.ProductRepository.GetByIdAsync(productModel.Id);
            if (editProduct == null)
                throw new ApplicationException($"Entity could not be loaded.");

            ObjectMapper.Mapper.Map<ProductModel, Product>(productModel, editProduct);

            await _productRepository.UpdateAsync(editProduct); 
        }

        public async Task Delete(ProductModel productModel)
        {
            ValidateProductIfNotExist(productModel);
            var deletedProduct = await _unitOfWork.ProductRepository.GetByIdAsync(productModel.Id);
            if (deletedProduct == null)
                throw new ApplicationException($"Entity could not be loaded.");

            await _productRepository.DeleteAsync(deletedProduct); 
        }

        private async Task ValidateProductIfExist(ProductModel productModel)
        {
            var existingEntity = await _productRepository.GetByIdAsync(productModel.Id);
            if (existingEntity != null)
                throw new ApplicationException($"{productModel.ToString()} with this id already exists");
        }

        private void ValidateProductIfNotExist(ProductModel productModel)
        {
            var existingEntity = _productRepository.GetByIdAsync(productModel.Id);
            if (existingEntity == null)
                throw new ApplicationException($"{productModel.ToString()} with this id is not exists");
        }


    }
}
