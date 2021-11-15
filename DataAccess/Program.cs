using System;
using Dapper;
using DataAccess.Models;
using Microsoft.Data.SqlClient;

namespace DataAccess
{
    class Program
    {
        static void Main(string[] args)
        {
            const string connectionString = "Server=localhost,1433;Database=balta;User ID=sa;Password=1q2w3e4r@#$";
            Guid id = new Guid("e2512250-9fec-4bb9-b827-65c17f82ef99");
            var title = "Segurança da Informação 21";

            using (var connection = new SqlConnection(connectionString))
            {
                //UpdateCategory(connection, id, title);
                DeleteCategory(connection, id);
                ListCategories(connection);
            }
        }
        static void ListCategories(SqlConnection connection)
        {
            var categories = connection.Query<Category>("SELECT [Id], [Title] FROM [Category]");
            foreach (var item in categories)
            {
                Console.WriteLine($"ID = {item.Id}, CURSO = {item.Title}");
            }
        }

        static void InsertCategory(SqlConnection connection,
            string title, string url,
            string summary, int order,
            string description, bool featured
        )
        {
            var category = new Category();
            category.Id = Guid.NewGuid();
            category.Title = title;
            category.Url = url;
            category.Summary = summary;
            category.Order = 8;
            category.Description = description;
            category.Featured = featured;

            var insertSql = (@"INSERT INTO 
                                   [Category]
                               VALUES(
                                   @Id,
                                   @Title,
                                   @Url,
                                   @Summary,
                                   @Order,
                                   @Description,
                                   @Featured)");

            var rows = connection.Execute(insertSql, new
            {
                category.Id,
                category.Title,
                category.Url,
                category.Summary,
                category.Order,
                category.Description,
                category.Featured
            });
            Console.WriteLine($"{rows} alterada(s)");
        }
        static void UpdateCategory(SqlConnection connection, Guid id, string title)
        {
            var updateSql = (@"UPDATE
                                [Category]
                            SET
                                [Title] = @Title
                            WHERE
                                [Id] = @Id");

            var rows = connection.Execute(updateSql, new
            {
                Id = id,
                Title = title
            });
            Console.WriteLine($"{rows} Registro(s) Alterado(s)");
        }
        static void DeleteCategory(SqlConnection connection, Guid id)
        {
            var deleteSql = (@"DELETE FROM
                                    [Category]
                                WHERE [Id] = @Id");
            var rows = connection.Execute(deleteSql, new
            {
                Id = id
            });
            Console.WriteLine($"{rows} registro(s) apagado(s)");
        }
    }
}
