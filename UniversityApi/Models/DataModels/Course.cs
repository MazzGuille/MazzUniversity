﻿using System.ComponentModel.DataAnnotations;

namespace UniversityApi.Models.DataModels
{

    public enum Level
    {
        Basic,
        Medium,
        Advanced,
        Expert
    }
    public class Course : BaseEntity
    {
        [Required, StringLength(50)]
        public string Name { get; set; } = String.Empty;

        [Required, StringLength(280)]
        public string ShortDescription { get; set; } = String.Empty;

        [Required]
        public string Description { get; set; } = String.Empty;

        public Level Level { get; set; } = Level.Basic;

        [Required]
        public ICollection<Category> Categories { get; set; } = new List<Category>();

        [Required]
        public ICollection<Student> Students { get; set; } = new List<Student>();

        [Required]
        public Chapter Chapter { get; set; } = new Chapter();

    }
}