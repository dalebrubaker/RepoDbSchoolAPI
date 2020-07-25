using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepoDb;

namespace RepoDbSchoolAPI
{
    class Program
    {
        private const string DbFileName = @".\Test.sqlite";
        private static string _connectionString;

        static void Main(string[] args)
        {
            _connectionString = $"Data Source = {DbFileName}";
            SqLiteBootstrap.Initialize();
            ITeacherRepository teacherRepository = new TeacherRepository(_connectionString);
            IStudentRepository studentRepository = new StudentRepository(_connectionString);
            var controller = new StudentController(studentRepository, teacherRepository);
            var exists = File.Exists(DbFileName);
            if (!exists)
            {
                CreateTestDatabase();
                var generate = controller.Generate();
                Console.WriteLine($"Generated {generate.Inserted:N0} students in {generate.ElapsedInMilliseconds:N1} msec");
            }
            var queryStat = controller.GetQueryStat();
            Console.WriteLine($"QueryStat: {queryStat.Iteration:N0} students in {queryStat.ElapsedInMilliseconds:N1} msec");
            var queryStatCache = controller.GetQueryStatCache();
            Console.WriteLine($"QueryStat: {queryStatCache.Iteration:N0} students in {queryStatCache.ElapsedInMilliseconds:N1} msec");
            Console.ReadKey();
        }

        private static void CreateTestDatabase()
        {
            using var connection = new SQLiteConnection(_connectionString);
            connection.Open();
            connection.ExecuteNonQuery(
                @"
                        CREATE TABLE [Teacher]
                        (
                            [Id] INTEGER
                            , [Name] NVARCHAR(128) NOT NULL
                            , CONSTRAINT [PK_Teacher] PRIMARY KEY ([Id] ASC AUTOINCREMENT)
                        )
                        ");
            connection.ExecuteNonQuery(
                @"
                        CREATE TABLE [Student]
                        (
                            [Id] INT IDENTITY(1,1)
                            , [TeacherId] INT 
                            , [Name] NVARCHAR(128) NOT NULL
                            , CONSTRAINT [PK_Student] PRIMARY KEY ([Id] ASC )
                        )
                        ");
            var existsAfter = File.Exists(DbFileName);
            connection.ExecuteNonQuery(
                @"
                        INSERT INTO [Teacher]
                        (
                            [Name]
                        )
                        VALUES
                        ('Lurlene Laury')
                        , ('Wynell Kort')
                        , ('Alexa Dempsey')
                        , ('Loan Goggins')
                        , ('Lien Lange')
                        , ('Veronika Hershey')
                        , ('Erasmo Milo')
                        , ('Columbus Hadden')
                        , ('Lena Cendejas')
                        , ('Shawana Bono')
                        , ('Morton Jourdan')
                        , ('Myesha Griffin')
                        , ('Cassi Pelayo')
                        , ('Shelly Chouinard')
                        , ('Gabrielle Cloninger')
                        , ('Brandee Rominger')
                        , ('Kimberly Blackmore')
                        , ('Efren Wey')
                        , ('Fabiola Douse')
                        , ('Heath Sessums');                        
            ");

        }
    }
}
