using System.ComponentModel.DataAnnotations;
using Wei.Repository;

namespace FFMS.EntityFrameWorkCore.Entitys
{
    public class BasItems:Entity
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
