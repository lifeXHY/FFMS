using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace FFMS.EntityFrameWorkCore.Extends
{
    public static class IQueryableExtends
    {
        public static async Task<PageData<T>> GetPageQueryAsync<T>(this IQueryable<T> query,int? page, int? limit)
        {
            PageData<T> pageData = new PageData<T>
            {
                Totals = await query.CountAsync(),
                Rows = await query.Skip((page.Value - 1) * limit.Value).Take(limit.Value).ToListAsync()
            };
            return pageData;
        }
    }
}
