
namespace DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    public partial class KhachHang
    {
        public KhachHang()
        {
            DatVes = new HashSet<DatVe>();
        }

        [Key]
        public int maKhachHang { get; set; }

        [StringLength(100)]
        public string tenKhachHang { get; set; }

        [StringLength(15)]
        public string soDienThoai { get; set; }

        [StringLength(60)]
        public string taiKhoan { get; set; }

        public virtual ICollection<DatVe> DatVes { get; set; }
    }
}
