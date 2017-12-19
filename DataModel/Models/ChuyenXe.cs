using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class ChuyenXe
    {
        public ChuyenXe()
        {
            DatVes = new HashSet<DatVe>();
        }

        [Key]
        public int MaChuyenXe { get; set; }

        public int MaTuyenXe { get; set; }

        public string TenChuyenXe { get; set; }

        public DateTime NgayGioChay { get; set; }

        public int TaiXe { get; set; }

        public virtual ICollection<DatVe> DatVes { get; set; }

        public virtual TuyenXe TuyenXe { get; set; }

    }
}
