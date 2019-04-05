using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SharedNav.Models
{
    [Table("DynamicProviders")]
    public partial class DynamicProvider
    {
        public string Id { get; set; }
        public string AssemblyName { get; set; }
        public string ClassName { get; set; }
        public string ConnectionInfo { get; set; }
    }
}
