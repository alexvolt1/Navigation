using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SharedNav.Models
{
    [Table("NavigationItemRecents")]
    public partial class NavigationItemRecent
    {
        public string NavigationItemTenantId { get; set; }
        public string UserId { get; set; }
        public string NavigationItemId { get; set; }
        public DateTime DateViewed { get; set; }
    }
}
