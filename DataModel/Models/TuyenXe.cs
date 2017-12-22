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
        [Key]
        public int MaTuyenXe { get; set; }
        public string TenTuyenXe { get; set; }
        public virtual ICollection<ChuyenXe> ChuyenXes { get; set; }
    }
}
