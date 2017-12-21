namespace DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("VaiTro")]
    public partial class VaiTro
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int maVT { get; set; }

        [StringLength(50)]
        public string tenVT { get; set; }

        [StringLength(100)]
        public string moTaVT { get; set; }

        public virtual ICollection<NhanVien> NhanViens { get; set; }
    }
}
