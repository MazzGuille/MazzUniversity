using System.ComponentModel.DataAnnotations;


namespace UniversityApi.Models.DataModels
{
    public class BaseEntity
    {
        [Required]
        [Key]
        public int Id { get; set; }
        public User CreatedBy { get; set; } = new User();
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public User UpdatedBy { get; set; } = new User();
        public DateTime? UpdatedAt { get; set; }
        public User DeletedBy { get; set; } = new User();
        public DateTime? DeletedAt { get; set; }
        public bool IsDeleted { get; set; }
    }
}
