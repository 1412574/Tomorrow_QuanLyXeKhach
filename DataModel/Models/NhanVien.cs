namespace DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NhanVien")]
    public partial class NhanVien
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Display(Name = "Mã nhân viên", ShortName = "Mã")]
        public int maNV { get; set; }

        [StringLength(30)]
        [CustomRequiredValidator]
        [Display(Name = "Họ và tên", ShortName = "Họ tên")]
        public string hoTen { get; set; }

        [StringLength(12)]
        [Display(Name = "CMND/CCCD", ShortName = "CMND/CCCD")]
        public string cCCD { get; set; }

        [StringLength(30)]
        [Display(Name = "Bằng cấp", ShortName = "Bằng cấp")]
        public string bangCap { get; set; }

        [StringLength(12)]
        [Display(Name = "Số điện thoại", ShortName = "SDT")]
        public string sDT { get; set; }

        [StringLength(100)]
        [Display(Name = "Địa chỉ", ShortName = "Địa chỉ")]
        public string diaChi { get; set; }

        [Display(Name = "Lương căn bản", ShortName = "Lương")]
        public float luongCanBan { get; set; }

        [Display(Name = "Vai trò", ShortName = "Vai trò")]
        public int maVT { get; set; }

        public virtual VaiTro VaiTro { get; set; }

        [Display(Name = "Trạng thái", ShortName = "Trạng thái")]
        public int maTT { get; set; }

        public virtual TrangThaiNV TrangThai { get; set; }

        public virtual ICollection<TaiKhoanNV> TaiKhoanNVs { get; set; }

    }
}
