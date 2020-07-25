using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using RepoDb;
using SchoolAPI.Models;

namespace RepoDbSchoolAPI
{
    public class TeacherRepository : BaseRepository<Teacher, SqlConnection>, ITeacherRepository
    {
        public TeacherRepository(string connectionString) : base(connectionString)
            //: base(@"Server=.;Database=SchoolDB;Integrated Security=SSPI;")
        { }


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
