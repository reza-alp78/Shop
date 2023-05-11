using Core.Domain.Entity.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces.Categories
{
    public interface ICategory
    {
        public Task<List<Category>> GetAllCategories();
        public Task<Category> GetCategory(int id);
        public Task<Category> AddCategory(Category category);
        public Category UpdateCategory(Category category);
        public void DeleteCategory(Category category);
    }
}
