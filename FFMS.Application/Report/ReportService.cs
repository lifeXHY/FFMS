using System.Linq;
using FFMS.Application.Common;
using FFMS.Application.Report.Dto;
using FFMS.EntityFrameWorkCore.Entitys;
using Wei.Repository;

namespace FFMS.Application.Report
{
    public class ReportService : IReportService
    {
        #region 构造函数注入
        private readonly IRepository<AccountBill> _accountbillrepository;

        public ReportService(IRepository<AccountBill> accountbillrepository)
        {
            _accountbillrepository = accountbillrepository;
        }
        #endregion

        /// <summary>
        /// 获取收支明细
        /// </summary>
        /// <returns></returns>
        public IQueryable<Report_DetailsDto> GetDetailsViewData(SearchReport_DetailsDto input)
        {
            var billQuey = _accountbillrepository.QueryNoTracking()
                .WhereIf(input.BegDate.HasValue, x => x.AccountDate >= input.BegDate.Value)
                .WhereIf(input.EndDate.HasValue, x => x.AccountDate <= input.EndDate.Value.AddDays(1).AddSeconds(-1))
                .WhereIf(input.UserID.HasValue, x => x.CreateUserID == input.UserID.Value)
                .Where(x => !x.IsDelete)
                ;
            var result = from a in billQuey
                         select new Report_DetailsDto()
                         {
                             BillType = a.BillType == BillTypeEnum.Income ? "收入" : "支出",
                             ItemType = a.ItemType,
                             AccountMoney = a.AccountMoney,
                             AccountDate = a.AccountDate,
                             CreateDisPlayName = a.CreateDisPlayName
                         };
            return result.OrderBy(x => x.BillType);
        }
    }
}
