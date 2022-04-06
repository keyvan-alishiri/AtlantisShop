using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace AtlantisShop.DataLayer.Models
{
    public partial class Users
    {
        public Users()
        {
            Address = new HashSet<Address>();
            UserRoles = new HashSet<UserRoles>();
            Wallets = new HashSet<Wallets>();
        }

        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ActivateCode { get; set; }
        public bool? IsActive { get; set; }
        public string UserAvatar { get; set; }
        public DateTime? RegisterDate { get; set; }
        public string Mobile { get; set; }
        public string Tell { get; set; }
        public bool? IsDelete { get; set; }

        public virtual ICollection<Address> Address { get; set; }
        public virtual ICollection<UserRoles> UserRoles { get; set; }
        public virtual ICollection<Wallets> Wallets { get; set; }
    }
}
