using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace DataModel
{
    [Table("LichPhongVan")]
    public class LichPhongVan
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Mã lịch phỏng vấn", ShortName = "Mã")]
        public int maLPV { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Ngày phỏng vấn", ShortName = "Ngày")]
        public DateTime ngay { get; set; }

        [Display(Name = "Địa điểm", ShortName = "Địa điểm")]
        public string diaDiem { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Tiêu chí", ShortName = "Tiêu chí")]
        public string tieuChi { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Ghi chú", ShortName = "Ghi chú")]
        public string ghiChu { get; set; }

        public virtual ICollection<UngVien> UngViens { get; set; }

        public string shortString
        {
            get
            {
                return this.maLPV + " - " + this.ngay.ToShortDateString();
            }
        }

    }
}
