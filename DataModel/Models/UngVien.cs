using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace DataModel
{
    [Table("UngVien")]
    public class UngVien
    {
        [Key]
        [Display(Name = "Mã ứng viên", ShortName = "Mã số")]
        public int maUV { get; set; }

        [StringLength(30)]
        [CustomRequiredValidator]
        [Display(Name = "Họ và tên", ShortName = "Họ tên")]
        public string hoTen { get; set; }

        [StringLength(15)]
        [Display(Name = "Số điện thoại", ShortName = "SĐT")]
        public string sDT { get; set; }

        [StringLength(75)]
        [DataType(DataType.EmailAddress, ErrorMessage = "Email chưa đúng.")]
        [Display(Name = "Email", ShortName = "Email")]
        public string email { get; set; }

        [Display(Name = "Trạng thái", ShortName = "Trạng thái")]
        public int trangThai { get; set; } = 1;

        [Display(Name = "Lịch phỏng vấn", ShortName = "Lịch phỏng vấn")]
        public int? maLPV { get; set; }
        public virtual LichPhongVan LichPhongVan { get; set; }
        public virtual TrangThaiUV TrangThaiUV { get; set; }

    }
}
