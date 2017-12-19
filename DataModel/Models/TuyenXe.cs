using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class TuyenXe
    {
        public TuyenXe()
        {
            ChuyenXes = new HashSet<ChuyenXe>();
        }
        [Key]
        public int MaTuyenXe { get; set; }
        public string TenTuyenXe { get; set; }
        public int GiaVe { get; set; }
        public virtual ICollection<ChuyenXe> ChuyenXes { get; set; }
    }
}
