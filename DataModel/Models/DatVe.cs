namespace DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class DatVe
    {
        [Key]
        public int maDatVe { get; set; }

        public int maChuyenXe { get; set; }

        [StringLength(5)]
        public string soGhe { get; set; }

        public int giaVe { get; set; }

        [StringLength(20)]
        public string trangThai { get; set; }

        public int maKhachHang { get; set; }

        public virtual ChuyenXe ChuyenXe { get; set; }

        public virtual KhachHang KhachHang { get; set; }
    }
}
