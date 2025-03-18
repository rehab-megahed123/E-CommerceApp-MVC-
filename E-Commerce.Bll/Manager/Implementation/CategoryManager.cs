using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_commerce.DAl.Model;
using E_commerce.DAl.Repository;
using E_commerce.DAl.Repository.Implementation;
using E_Commerce.Bll.Manager.Abstraction;

namespace E_Commerce.Bll.Manager.Implementation
{
    
    public class CategoryManager : ICategoryManager
    {
        private IUnitOfWork Unit;
        public CategoryManager(IUnitOfWork unit)
        {
            Unit = unit;
        }
        public async Task AddCategory(Category category)
        {
           await Unit.Categories.AddAsync(category);
            await Unit.CompleteAsync();
        }

        public async Task DeleteCategory(int id)
        {
            await Unit.Categories.DeleteAsync(id);
            await Unit.CompleteAsync();
        }

        public async Task<List<Category>> GetAll()
        {
            
            var res = await Unit.Categories.GetAllAsync();

            
            await Unit.CompleteAsync();

            
            return res.ToList();
        }


        public async Task< Category> GetById(int id)
        {
            var res = await Unit.Categories.GetByIdAsync(id);
            
            return res;
        }

        public Task<Category> GetByName(string name)
        {
           var res= Unit.Categories.GetByNameAsync(name);
            return res;
        }

        public async Task SaveContext()
        {
            await Unit.CompleteAsync();
        }

        public async Task UpdateCategory(Category category)
        {
            await Unit.Categories.UpdateAsync(category);
            await Unit.CompleteAsync();

        }

        
    }
}
