using E_commerce.DAl.Model;
using E_commerce.DAl.Repository;
using E_commerce.DAl.Repository.Implementation;
using E_Commerce.Bll.Manager.Abstraction;
using E_Commerce.Bll.Manager.Implementation;
using E_CommerceApp.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace E_CommerceApp.Controllers
{
    public class SearchController : Controller
    {
        private ICategoryManager CategoryManager;
        private IProductManager ProductManager;
        private IUnitOfWork unitOfWork;
        public SearchController(ICategoryManager categoryManager,IUnitOfWork unitOfWork , IProductManager ProductManager)
        {
            CategoryManager = categoryManager;
            this.unitOfWork = unitOfWork;
            this.ProductManager = ProductManager;
        }

        public async Task<IActionResult> SearchPage( string searchTerm)
        {
            
           var res=  await CategoryManager.GetAll();
            SearchResultViewModel search = new SearchResultViewModel
            {
                SearchItem = searchTerm,
                CategoryList = res
            };
            return View("SearchPage", search);
        }
        [HttpPost]
        public async Task< IActionResult> Index(SearchResultViewModel search)
        {
           // var pr = unitOfWork.Products.GetProductByName(search.SearchItem);
            var productList = await ProductManager.GetAllsearchbyname(search.SearchItem,search.CategoryId) ;
          
            return View("ShowResult", productList);
            
        }
    }
}
