using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace AtlantisShop.DataLayer.Models
{
    public partial class WalletTypes
    {
        public WalletTypes()
        {
            Wallets = new HashSet<Wallets>();
        }

        public int WalletTypeId { get; set; }
        public string WalletTypeTitle { get; set; }

        public virtual ICollection<Wallets> Wallets { get; set; }
    }
}
