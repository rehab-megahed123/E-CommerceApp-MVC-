using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Bll.DTOS.Category
{
    public class Update_GetCategoryDTO
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public ICollection<Update_GetCategoryDTO>? Products { get; set; }
    }
}
