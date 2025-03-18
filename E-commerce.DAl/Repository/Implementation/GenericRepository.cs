using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_commerce.DAl.Model;
using E_commerce.DAl.Repository.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace E_commerce.DAl.Repository.Implementation
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
            }
        }

        public async Task<T> GetByNameAsync(string name)
        {
            var res = await _dbSet.FirstOrDefaultAsync(e => EF.Property<string>(e, "Name") == name);
            return res;
        }

        public async Task<List<T>> GetByForeignKey(int id)
        {
            return await _dbSet.Where(e => EF.Property<int>(e, "CategoryId") == id).ToListAsync();
        }

        public Product GetProductByName(string Name)
        {
            var res = _context.Products.FirstOrDefault(p => p.Name == Name);
            return res;
        }

        public List<Product> GetByCategory(int id, string productName)
        {

            var res = _context.Products.Where(c => c.CategoryId == id && c.Name == productName).ToList();

            return res;
        }
        public async  Task UpdateProduct(Product product)
        {

            var prod = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == product.ProductId);
            if (prod == null) throw new Exception("Product not found");
            prod.Name = product.Name;
            prod.ProductId = product.ProductId;
            prod.Price = product.Price;
            prod.Description = product.Description;
            prod.ImageUrl = product.ImageUrl;
            prod.Stock = product.Stock;
            prod.CategoryId = product.CategoryId;
            await _context.SaveChangesAsync();

        }
        public void Remove(Product product)
        {
            _context.Products.Remove(product);
            _context.SaveChanges();
        }
        public Task<List<Product>> GetAllsearchbyname(string searchTerm = null, int? categoryId = null)
        {
            var query = _context.Products.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                searchTerm = searchTerm.ToLower();
                query = query.Where(p => p.Name.ToLower().Contains(searchTerm));
            }

            if (categoryId != null && categoryId != 0)
            {
                query = query.Where(p => p.CategoryId == categoryId);
            }

            return query.ToListAsync();
        }

        public List<Order> GetAllByUserId(string userId)
        {
           var res= _context.Orders.Include(a=>a.OrderItems).Where(o => o.UserId == userId).ToList();
            return res;

        }
    }
}
