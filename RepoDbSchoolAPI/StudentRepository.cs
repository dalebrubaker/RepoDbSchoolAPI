using System.Collections.Generic;
using System.Data.SQLite;
using RepoDb;

namespace RepoDbSchoolAPI
{
    public class StudentRepository : BaseRepository<Student, SQLiteConnection>, IStudentRepository
    {
        public StudentRepository(string connectionString) : base(connectionString)
        //: base(@"Server=.;Database=SchoolDB;Integrated Security=SSPI;")
        {
        }

        public IEnumerable<Student> GetAllStudents()
        {
            return QueryAll();
        }

        public int Save(Student student)
        {
            return Insert<int>(student);
        }

        public int SaveAll(IList<Student> students)
        {
            return 0;
            //return this.BulkInsert(students);
        }
    }
}