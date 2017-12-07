namespace DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PhongBan")]
    public partial class PhongBan
    {
        
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int maPB { get; set; }

        [StringLength(10)]
        public string tenPB { get; set; }

        [StringLength(10)]
        public string moTaPB { get; set; }

        public PhongBan(int maPB, string tenPB, string moTaPB)
        {
            this.maPB = maPB;
            this.tenPB = tenPB;
            this.moTaPB = moTaPB;
        }
        public PhongBan() { }
    }
}
