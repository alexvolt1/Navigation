using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SharedNav.Models
{
    [Table("NavigationItemLocales")]
    public partial class NavigationItemLocale
    {
        public string NavigationItemTenantId { get; set; }
        public string NavigationItemId { get; set; }
        public string Locale { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
