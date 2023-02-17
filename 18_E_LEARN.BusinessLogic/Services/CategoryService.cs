using _18_E_LEARN.DataAccess.Data.Context;
using _18_E_LEARN.DataAccess.Data.Models.Categories;
using _18_E_LEARN.DataAccess.Data.Models.User;
using _18_E_LEARN.DataAccess.Data.ViewModels.Category;
using _18_E_LEARN.DataAccess.Data.ViewModels.User;
using AutoMapper;
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
        AppDbContext context = new AppDbContext();

        public async Task<ServiceResponse> GetAllCategories()
        {
            List<Category> categories = context.Categories.ToList();
            if (categories.Count == 0)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = "No categories were found",
                };
            }
            else
            {
                return new ServiceResponse
                {
                    Success = true,
                    Message = "All categories were loaded",
                    Payload = categories
                };
            }
        }
    }
}
