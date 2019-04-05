using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SharedNav.Models
{
    [Table("NavigationItemAuthorizations")]
    public partial class NavigationItemAuthorization
    {
        public string NavigationItemTenantId { get; set; }
        public string NavigationItemId { get; set; }
        public string NavigationGroupId { get; set; }
        public string NotInGroup { get; set; }
    }
}
