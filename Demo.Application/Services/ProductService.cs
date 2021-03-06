﻿using Demo.Application.Interfaces;
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
        //private readonly IProductRepository _productRepository;
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
            var mapped = ProductMapper.Mapper.Map<IEnumerable<ProductModel>>(productList);
            return mapped;
        }

        public async Task<ProductModel> GetProductById(int productId)
        {
            var product = await _unitOfWork.ProductRepository.GetByIdAsync(productId);
            var mapped = ProductMapper.Mapper.Map<ProductModel>(product);
            return mapped;
        }


        public async Task<IEnumerable<ProductModel>> GetProductByName(string productName)
        {
            var productList = await _unitOfWork.ProductRepository.GetProductByName(productName);
            var mapped = ProductMapper.Mapper.Map<IEnumerable<ProductModel>>(productList);
            return mapped;
        }

        public async Task<IEnumerable<ProductModel>> GetProductByCategory(string category)
        {
            var productList = await _unitOfWork.ProductRepository.GetProductByCategory(category);
            var mapped = ProductMapper.Mapper.Map<IEnumerable<ProductModel>>(productList);
            return mapped;
        }

        //bu metod IRepository AddRangeAsync metodu olarak implemente edilmesi daha doğru olur. 
        //commit örneği vermek amaçlı metod eklendi.
        public async Task<int> CreateProducts(IEnumerable<ProductModel> productModels)
        {
            var mapped = ProductMapper.Mapper.Map<IEnumerable<Product>>(productModels);

            foreach (var product in mapped)
            {
                _unitOfWork.ProductRepository.AddAsync(product);
            }

           return await _unitOfWork.CommitAsync(); 
        }

        public async Task<ProductModel> Create(ProductModel productModel)
        {
            await ValidateProductIfExist(productModel);

            var mappedEntity = ProductMapper.Mapper.Map<Product>(productModel);
            if (mappedEntity == null)
                throw new ApplicationException($"Entity could not be mapped.");

            var newEntity = await _unitOfWork.ProductRepository.AddAsync(mappedEntity);
            await _unitOfWork.CommitAsync();

            var newMappedEntity = ProductMapper.Mapper.Map<ProductModel>(newEntity);
            return newMappedEntity;
        } 

        public async Task Update(ProductModel productModel)
        {
            await ValidateProductIfNotExist(productModel);

            var updatedEntity = ProductMapper.Mapper.Map<Product>(productModel);

            var editProduct = await _unitOfWork.ProductRepository.GetByIdAsync(productModel.Id);
            if (editProduct == null)
                throw new ApplicationException($"Entity could not be loaded.");

            editProduct.Category = updatedEntity.Category;
            editProduct.Name = updatedEntity.Name;
            editProduct.Price = updatedEntity.Price;
            editProduct.Description = updatedEntity.Description;

            await _unitOfWork.ProductRepository.Update(editProduct);
            await _unitOfWork.CommitAsync();
        }

        public async Task Delete(ProductModel productModel)
        {
            await ValidateProductIfNotExist(productModel);
            var deletedProduct = await _unitOfWork.ProductRepository.GetByIdAsync(productModel.Id);
            if (deletedProduct == null)
                throw new ApplicationException($"Entity could not be loaded.");

            await _unitOfWork.ProductRepository.Delete(deletedProduct);
            await _unitOfWork.CommitAsync();
        }

        private async Task ValidateProductIfExist(ProductModel productModel)
        {
            var existingEntity = await _unitOfWork.ProductRepository.GetByIdAsync(productModel.Id);
            if (existingEntity != null)
                throw new ApplicationException($"{productModel.Id} with this id already exists");
        }

        private async Task ValidateProductIfNotExist(ProductModel productModel)
        {
            var existingEntity = await _unitOfWork.ProductRepository.GetByIdAsync(productModel.Id);
            if (existingEntity == null)
                throw new ApplicationException($"{productModel.Id} with this id is not exists");
        } 
    }
}
