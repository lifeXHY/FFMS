using FFMS.EntityFrameWorkCore.Entitys;
using Microsoft.EntityFrameworkCore;
using Wei.Repository;

namespace FFMS.EntityFrameWorkCore
{
    public class FFMSDbContext : BaseDbContext
    {
        public FFMSDbContext(DbContextOptions<FFMSDbContext> options) : base(options)
        {
        }
        //public virtual DbSet<InfoLog> InfoLog { get; set; }
        public virtual DbSet<BasUser> BasUser { get; set; }
        public virtual DbSet<BasItems> BasItems { get; set; }

    }
}
