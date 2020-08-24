using System.ComponentModel.DataAnnotations;

namespace FFMS.Application.Items.Dto
{
    public class BasItemsDto
    {
        [Required]
        [StringLength(100)]
        public string ItemType { get; set; }

        [StringLength(500)]
        public string Memo { get; set; }

        public int CreateUserID { get; set; }

        [StringLength(50)]
        public string CreateDisPlayName { get; set; }
    }
}
