using System;
using System.Collections.Generic;
using System.Linq;
using RepoDb.Extensions;

namespace RepoDbSchoolAPI
{
    public class StudentController
    {
        private readonly IStudentRepository _studentRepository;
        private readonly ITeacherRepository _teacherRepository;

        public StudentController(IStudentRepository studentRepository,
            ITeacherRepository teacherRepository)
        {
            _studentRepository = studentRepository;
            _teacherRepository = teacherRepository;
        }

        public IEnumerable<Student> Get()
        {
            return _studentRepository.GetAllStudents().AsList();
        }

        public dynamic GetQueryStat()
        {
            var students = _studentRepository.GetAllStudents();
            var iteration = 0;
            var dateTime = DateTime.UtcNow;
            foreach (var student in students)
            {
                var teacher = _teacherRepository.GetTeacher(student.TeacherId);
                if (teacher != null)
                {
                    iteration++;
                }
            }
            var elapsed = (DateTime.UtcNow - dateTime).TotalMilliseconds;
            return new
            {
                Iteration = iteration,
                ElapsedInMilliseconds = elapsed
            };
        }

        public dynamic GetQueryStatCache()
        {
            var students = _studentRepository.GetAllStudents();
            var iteration = 0;
            var dateTime = DateTime.UtcNow;
            foreach (var student in students)
            {
                var teacher = _teacherRepository.GetTeacherCache(student.TeacherId);
                if (teacher != null)
                {
                    iteration++;
                }
            }
            var elapsed = (DateTime.UtcNow - dateTime).TotalMilliseconds;
            return new
            {
                Iteration = iteration,
                ElapsedInMilliseconds = elapsed
            };
        }

        public dynamic Generate(int count = 10000)
        {
            var teachers = _teacherRepository
                .GetAllTeachers()
                .AsList();
            var random = new Random();
            var students = new List<Student>();
            var dateTime = DateTime.UtcNow;

            for (var i = 0; i < count; i++)
            {
                var teacher = teachers.ElementAt(random.Next(0, teachers.Count - 1));
                var student = new Student
                {
                    Id = i,
                    Name = $"Student-{Guid.NewGuid().ToString()}",
                    TeacherId = teacher.Id
                };
                students.Add(student);
            }

            var insertedResult = _studentRepository.SaveAll(students);
            var elapsed = (DateTime.UtcNow - dateTime).TotalMilliseconds;

            return new
            {
                Inserted = insertedResult,
                ElapsedInMilliseconds = elapsed
            };
        }
    }
}