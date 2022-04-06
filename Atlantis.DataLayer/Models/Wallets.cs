using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace AtlantisShop.DataLayer.Models
{
    public partial class Wallets
    {
        public int WalletId { get; set; }
        public int WalletTypeId { get; set; }
        public int UserId { get; set; }
        public int Amount { get; set; }
        public string Description { get; set; }
        public bool IsPay { get; set; }
        public DateTime CreateDate { get; set; }

        public virtual Users User { get; set; }
        public virtual WalletTypes WalletType { get; set; }
    }
}
