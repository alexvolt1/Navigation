using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SharedNav.Models
{
    [Table("NavigationItemView")]
    public partial class NavigationItemViewSingle
    {
        public int Id { get; set; }
        public string ClientId { get; set; }
        public string TopicId { get; set; }
        public string ViewId { get; set; }
        public string Rtype { get; set; }
    }
}
