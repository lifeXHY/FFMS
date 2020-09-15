using System.Linq;
using FFMS.Application.AutoRegisterService;
using FFMS.Application.Report.Dto;

namespace FFMS.Application.Report
{
    public interface IReportService: IDenpendency
    {
        IQueryable<Report_DetailsDto> GetDetailsViewData(SearchReport_DetailsDto input);
    }
}
