using System.Collections.Generic;

namespace RepoDbSchoolAPI
{
    public interface ITeacherRepository
    {
        IEnumerable<Teacher> GetAllTeachers();
        Teacher GetTeacher(int teacherId);
        Teacher GetTeacherCache(int teacherId);
    }
}
