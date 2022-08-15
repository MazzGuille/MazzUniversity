using System.Collections.Generic;
using UniversityApi.Models.DataModels;

namespace UniversityApi.Services
{
    public interface IStudentsService
    {
        IEnumerable<Student> GetStudentsWithCourses();
        IEnumerable<Student> GetStudentsWithNoCourses();
    }
}
