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
            Guid id = new Guid("09ce0b7b-cfca-497b-92c0-3290ad9d5142");
            var title = "Segurança da Informação 21";

            using (var connection = new SqlConnection(connectionString))
            {
                //UpdateCategory(connection, id, title);
                //DeleteCategory(connection, id);
                //InsertManyCategory(connection);
                //ExecuteProcedure(connection, id);
                //ExecuteReadProcedure(connection, id);
                //InsertExecuteScalar(connection);
                //ListCategories(connection);
                //ReadView(connection);
                OneToOne(connection);
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
        static void InsertExecuteScalar(SqlConnection connection)
        {
            var category = new Category();
            category.Title = "title";
            category.Url = "url";
            category.Summary = "summary";
            category.Order = 8;
            category.Description = "description";
            category.Featured = false;

            var insertSql = (@"INSERT INTO 
                                   [Category]
                               OUTPUT inserted.[Id]
                               VALUES(
                                   NEWID(),
                                   @Title,
                                   @Url,
                                   @Summary,
                                   @Order,
                                   @Description,
                                   @Featured)
                                SELECT SCOPE_IDENTITY()");

            var id = connection.ExecuteScalar<Guid>(insertSql, new
            {
                category.Title,
                category.Url,
                category.Summary,
                category.Order,
                category.Description,
                category.Featured
            });
            Console.WriteLine($"ID Gerado: {id}");
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
        static void InsertManyCategory(SqlConnection connection)
        {
            var category = new Category();
            category.Id = Guid.NewGuid();
            category.Title = "title";
            category.Url = "url";
            category.Summary = "summary";
            category.Order = 8;
            category.Description = "description";
            category.Featured = false;

            var category2 = new Category();
            category2.Id = Guid.NewGuid();
            category2.Title = "title2";
            category2.Url = "url2";
            category2.Summary = "summary";
            category2.Order = 9;
            category2.Description = "description2";
            category2.Featured = true;

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

            var rows = connection.Execute(insertSql, new[]
            {
                new
                {
                    category.Id,
                    category.Title,
                    category.Url,
                    category.Summary,
                    category.Order,
                    category.Description,
                    category.Featured
                },
                new
                {
                    category2.Id,
                    category2.Title,
                    category2.Url,
                    category2.Summary,
                    category2.Order,
                    category2.Description,
                    category2.Featured
                }
            });
            Console.WriteLine($"{rows} alterada(s)");
        }
        static void ExecuteProcedure(SqlConnection connection, Guid id)
        {
            //para executar a procudere, na query no C#, 
            //não precisa colocar o parametro, o dapper mapeia automaticamente
            var procudure = (@"[spDeleteStudent]");
            //outra forma que pode ser referenciado os valores dos parâmetros do comando de execução.
            var param = new { StudentId = id };
            var rows = connection.Execute(procudure, param,
                                commandType: System.Data.CommandType.StoredProcedure);
            Console.WriteLine("Procudure Exucutada.");
            Console.WriteLine($"{rows} linhas afetadas.");
        }
        static void ExecuteReadProcedure(SqlConnection connection, Guid id)
        {
            //para executar a procudere, na query no C#, 
            //não precisa colocar o parametro, o dapper mapeia automaticamente
            var procudure = ("[spCoursesByCategory]");
            //outra forma que pode ser referenciado os valores dos parâmetros do comando de execução.
            var param = new { CategoryId = id };
            var courses = connection.Query(procudure, param,
                                commandType: System.Data.CommandType.StoredProcedure);
            Console.WriteLine("Procudure Exucutada.");

            foreach (var course in courses)
            {
                Console.WriteLine($"{course.Title}");
            }
        }
        static void ReadView(SqlConnection connection)
        {
            var querySql = @"SELECT * FROM [vwCourses]";
            var courses = connection.Query(querySql);
            foreach (var item in courses)
            {
                Console.WriteLine($"Curso - {item.Title}");
            }
        }
        static void OneToOne(SqlConnection connection)
        {
            var querySql = @"
                SELECT
                    *
                FROM
                    [CareerItem]
                INNER JOIN
                    [Course]
                    ON [CareerItem].[CourseId] = [Course].[Id]";

            //como fazer uma junção de 2 objetos como igual o INNER JOIN
            //Quando a estrutura quando um objeto está como propriedade em outro
            //no momento de tipar, coloca quem é o princiapal, quem está dentro,
            //e em qual objeto irá o resultado. 
            //var items = connection.Query<CareerItem, Course, CareerItem>(querySql);

            var items = connection.Query<CareerItem, Course, CareerItem>(
                // toda vez que fizer esse estrutra, após a variavel com a query, 
                //precisamos explicar como será feito, como será carregado um objeto dentro do outro
                querySql,
                //dentro da função a declação de variavel é automática conforme a ordem dos objetos definidos na tipagem
                (careerItem, course) =>
                {
                    //e agora definimos que o Objeto dentro CareerItem, vai receber o Objeto que queremos unir.
                    careerItem.Course = course;
                    return careerItem;
                },
                //e agora, igual no resultado da query no banco, toda vez que acaba uma tabela inicia a outra,
                //então é necessário dizer quando termina um objeto e começa o outro.
                splitOn: "Id");

            foreach (var item in items)
            {
                Console.WriteLine($"{item.Course.Title}: \n{item.Title}\n---------------------------");
            }
        }
    }
}
