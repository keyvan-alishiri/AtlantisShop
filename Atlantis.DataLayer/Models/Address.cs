using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace AtlantisShop.DataLayer.Models
{
    public partial class Address
    {
        public int AddressId { get; set; }
        public int UserId { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Address1 { get; set; }

        public virtual Users User { get; set; }
    }
}
