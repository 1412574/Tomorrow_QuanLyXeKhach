namespace DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TrangThaiNV")]
    public partial class TrangThaiNV
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int maTT { get; set; }

        [StringLength(30)]
        public string tenTT { get; set; }

        [StringLength(100)]
        public string moTaTT { get; set; }

        public virtual ICollection<NhanVien> NhanViens { get; set; }
    }
}
