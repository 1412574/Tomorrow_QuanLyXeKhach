

namespace DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ChiTietDatVe
    {
        [Key]
        public int maCTDV { get; set; }

        public int maDatVe { get; set; }

        public double? giaTien { get; set; }

        public int? soGhe { get; set; }

        public virtual DatVe DatVe { get; set; }
    }
}
