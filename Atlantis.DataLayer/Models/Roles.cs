using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace AtlantisShop.DataLayer.Models
{
    public partial class Roles
    {
        public Roles()
        {
            UserRoles = new HashSet<UserRoles>();
        }

        public int RoleId { get; set; }
        public string RoleTitle { get; set; }
        public string RoleName { get; set; }
        public bool? IsDelete { get; set; }

        public virtual ICollection<UserRoles> UserRoles { get; set; }
    }
}
