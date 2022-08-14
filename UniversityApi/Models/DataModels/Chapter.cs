using System.ComponentModel.DataAnnotations;

namespace UniversityApi.Models.DataModels
{
    public class Chapter : BaseEntity
    {
        public int CourseId { get; set; }

        public virtual Course Course { get; set; } = new Course();

        [Required, StringLength(50)]
        public string List = String.Empty;
    }
}
