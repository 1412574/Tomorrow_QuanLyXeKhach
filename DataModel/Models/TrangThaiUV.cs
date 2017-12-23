using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace DataModel
{
    [Table("TrangThaiUV")]
    public class TrangThaiUV
    {
        [Key]
        public int maTT { get; set; }

        [CustomRequiredValidator]
        public string tenTT { get; set; }

        public string moTaTT { get; set; }
    }
}
