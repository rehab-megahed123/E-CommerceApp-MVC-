using E_commerce.DAl.Model;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace E_CommerceApp.ViewModel
{
    public class AddProductVM
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }
        public int CategoryId { get; set; }
        public IFormFile formFile { get; set; }
       public  List<Category>? CategoryList { get; set; }
        public SelectList? CategoryOptions { get; set; }


    }
}
