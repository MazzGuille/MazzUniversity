using System.ComponentModel.DataAnnotations;

namespace UniversityApi.Models.DataModels
{
    public class UserLogings
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
