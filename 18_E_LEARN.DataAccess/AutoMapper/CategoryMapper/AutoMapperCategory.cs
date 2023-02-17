using _18_E_LEARN.DataAccess.Data.Models.Categories;
using _18_E_LEARN.DataAccess.Data.Models.User;
using _18_E_LEARN.DataAccess.Data.ViewModels.Category;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _18_E_LEARN.DataAccess.AutoMapper.CategoryMapper
{
    public class AutoMapperCategory : Profile
    {
        public AutoMapperCategory()
        {
            CreateMap<Category, CategoryVM>().ReverseMap();
        }
    }
}
