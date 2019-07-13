using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.DataAccess.Entities;
using WebApi.DataAccess.Extensions;
using WebApi.DataAccess.Repositories.Interfaces;
using WebApi.Services.Interfaces;
using WebApi.ViewModels;

namespace WebApi.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductCategoryRepository _productCategoryRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IProductCategoryRepository productCategoryRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _productCategoryRepository = productCategoryRepository;
            _mapper = mapper;
        }

        public async Task<List<ProductVM>> GetAllAsync()
        {
            List<Product> dbProducts = await _productRepository.GetAllAsync();
            return _mapper.Map<List<Product>, List<ProductVM>>(dbProducts);
        }

        public async Task<List<ProductVM>> GetAllActiveAsync()
        {
            List<Product> dbProducts = await _productRepository.FindAsync(x => x.IsActive);
            return _mapper.Map<List<Product>, List<ProductVM>>(dbProducts);
        }

        public async Task<List<ProductVM>> GetByCategoryIdAsync(int categoryId)
        {
            if (!await _productCategoryRepository.ExistAsync(categoryId))
            {
                return null;
            }
            // Assumption: only return active products
            List<Product> dbProducts = await _productRepository.FindAsync(x => x.IsActive && x.CategoryId == categoryId);
            return _mapper.Map<List<Product>, List<ProductVM>>(dbProducts);
        }

        public async Task<ProductVM> GetByIdAsync(int id)
        {
            Product dbProduct = await _productRepository.GetAsync(id,
                includes: q => q.Include(x => x.ProductImages).Include(x => x.Functions));
            if (dbProduct.IsObjectNull())
            {
                return null;
            }
            return _mapper.Map<Product, ProductVM>(dbProduct);
        }

        public async Task<ProductVM> AddAsync(ProductVM product)
        {
            Product dbProduct = _mapper.Map<ProductVM, Product>(product);
            await _productRepository.AddAsync(dbProduct);
            return _mapper.Map<Product, ProductVM>(dbProduct);
        }

        public async Task<int> UpdateAsync(ProductVM product)
        {
            Product dbProduct = _mapper.Map<ProductVM, Product>(product);
            return await _productRepository.UpdateAsync(dbProduct);
        }

        public async Task<int> ActivateAsync(int id)
        {
            return await SetActiveAsync(id, true);
        }

        public async Task<int> DeleteAsync(int id)
        {
            return await SetActiveAsync(id, false);
        }

        private async Task<int> SetActiveAsync(int id, bool active)
        {
            Product dbProduct = await _productRepository.GetAsync(id);
            if (dbProduct.IsObjectNull())
            {
                return -1;
            }
            dbProduct.IsActive = active;
            return await _productRepository.UpdateAsync(dbProduct);
        }
    }
}
