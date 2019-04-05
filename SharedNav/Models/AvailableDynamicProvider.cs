using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SharedNav.Models
{
    [Table("AvailableDynamicProviders")]
    public partial class AvailableDynamicProvider
    {
        public string AssemblyName { get; set; }
        public string ClassName { get; set; }
        public string IsDynamic { get; set; }
    }
}
