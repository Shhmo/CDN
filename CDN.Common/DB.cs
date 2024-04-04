using Microsoft.EntityFrameworkCore;
using CDN.Common;

namespace CDN.Common
{
    public partial class DB : DbContext
    {
        public DB(DbContextOptions
        <DB> options)
            : base(options)
        {
        }
        public virtual DbSet<FreeLancer> FreeLancers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FreeLancer>(entity => {
                entity.HasKey(k => k.ID);
            });
            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
