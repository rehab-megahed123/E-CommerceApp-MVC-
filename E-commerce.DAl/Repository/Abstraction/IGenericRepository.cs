using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_commerce.DAl.Model;
using Microsoft.EntityFrameworkCore;

namespace E_commerce.DAl.Repository.Abstraction
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<T> GetByNameAsync(string name);
        Task<List<T>> GetByForeignKey(int id);

        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
        Product GetProductByName(string Name);
        List<Product> GetByCategory(int Catid, string productName);
        public Task UpdateProduct(Product product);
        public void Remove(Product product);
        public Task<List<Product>> GetAllsearchbyname(string searchTerm = null, int? categoryId = null);

        public List<Order> GetAllByUserId(string userId);
    }
}
