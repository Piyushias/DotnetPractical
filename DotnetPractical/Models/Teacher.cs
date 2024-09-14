using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotnetPractical.Models
{
	public class Teacher
	{
        [Key]
        public int Teacher_Id { get; set; }
        [Required]
        public String Teacher_Name { get; set; }
        [Required]
        public String Teacher_Age { get; set; }
        public byte[]? Image { get; set; }
        [Required]
        public String Teacher_Sex { get; set; }
        public string? SubjectIds { get; set; }
        [NotMapped]
        public List<int>? SubjectId { get; set; }


        [NotMapped]
        public IFormFile? FormFilePic { get; set; }

    }
}
