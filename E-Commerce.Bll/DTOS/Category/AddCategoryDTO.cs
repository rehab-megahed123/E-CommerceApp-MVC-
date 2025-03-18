using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Bll.DTOS.Category
{
   public class AddCategoryDTO
    {
        
        public string Name { get; set; }
        public ICollection<CreateDTOProduct>? Products { get; set; }
    }
}
