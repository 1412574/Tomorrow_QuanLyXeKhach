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
        public int maNV { get; set; }

        [StringLength(30)]
        public string hoTen { get; set; }

        [StringLength(12)]
        public string cCCD { get; set; }

        [StringLength(30)]
        public string bangCap { get; set; }

        [StringLength(12)]
        public string sDT { get; set; }

        [StringLength(100)]
        public string diaChi { get; set; }

        public float luongCanBan { get; set; }

        public int maVT { get; set; }

        public virtual VaiTro VaiTro { get; set; }

        public int maTT { get; set; }

        public virtual TrangThaiNV TrangThai { get; set; }

        public virtual ICollection<TaiKhoanNV> TaiKhoanNVs { get; set; }

    }
}
