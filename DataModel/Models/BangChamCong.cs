using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataModel
{
    public class BangChamCong
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Mã chấm công", ShortName = "Mã chấm công")]
        public int maCC { get; set; }

        [CustomRequiredValidator]
        [Display(Name = "Mã nhân viên", ShortName = "Mã nhân viên")]
        public int maNV { get; set; }

        [CustomRequiredValidator]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Ngày chấm công", ShortName = "Ngày chấm công")]
        public DateTime ngay { get; set; }

        [CustomRequiredValidator]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
        [Display(Name = "Giờ bắt đầu", ShortName = "Giờ bắt đầu")]
        public DateTime gioBatDau { get; set; }

        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
        [Display(Name = "Giờ kết thúc", ShortName = "Giờ kết thúc")]
        public DateTime gioKetThuc { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Ghi chú", ShortName = "Ghi chú")]
        public string ghiChu { get; set; }

        [Display(Name = "Nhân viên", ShortName = "Nhân viên")]
        public virtual NhanVien NhanVien { get; set; }

    }
}
