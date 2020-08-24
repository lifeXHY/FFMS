using System.ComponentModel.DataAnnotations;

namespace FFMS.Application.User.Dto
{
    public class ChangePasswordDto
    {
        public int Id { get; set; }

        [StringLength(128)]
        public string OldPassWord { get; set; }

        [StringLength(128)]
        public string NewPassWord { get; set; }
    }
}
