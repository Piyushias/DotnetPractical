using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotnetPractical.Models
{
	public class Student
	{
        [Key]
        public int Student_Id { get; set; }
        [Required]
        public string Student_Name { get; set; }
        [Required]
        public int Student_Age { get; set; }
        public int Standard { get; set; }
        public byte[]? Image { get; set; }

        [NotMapped]
        public IFormFile? FormFilePic { get; set; }
        public string? SubjectsId { get; set; }
        [NotMapped]
        public List<int>? Subjects { get; set; }
        [NotMapped]
        public List<Subject>? Subject { get; set; }

        [Required]
        public int Stuent_RollNo { get; set; }
    }

    public class Detaials
    {
        
    }

}
