using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SharedNav.Models
{
    [Table("NavigationItems")]
    public partial class NavigationItem
    {
        public string TenantId { get; set; }
        public string Id { get; set; }
        public string StoredType { get; set; }
        public string Owner { get; set; }
        public string DefaultName { get; set; }
        public string DefaultDescription { get; set; }
        public string Source { get; set; }
        public string ImageUrl { get; set; }
        public string LargeImageUrl { get; set; }
        public string IsMobile { get; set; }
        public string DocumentMode { get; set; }
    }
}
