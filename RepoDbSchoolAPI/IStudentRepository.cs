using System.Collections.Generic;
using SchoolAPI.Models;

namespace RepoDbSchoolAPI
{
    public interface IStudentRepository
    {
        IEnumerable<Student> GetAllStudents();
        int Save(Student student);
        int SaveAll(IList<Student> students);
    }
}
