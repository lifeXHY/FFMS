using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace FFMS.EntityFrameWorkCore
{
    public class DesignTimeDbContextFactory:IDesignTimeDbContextFactory<DbContext>
    {
        public DbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DbContext>();
            optionsBuilder.UseSqlServer($"tmp.db");

            return new DbContext(optionsBuilder.Options);
        }
    }
}
