using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using FFMS.EntityFrameWorkCore.Entitys;

namespace FFMS.Application.Bill.Dto
{
    public class AccountBillDto
    {
        [Required]
        public decimal AccountMoney { get; set; }

        //[StringLength(500)]
        //[Required]
        //public string AccountDetails { get; set; }

        [Required]
        public DateTime AccountDate { get; set; }
        public int CreateUserID { get; set; }

        [StringLength(50)]
        public string CreateDisPlayName { get; set; }
        public int ItemsId { get; set; }

        [Required]
        [StringLength(100)]
        public string ItemType { get; set; }

        [Required]
        public BillTypeEnum BillType { get; set; }
    }
}
