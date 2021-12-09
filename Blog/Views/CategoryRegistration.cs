using Blog.Repositories;
using Blog.Models;
using System;
using System.Data.SqlClient;

namespace Blog.Views
{
    class CategoryRegistration
    {
        private const string connectionString = @"Server=localhost,1433;Database=Blog;User ID = sa; Password=1q2w3e4r@#$";
        public static void Show()
        {
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.ForegroundColor = ConsoleColor.Black;
            var connection = new SqlConnection(connectionString);

            Console.Clear();
            Console.WriteLine("--------------------CATEGORY--------------------\n\n\n");

            Console.WriteLine("\t1 - Criar um novo categoria");
            Console.WriteLine("\t2 - Listar categorias");
            Console.WriteLine("\t3 - Atualizar um categoria");
            Console.WriteLine("\t4 - Apagar um category");
            Console.WriteLine("\t0 - Voltar");
            short choise = short.Parse(Console.ReadLine());

            switch (choise)
            {
                case 0: MenuMain.Show(true); break;
                case 1: InsertCategory(connection); break;
                case 2: ReadCategorys(connection); break;
                case 3: UpdateCategory(connection); break;
                case 4: InsertCategory(connection); break;
                default: CategoryRegistration.Show(); break;
            }
            //InsertCategory(connection);
            

            MenuMain.Show(true);
        }

        public static void ReadCategorys(SqlConnection connection)
        {
            var repository = new Repository<Models.Category>(connection);
            var categorys = repository.Read();
            foreach(var category in categorys)
            {
                Console.WriteLine($"\t\tId: {category.Id}");
                Console.WriteLine($"\t\tNome: {category.Name}");
                Console.WriteLine($"\t\tURL: {category.Slug}\n");
            }
            Console.ReadKey();
            CategoryRegistration.Show();
        }

        public static void InsertCategory(SqlConnection connection)
        {
            var repository = new Repository<Models.Category>(connection);
            var category = CreateCategory();

            connection.Open();
            repository.Create(category);
            connection.Close();

            Console.WriteLine("Categoria criado com sucesso!");
            Console.ReadKey();
            CategoryRegistration.Show();
        }

        public static void UpdateCategory(SqlConnection connection)
        {
            var repository = new Repository<Models.Category>(connection);
            var category = CreateCategory();

            Console.WriteLine("Insira o Id do categoria a ser atualizado");
            category.Id = short.Parse(Console.ReadLine());

            connection.Open();
            repository.Update(category);
            connection.Close();

            Console.WriteLine("Categoria Atualizado com sucesso!");
            Console.ReadKey();
            CategoryRegistration.Show();
        }

        public static void DeleteCategory(SqlConnection connection)
        {
            var repository = new Repository<Models.Category>(connection);
            
            Console.WriteLine("Insira o Id do categoria a ser apagado");
            var id = short.Parse(Console.ReadLine());

            repository.Delete(id);
            connection.Close();

            Console.WriteLine("Categoria Deletado com sucesso!");
            Console.ReadKey();
            CategoryRegistration.Show();
        }


        public static Category CreateCategory()
        {
            var category = new Category();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Informações do Categoria");

                Console.Write("\tNome: ");
                category.Name = Console.ReadLine();

                Console.Write("\tUrl dos Categorias: ");
                category.Slug = Console.ReadLine();

                Console.Clear();
                Console.WriteLine("\tConfirmar? 1 - SIM");

                Console.WriteLine($"\t\tNome: {category.Name}");
                Console.WriteLine($"\t\tLink de usuário: {category.Slug}");

                var choise = Console.ReadLine();
                if (choise == "1")
                    break;
            }
            return category;
        }
    }
}
