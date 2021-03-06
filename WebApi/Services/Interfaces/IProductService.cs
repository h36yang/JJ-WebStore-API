﻿using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.ViewModels;

namespace WebApi.Services.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductVM>> GetAllAsync();

        Task<List<ProductVM>> GetAllActiveAsync();

        Task<List<ProductVM>> GetByCategoryIdAsync(int categoryId);

        Task<ProductVM> GetByIdAsync(int id);

        Task<ProductVM> AddAsync(ProductForUpdateVM product);

        Task<int> UpdateAsync(ProductForUpdateVM product);

        Task<int> ActivateAsync(int id);

        Task<int> DeleteAsync(int id);
    }
}
