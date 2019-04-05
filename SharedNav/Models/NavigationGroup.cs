using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SharedNav.Models
{
    [Table("NavigationGroups")]
    public partial class NavigationGroup
    {
        public string TenantId { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Platform { get; set; }
    }
}
