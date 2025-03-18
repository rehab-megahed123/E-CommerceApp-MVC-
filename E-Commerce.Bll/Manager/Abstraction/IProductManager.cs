using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_commerce.DAl.Model;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Bll.Manager.Abstraction
{
    public interface IProductManager
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product> GetByIdAsync(int id);
        Task<List<Product>> GetByCategoryIdAsync(int id);
        Task<Product> GetByNameAsync(string name);
        Task AddAsync(Product entity);
        Task UpdateAsync(Product entity);
        Task DeleteAsync(int id);
        Task SaveChange();
        public Task UpdateProduct(Product product);
        public  Task<List<Product>> GetAllsearchbyname(string searchTerm = null, int? categoryId = null);
        

        }
}
