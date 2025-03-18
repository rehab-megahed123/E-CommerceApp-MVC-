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
    public class OrderManager : IOrderManager
    {
        private readonly IUnitOfWork _unitOfWork;


        public OrderManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public List<Order> ShowAll(string  userid)
        {
            var res=_unitOfWork.Orders.GetAllByUserId(userid);
            return res;
        }
    }
}
