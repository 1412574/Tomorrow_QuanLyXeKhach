namespace DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ThongKe")]
    public partial class ThongKe
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int maBienBan { get; set; }

        [StringLength(50)]
        public string tenBienBan { get; set; }

        [StringLength(200)]
        public string noiDung { get; set; }

        public DateTime ngayLapThongKe { get; set; }

        
        public DateTime ngaySuaDoi { get; set; }

        
        public int lanSuaDoi { get; set; }

        [StringLength(200)]
        public string ghiChu { get; set; }

        public ThongKe(int maBienBan, string tenBienBan, string noiDung,
            DateTime ngayLap, DateTime ngaySua, int lanSuaDoi, string ghiChu)
        {
            this.maBienBan = maBienBan;
            this.tenBienBan = tenBienBan;
            this.noiDung = noiDung;
            this.ngayLapThongKe = ngayLap;
            this.ngaySuaDoi = ngaySua;
            this.lanSuaDoi = lanSuaDoi;
            this.ghiChu = ghiChu;
        }
        public ThongKe() { }
    }
}
