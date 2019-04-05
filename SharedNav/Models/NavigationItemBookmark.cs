using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SharedNav.Models
{
    [Table("NavigationItemBookmarks")]
    public partial class NavigationItemBookmark
    {
        public string NavigationItemTenantId { get; set; }
        public string UserId { get; set; }
        public string NavigationItemId { get; set; }
        public int Sequence { get; set; }
    }
}
