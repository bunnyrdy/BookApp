using System.ComponentModel.DataAnnotations;
namespace Web.Models.Models
{
    public class Category
    {
        [Key]
        public int Category_Id{ get; set; }
        [Required]
        [MaxLength(50)]
        public string? Name { get; set; }
        public int DisplayOrder { get; set; }
    }
}