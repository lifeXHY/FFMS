using System;

namespace FFMS.Application.Report.Dto
{
    public class Report_DetailsDto
    {
        public string BillType { get; set; }
        public string ItemType { get; set; }
        public decimal AccountMoney { get; set; }
        public DateTime AccountDate { get; set; }
        public string CreateDisPlayName { get; set; }
    }
}
