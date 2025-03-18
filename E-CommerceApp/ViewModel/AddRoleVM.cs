using System.ComponentModel.DataAnnotations;

namespace E_CommerceApp.ViewModel
{
    public class AddRoleVM
    {
        [Display(Name = "Role Name")]
        public string RoleName { get; set; }
    }
}
