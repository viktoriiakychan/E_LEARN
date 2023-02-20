using _18_E_LEARN.DataAccess.Data.Context;
using _18_E_LEARN.DataAccess.Data.IRepository;
using _18_E_LEARN.DataAccess.Data.Models.Categories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _18_E_LEARN.DataAccess.Data.Repository
{
    public class CategoryRepository : ICategoryRepository // implementing 
    {
        public async Task<List<Category>> GetAllAsync() // just GetALl, no GetAllCategories
        {
            using (var _context = new AppDbContext()) // to close all connections
            {
                List<Category> categories = await _context.Categories.ToListAsync();
                return categories;
            }
        }
        public void EditCategory(Category category)
        {
            using (var _context = new AppDbContext()) // to close all connections
            {
                _context.Categories.First(c => c.Id == category.Id).Name = category.Name;
                _context.SaveChanges();
            }
        }
        public Category GetById(int id)
        {
            using (var _context = new AppDbContext()) // to close all connections
            {
                var category = _context.Categories.Find(id);
                return category;
            }
        }
        public void DeleteCategory(int id)
        {
            using (var _context = new AppDbContext()) // to close all connections
            {
                _context.Categories.Remove(GetById(id));
                _context.SaveChanges();
            }
        }
        public void AddCategory(Category category)
        {
            using (var _context = new AppDbContext()) // to close all connections
            {
                _context.Categories.Add(category);
                _context.SaveChanges();
            }
        }
    }
}
