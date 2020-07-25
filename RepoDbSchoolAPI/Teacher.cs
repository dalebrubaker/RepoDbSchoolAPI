using System.Collections.Generic;

namespace RepoDbSchoolAPI
{
    public class Teacher
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public IEnumerable<Student> Students { get; set; }
    }
}