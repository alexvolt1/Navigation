using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SharedNav.Models
{
    [Table("NavigationItemViews")]
    public partial class NavigationItemView
    {
        public string NavigationItemTenantId { get; set; }
        public string UserId { get; set; }
        public string NavigationItemId { get; set; }
        public DateTime DateViewed { get; set; }
    }
}
