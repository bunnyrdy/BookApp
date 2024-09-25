using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Web.Models.Models
{
    public class    Product
    {
        [Key]
        public int Product_Id{get;set;}

        [Required]
        public string? Title {get;set;}

        public string? Description {get;set;}

        [Required]
        public string? ISBN {get;set;}

        [Required]

        public string? Author {get;set;}

        [Required]
        [Display(Name = "List Price")]
        [Range(1,1000)]
        public double ListPrice{get;set;}

        // [Required]
        // [Display(Name = "price for 1-50")]
        // [Range(1,1000)]
        [ValidateNever]
        public double Price{get;set;}

        [Required]
        [Display(Name = "Price for 50+")]
        [Range(1,1000)]
        public double Price50{get;set;}

        [Required]
        [Display(Name = "Price for 100+")]
        [Range(1,1000)]
        public double Price100{get;set;}

        public int CategoryId {  get ; set;}
        [ForeignKey("CategoryId")]

        [ValidateNever]
        public Category Category { get; set; }
        [ValidateNever]
        public string Imageurl{get;set;}
        
    }
}