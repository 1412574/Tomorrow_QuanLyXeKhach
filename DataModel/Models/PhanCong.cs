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
        [Display(Name = "Mã ứng viên", ShortName = "Mã ứng viên")]
        public int maNV { get; set; }

        [CustomRequiredValidator]
        [Display(Name = "Mã công việc", ShortName = "Mã công việc")]
        public int maCV { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Ngày phân công", ShortName = "Ngày phân công")]
        public DateTime ngayPC { get; set; } = DateTime.Now;

        [Display(Name = "Đánh giá", ShortName = "Đánh giá")]
        public string danhGia { get; set; }

        [Display(Name = "Ghi chú", ShortName = "Ghi chú")]
        public string ghiChu { get; set; }

        public virtual CongViec CongViec { get; set; }

        public virtual NhanVien NhanVien { get; set; }
    }
}
