using _18_E_LEARN.DataAccess.Data.Context;
using _18_E_LEARN.DataAccess.Data.IRepository;
using _18_E_LEARN.DataAccess.Data.Models.Categories;
using _18_E_LEARN.DataAccess.Data.Models.User;
using _18_E_LEARN.DataAccess.Data.ViewModels.User;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _18_E_LEARN.BusinessLogic.Services
{
    public class CategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<ServiceResponse> GetAllAsync()
        {
            List<Category> categories= await _categoryRepository.GetAllAsync();
            return new ServiceResponse
            {
                Success = true,
                Message = "All categories were loaded",
                Payload = categories
            };
        }
        public async Task<ServiceResponse> EditCategoryAsync(Category category)
        {
            _categoryRepository.EditCategory(category);
            return new ServiceResponse
            {
                Success = true,
                Message = "All categories were loaded"
            };
        }
        public async Task<ServiceResponse> GetByIdAsync(int id)
        {
            var category = _categoryRepository.GetById(id);
            if(category == null) 
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = "Nothing was found",
                };
            }
            return new ServiceResponse
            {
                Success = true,
                Message = "A category was found",
                Payload = category
            };

        }
        public async Task<ServiceResponse> DeleteIdAsync(int id)
        {
            var category = _categoryRepository.GetById(id);
            if (category == null)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = "Nothing was found",
                };
            }
            _categoryRepository.DeleteCategory(id);
            return new ServiceResponse
            {
                Success = true,
                Message = "A category was deleted",
                Payload = category
            };
        }
        public async Task<ServiceResponse> AddCategoryAsync(Category category)
        {
            _categoryRepository.AddCategory(category);
            return new ServiceResponse
            {
                Success = true,
                Message = "All categories were loaded"
            };
        }
    }
}
