using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using RepoDb;

namespace RepoDbSchoolAPI
{
    public class TeacherRepository : BaseRepository<Teacher, SQLiteConnection>, ITeacherRepository
    {
        public TeacherRepository(string connectionString) : base(connectionString)
        //: base(@"Server=.;Database=SchoolDB;Integrated Security=SSPI;")
        {
        }

        public IEnumerable<Teacher> GetAllTeachers()
        {
            return QueryAll();
        }

        public Teacher GetTeacher(int teacherId)
        {
            return Query(teacherId).FirstOrDefault();
        }

        public Teacher GetTeacherCache(int teacherId)
        {
            return Query(teacherId, cacheKey: $"Teacher{teacherId}").FirstOrDefault();
        }
    }
}