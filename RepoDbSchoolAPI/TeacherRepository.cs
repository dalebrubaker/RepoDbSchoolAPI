using System;
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

        public IEnumerable<Teacher> GetTeachersWithStudents(IEnumerable<int> ids)
        {
            var (item1, item2) = DbRepository.QueryMultiple<Teacher, Student>(teacher => ids.Contains(teacher.Id),
                student => ids.Contains(student.TeacherId));
            var teachers = item1.ToList();
            teachers.ForEach(teacher => teacher.Students = item2.Where(student => student.TeacherId == teacher.Id));
            return teachers;
        }
    }
}