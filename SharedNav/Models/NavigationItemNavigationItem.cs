using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SharedNav.Models
{
    [Table("NavigationItemNavigationItems")]
    public partial class NavigationItemNavigationItem
    {
        public string NavigationItemTenantId { get; set; }
        public string ParentId { get; set; }
        public string NavigationItemId { get; set; }
        public int Sequence { get; set; }
        public string Inherited { get; set; }
    }
}
