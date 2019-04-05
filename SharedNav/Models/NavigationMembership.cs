using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SharedNav.Models
{
    [Table("NavigationMemberships")]
    public partial class NavigationMembership
    {
        public string NavigationGroupTenantId { get; set; }
        public string Member { get; set; }
        public string Type { get; set; }
        public string NavigationGroupId { get; set; }
    }
}
