using System;
using System.Collections.Generic;
using System.Text;

namespace FFMS.Application.Bill.Dto
{
    public class SearchAccountBillDto
    {
        //只能查询自身的数据
        public int UserID { get; set; }

        /// <summary>
        /// 收支项目
        /// </summary>
        public string ItemType { get; set; }

        public DateTime? BegDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
