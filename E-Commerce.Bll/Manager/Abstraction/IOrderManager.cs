using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_commerce.DAl.Model;

namespace E_Commerce.Bll.Manager.Abstraction
{
  public  interface IOrderManager
    {
        public List<Order> ShowAll(string userId);
        public Task<List<Order>> Show();

    }
}
