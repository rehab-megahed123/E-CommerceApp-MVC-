using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_commerce.DAl.Model;

namespace E_Commerce.Bll.Manager.Abstraction
{
    public interface ICategoryManager
    {
        public Task AddCategory(Category category);
        public Task<Category> GetById(int id);
        public Task<Category> GetByName(string name);
        public Task<List<Category>> GetAll();
        public Task UpdateCategory(Category category);
        public Task DeleteCategory(int id);
        public Task SaveContext();
    }
}
