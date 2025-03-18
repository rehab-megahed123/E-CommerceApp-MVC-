using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace E_commerce.DAl.Model
{
    public class Product
    {
        
            public int ProductId { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public decimal Price { get; set; }
            public string ImageUrl { get; set; }
            public int CategoryId { get; set; }
            public Category Category { get; set; }
            public int Stock { get; set; }
        [NotMapped]
        [DisplayName("Upload image")]
        public IFormFile formFile { get; set; }

    }
}
