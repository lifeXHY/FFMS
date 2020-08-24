using System.ComponentModel.DataAnnotations;

namespace FFMS.Application.User.Dto
{
    public class BasUserDto
    {
        [StringLength(50)]
        [MaxLength(50)]
        [Required]
        [Display(Name = "用户名")]
        public string UserName { get; set; }

        [StringLength(128)]
        [MaxLength(128)]
        [Required]
        [Display(Name = "密码")]
        public string PassWord { get; set; }

        [StringLength(50)]
        [MaxLength(50)]
        [Required]
        [Display(Name = "姓名")]
        public string DisPlayName { get; set; }

        public bool IfAdmin { get; set; }

        [Display(Name = "是否可用")]
        public bool IsActive { get; set; }

        [StringLength(1)]
        [Required]
        [Display(Name = "性别")]
        public string Sex { get; set; }
    }
}
