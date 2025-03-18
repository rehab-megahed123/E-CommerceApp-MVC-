using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_commerce.DAl.Model;
using E_commerce.DAl.Repository;
using E_Commerce.Bll.Manager.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Bll.Manager.Implementation
{
    public class ProductManager : IProductManager
    {
        private readonly IUnitOfWork _unitOfWork;

        
        public ProductManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task AddAsync(Product entity)
        {
           
                await _unitOfWork.Products.AddAsync(entity);
                _unitOfWork.CompleteAsync();
            
        }

        public async Task DeleteAsync(int id)
        {
           var product=await _unitOfWork.Products.GetByIdAsync(id);
           if(product!=null)
            {
                await _unitOfWork.Products.DeleteAsync(id);
            }
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            var res=await _unitOfWork.Products.GetAllAsync();
            return res;
        }

        public async Task<List<Product>> GetByCategoryIdAsync(int id)
        {
           var res= await _unitOfWork.Products.GetByForeignKey(id);
            return res;
        }

        public async  Task<Product> GetByIdAsync(int id)
        {
          var res=  await _unitOfWork.Products.GetByIdAsync(id);
            return res;
        }

        public async Task<Product> GetByNameAsync(string name)
        {
            var res = await _unitOfWork.Products.GetByNameAsync(name);
            return res;
        }

        public async Task  SaveChange()
        {
            await _unitOfWork.CompleteAsync();
        }

        public async Task UpdateAsync(Product entity)
        {
            await _unitOfWork.Products.UpdateAsync(entity);
        }

        public async Task UpdateProduct(Product product)
        {
           await _unitOfWork.Products.UpdateProduct(product);
        }
        public async Task<List<Product>> GetAllsearchbyname(string searchTerm = null, int? categoryId = null)
        {
           var res=await _unitOfWork.Products.GetAllsearchbyname(searchTerm, categoryId);
            return res;
        }
        }
}
