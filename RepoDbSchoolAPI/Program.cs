using System;
using System.Collections.Generic;
using System.Data.SQLite;
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
            CreateTestDatabase();
            SqLiteBootstrap.Initialize();
        }

        private static void CreateTestDatabase()
        {
            var exists = File.Exists(DbFileName);
            if (exists)
            {
                return;
            }
            using var connection = new SQLiteConnection(_connectionString);
            connection.Open();
            connection.ExecuteNonQuery(
                @"
                        CREATE TABLE [Teacher]
                        (
                            [Id] INT IDENTITY(1,1)
                            , [Name] NVARCHAR(128) NOT NULL
                            , CONSTRAINT [PK_Teacher] PRIMARY KEY ([Id] ASC )
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
