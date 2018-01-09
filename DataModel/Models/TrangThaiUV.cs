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
        [Display(Name = "Mã trạng thái", ShortName = "Mã trạng thái")]
        public int maTT { get; set; }

        [CustomRequiredValidator]
        [Display(Name = "Tên trạng thái", ShortName = "Tên trạng thái")]
        public string tenTT { get; set; }

        [Display(Name = "Mô tả trạng thái", ShortName = "Mô tả trạng thái")]
        public string moTaTT { get; set; }
    }
}
