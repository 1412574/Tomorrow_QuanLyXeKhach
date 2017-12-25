namespace DataModel
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("BaoCao")]
    public partial class BaoCao
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int maBienBan { get; set; }

        [StringLength(50)]
        public string tenBienBan { get; set; }

        [StringLength(200)]
        [DataType(DataType.MultilineText)]
        public string noiDung { get; set; }

        public DateTime ngayLapBaoCao { get; set; }


        public DateTime ngaySuaDoi { get; set; }


        public int lanSuaDoi { get; set; }

        [StringLength(200)]
        public string ghiChu { get; set; }

        public BaoCao (int maBienBan, string tenBienBan, string noiDung,
            DateTime ngayLap, DateTime ngaySua, int lanSuaDoi, string ghiChu)
        {
            this.maBienBan = maBienBan;
            this.tenBienBan = tenBienBan;
            this.noiDung = noiDung;
            this.ngayLapBaoCao = ngayLap;
            this.ngaySuaDoi = ngaySua;
            this.lanSuaDoi = lanSuaDoi;
            this.ghiChu = ghiChu;
        }
        public BaoCao() { }
    }
}
