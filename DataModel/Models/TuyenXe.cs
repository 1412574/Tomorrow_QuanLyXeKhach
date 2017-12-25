using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    [Table("TuyenXe")]
    public class TuyenXe
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int MaTuyenXe { get; set; }

        [StringLength(50)]
        [Display(Name = "Tên tuyến xe")]
        public string TenTuyenXe { get; set; }

        [Display(Name = "Giá vé")]
        public int GiaVe { get; set; }

        [StringLength(50)]
        [Display(Name = "Loại xe")]
        public string LoaiXe { get; set; }

        [Display(Name = "Thời gian (giờ)")]
        public int ThoiGian { get; set; }

        [Display(Name = "Quãng đường (km)")]
        public int QuangDuong { get; set; }

        [Display(Name = "Số chuyến /ngày")]
        public int SoChuyen { get; set; }

        public virtual ICollection<ChuyenXe> ChuyenXes { get; set; }
        public virtual ICollection<HanhTrinh> HanhTrinhs { get; set; }

        
    }
}
