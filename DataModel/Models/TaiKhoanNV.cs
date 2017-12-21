namespace DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TaiKhoanNV")]
    public partial class TaiKhoanNV
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int maTK { get; set; }

        [StringLength(40)]
        public string matKhau { get; set; }

        public int maNV { get; set; }

        public virtual NhanVien NhanVien { get; set; }
    }
}
