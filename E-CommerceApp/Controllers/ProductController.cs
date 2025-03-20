using E_commerce.DAl.Model;
using E_Commerce.Bll.Manager.Abstraction;
using E_CommerceApp.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace E_CommerceApp.Controllers
{
    public class ProductController : Controller
    {
        private IProductManager ProductManager;
        private ICategoryManager CategoryManager;

        public ProductController(IProductManager productManager,ICategoryManager categoryManager)
        {
            ProductManager = productManager;
            CategoryManager = categoryManager;
        }
        public IActionResult ShowProducts()
        {
            return View("Products");
        }
        
        public async Task< IActionResult> Details(int id)
        {
          var res= await ProductManager.GetByIdAsync(id);
            return View("Details",res);
        }
        [Authorize(Roles = "Admin,Seller") ]
        [HttpPost]
        public  async Task<IActionResult> SaveAddProduct(AddProductVM product)
        {
            if (ModelState.IsValid == true)
            {
                
                
                //Custom Validator
                if (product.CategoryId != 0)
                {
                    try
                    {
                        if (product.formFile != null && product.formFile.Length > 0)
                        {
                            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", product.formFile.FileName);
                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                product.formFile.CopyTo(stream);
                            }

                            product.ImageUrl = "/images/" + product.formFile.FileName;
                        }

                       await ProductManager.AddAsync(new Product
                        {
                           Name=product.Name,
                           Description=product.Description,
                           Price=product.Price,
                           ImageUrl=product.ImageUrl,
                           CategoryId=product.CategoryId,
                           formFile=product.formFile


                        });
                       await ProductManager.SaveChange();

                        return View("MainScreen");
                    }

                    catch (Exception ex)
                    {
                        ModelState.AddModelError("", ex.Message);

                    }
                }
                else
                {
                    ModelState.AddModelError("DepartmentId", "Please select Department");
                }
            }


           // product.CategoryList = await CategoryManager.GetAll();

            //product.CategoryOptions = new SelectList(product.CategoryList, "Id", "Name");


            return View("MainScreen");

        }
        public async Task< IActionResult> GetByName(string name)
        {
           var res= await CategoryManager.GetByName(name);
            if(res!=null)
            {
                
                    int categoryId = res.CategoryId;
                   var list= ProductManager.GetByCategoryIdAsync(categoryId);
                    if (list != null)
                    {
                        return View("SearchResult",list);
                    }
                    
                
            }
            return Content("This catigory not valid");

        }
        [Authorize(Roles = "Admin,Seller")]
        public async Task< IActionResult> Update(AddProductVM product)
        {
            
            return View("Update",product);

        }
        [Authorize(Roles = "Admin,Seller")]
        public async Task <IActionResult> SaveUpdateProduct(AddProductVM product)
        {

            //Custom Validator
            if (product.CategoryId != 0)
            {
                try
                {
                    if (product.formFile != null && product.formFile.Length > 0)
                    {
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", product.formFile.FileName);
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            product.formFile.CopyTo(stream);
                        }

                        product.ImageUrl = "/images/" + product.formFile.FileName;
                    }

                    Product product1 = new Product
                    {
                        Name = product.Name,
                        ProductId = product.ProductId,
                        Price = product.Price,
                        Description = product.Description,
                        ImageUrl = product.ImageUrl,
                        CategoryId = product.CategoryId

                    };
                  // await ProductManager.DeleteAsync(product.ProductId);
                   await ProductManager.UpdateProduct(product1);

                    
                    return View("MainScreen");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);

                }
            }
            else
            {
                ModelState.AddModelError("DepartmentId", "Please select Department");
            }
            return View("Update", product);


        }
        [Authorize(Roles = "Admin,Seller")]
        public async Task< IActionResult> Delete(AddProductVM product )
        {
            
           await ProductManager.DeleteAsync(product.ProductId);
            await ProductManager.SaveChange();
            return View("MainScreen");

        }
    }
}
