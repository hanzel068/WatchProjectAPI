using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace WatchProjectAPI.Model
{
    public class Watch
    {
        public Guid Id { get; set; }

        [Required]
        public string? Name { get; set; }

        public string? ImageUrl { get; set; }

        [Required]
        [Display(Name = "Short Description")]
        [StringLength(50, ErrorMessage = "Maximum of 50 character only")]
        public string? Short { get; set; }

        [Required]
        [Display(Name = "Full Description")]
        public string? FullDescription { get; set; }

        [Required, Range(1, 9999)]
        //[MaxLength(5)]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }
        [Required]
        public string? Caliber { get; set; }
        [Required]
        public string? Movement { get; set; }
        public string? Chronograph { get; set; }
        [Required, Range(1, 100)]
        [MaxLength(3)]
        public string? Weight { get; set; }
        [Required, Range(1, 100)]
        [MaxLength(3)]
        public string? Height { get; set; }
        [Required, Range(1, 100)]
        [MaxLength(3)]
        public string? Diameter { get; set; }
        [Required, Range(1, 100)]
        [MaxLength(3)]
        public string? Thickness { get; set; }
        [Required, Range(0, 100)]
        public string? Jewel { get; set; }
        [Required]
        [Display(Name = "Case Material")]
        public string? CaseMaterial { get; set; }
        [Required]
        [Display(Name = "Strap Material")]
        public string? StrapMaterial { get; set; }
       // [Required(ErrorMessage = "Please select Image")]
        [NotMapped]
        public byte[]? WatchImage { get; set; }
    }

  

}
