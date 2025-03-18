using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_commerce.DAl.Model;
using E_commerce.DAl.Repository.Abstraction;

namespace E_commerce.DAl.Repository
{
    public interface IUnitOfWork
    {
        IGenericRepository<Product> Products { get; }
        IGenericRepository<Category> Categories { get; }
        IGenericRepository<Order> Orders { get; }
        IGenericRepository<OrderItem> OrderItems { get; }
        IGenericRepository<ShoppingCart> ShoppingCarts { get; }
        Task<int> CompleteAsync(); 
    }
}
