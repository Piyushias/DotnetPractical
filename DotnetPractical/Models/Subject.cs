using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotnetPractical.Models
{
	public class Subject
	{
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Standard { get; set; }
        public string? Language { get; set; } 

        [NotMapped]
        public List<string>? Languages { get; set; }
        [NotMapped]
        public List<Teacher>? Teachers { get; set; }
	}
}
