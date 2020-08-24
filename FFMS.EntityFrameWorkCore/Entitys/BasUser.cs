using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Wei.Repository;

namespace FFMS.EntityFrameWorkCore.Entitys
{
    public class BasUser:Entity
    {
        [StringLength(50)]
        [Required]
        [Column(@"UserName", TypeName = "varchar(50)")]
        public string UserName { get; set; }

        [StringLength(128)]
        [Required]
        [Column(@"PassWord", TypeName = "varchar(128)")]
        public string PassWord { get; set; }

        [StringLength(50)]
        [Required]
        [Column(@"DisPlayName", TypeName = "varchar(50)")]
        public string DisPlayName { get; set; }

        [Column(@"IfAdmin", TypeName = "bit")]
        public bool IfAdmin { get; set; }

        [Column(@"IsActive", TypeName = "bit")]
        public bool IsActive { get; set; }

        [Column(@"Sex", TypeName = "char(1)")]
        [StringLength(1)]
        [Required]
        public string Sex { get; set; }
    }
}
