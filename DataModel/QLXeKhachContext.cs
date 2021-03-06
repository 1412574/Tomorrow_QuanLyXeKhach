namespace DataModel
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class QLXeKhachContext : DbContext
    {
        public QLXeKhachContext()
            : base("name=Model12")
        {
        }

        public virtual DbSet<PhongBan> PhongBans { get; set; }
        public virtual DbSet<TuyenXe> TuyenXes { get; set; }
        public virtual DbSet<ChuyenXe> ChuyenXes { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
