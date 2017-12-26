using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class HanhTrinh
    {
        [Key]
        public int MaHanhTrinh { get; set; }

        public int MaTuyenXe { get; set; }
        
        public int MaTramXe { get; set; }
        
        public int ThuTu { get; set; }

        public virtual TuyenXe TuyenXe { get; set; }

        public virtual TramXe TramXe { get; set; }
    }
}
