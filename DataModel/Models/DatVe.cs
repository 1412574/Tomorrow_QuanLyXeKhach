namespace DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class DatVe
    {
        public DatVe()
        {
            ChiTietDatVes = new HashSet<ChiTietDatVe>();
        }

        [Key]
        public int maDatVe { get; set; }

        public int? maChuyenXe { get; set; }

        public int? maKhachHang { get; set; }

        public double? tongTien { get; set; }

        public DateTime? ngayDat { get; set; }

        [StringLength(20)]
        public string trangThai { get; set; }

        public virtual ICollection<ChiTietDatVe> ChiTietDatVes { get; set; }

        public virtual ChuyenXe ChuyenXe { get; set; }

        public virtual KhachHang KhachHang { get; set; }
    }
}
