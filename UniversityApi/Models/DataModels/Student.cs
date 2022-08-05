using System.ComponentModel.DataAnnotations;

namespace UniversityApi.Models.DataModels
{
    public class Student : BaseEntity
    {
        [Required]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        public string LastName { get; set; } = string.Empty;
        [Required]
        public DateTime Dob { get; set; } = DateTime.Now;

        public ICollection<Course> Courses { get; set; } = new List<Course>();
    }
}
