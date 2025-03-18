using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace E_commerce.DAl.Model
{
    public  class ApplicationUser : IdentityUser
{
    public string? FullName { get; set; }
    
    public string? Address { get; set; }
    public ICollection<Order>? Orders { get; set; }
    public string? UserType { get; set; } // Customer, Buyer, Admin
}
    
}
