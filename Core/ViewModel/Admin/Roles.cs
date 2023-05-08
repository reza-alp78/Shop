using System.ComponentModel;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Core.ViewModel.Admin
{
    public class Roles
    {
        [DisplayName("ایمیل")]
        public string Email { get; set; }

        [DisplayName("نام نقش")]
        public string RoleName { get; set; }

        [DisplayName("انتخاب نقش")]
        public List<SelectListItem> SelectRoles { get; set; } = new List<SelectListItem>();

    }
}
