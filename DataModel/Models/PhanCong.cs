using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataModel
{
    public class PhanCong
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Mã phân công", ShortName = "Mã phân công")]
        public int maPC { get; set; }

        [CustomRequiredValidator]
        [Display(Name = "Mã nhân viên", ShortName = "Mã nhân viên")]
        public int maNV { get; set; }

        [CustomRequiredValidator]
        [Display(Name = "Mã công việc", ShortName = "Mã công việc")]
        public int maCV { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Ngày phân công", ShortName = "Ngày phân công")]
        public DateTime ngayPC { get; set; } = DateTime.Now;

        [Display(Name = "Đánh giá", ShortName = "Đánh giá")]
        public string danhGia { get; set; }

        [Display(Name = "Nhiệm vụ", ShortName = "Nhiệm vụ")]
        public string nhiemVu { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Ghi chú", ShortName = "Ghi chú")]
        public string ghiChu { get; set; }

        [Display(Name = "Công việc", ShortName = "Công việc")]
        public virtual CongViec CongViec { get; set; }

        [Display(Name = "Nhân viên", ShortName = "Nhân viên")]
        public virtual NhanVien NhanVien { get; set; }
    }
}
