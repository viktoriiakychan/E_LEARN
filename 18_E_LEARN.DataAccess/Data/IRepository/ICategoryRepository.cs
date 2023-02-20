using _18_E_LEARN.DataAccess.Data.Models.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _18_E_LEARN.DataAccess.Data.IRepository
{
    public interface ICategoryRepository // describe something
    {
        Task<List<Category>> GetAllAsync();
        void EditCategory(Category category);
        public Category GetById(int id);
        public void DeleteCategory(int id);
        void AddCategory(Category category);
    }
}
