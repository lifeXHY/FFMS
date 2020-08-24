using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Wei.Repository;

namespace FFMS.EntityFrameWorkCore.Entitys
{
    public class AccountBill:Entity
    {
        [Column(@"AccountMoney", TypeName = "decimal(18,3)")]
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
