using Blog.Models;
using Blog.Repositories;
using System;
using System.Data.SqlClient;

namespace Blog.Views
{
    class PostRegistration
    {
        private const string connectionString = @"Server=localhost,1433;Database=Blog;User ID = sa; Password=1q2w3e4r@#$";
        public static void Show()
        {
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.ForegroundColor = ConsoleColor.Black;

            Console.Clear();
            Console.WriteLine("--------------------CADASTRAR NOVO POST--------------------\n\n\n");

            var connection = new SqlConnection(connectionString);
            InsertPost(connection);

            MenuMain.Show();
        }


        public static void InsertPost(SqlConnection connection)
        {
            var repository = new Repository<Post>(connection);

            var post = CreatePost();
            var dateTimeNow = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            post.CreateDate = DateTime.Parse(dateTimeNow);
            post.LastUpdateDate = DateTime.Parse(dateTimeNow);
            connection.Open();
            repository.Create(post);
            connection.Close();

            Console.WriteLine("Post criado com sucesso!");
            Console.ReadKey();

        }
        public static Post CreatePost()
        {
            var connection = new SqlConnection(connectionString);
            var repositoryCategory = new Repository<Category>(connection);
            var repositoryUser = new Repository<User>(connection);

            var post = new Post();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Informações do Post");

                Console.Write("\tTitulo: ");
                post.Title = Console.ReadLine();


                Console.Write("\tSumário: ");
                post.Summary = Console.ReadLine();


                Console.Write("\tTexto: ");
                post.Body = Console.ReadLine();


                Console.Write("\tUrl: ");
                post.Slug = Console.ReadLine();

                post.LastUpdateDate = DateTime.Now;

                Console.WriteLine("\tEscolha uma categoria: ");
                Console.WriteLine("\t\tGostaria de ver as categorias?  1 - SIM");
                var choiseCategory = Console.ReadLine();
                if(choiseCategory == "1")
                {
                    var categories = repositoryCategory.Read();
                    foreach(var category in categories)
                    {
                        Console.WriteLine($"{category.Name} => {category.Id}");
                    }
                }

                Console.Write("\tId de Categoria: ");
                post.CategoryId = int.Parse(Console.ReadLine());

                Console.Write("\tId do Autor ");
                if(Logon.Id != 0)
                    post.AuthorId = Logon.Id;
                else
                {
                    Console.WriteLine("\tEscolha o Id do Autor: ");
                    Console.WriteLine("\t\tGostaria de ver as categorias?  1 - SIM");
                    var choiseAuthor = Console.ReadLine();
                    if (choiseAuthor == "1")
                    {
                        var users = repositoryUser.Read();
                        foreach (var user in users)
                        {
                            Console.WriteLine($"{user.Name} => {user.Id}");
                        }
                    }
                    Console.Write("\tId do Autor: ");
                    post.AuthorId = int.Parse(Console.ReadLine());
                }

                Console.Clear();
                Console.WriteLine("\tConfirmar? 1 - SIM | 0 - Voltar ao Menu");

                Console.WriteLine($"\t\tTitulo: {post.Title}");
                Console.WriteLine($"\t\tSummario: {post.Summary}");
                Console.WriteLine($"\t\tTexto: {post.Body}");
                Console.WriteLine($"\t\tId de Categoria: {post.CategoryId}");
                Console.WriteLine($"\t\tId do Autor: {post.AuthorId}");

                var choise = Console.ReadLine();
                if (choise == "1")
                    break;
                else if(choise == "0")
                {
                    MenuMain.Show(true);
                }

            }
            
            return post;
        }
    }
}
