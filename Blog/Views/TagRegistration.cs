using Blog.Models;
using Blog.Repositories;
using System;
using System.Data.SqlClient;

namespace Blog.Views
{
    class TagRegistration
    {
        private const string connectionString = @"Server=localhost,1433;Database=Blog;User ID = sa; Password=1q2w3e4r@#$";
        public static void Show()
        {
            Console.BackgroundColor = ConsoleColor.Magenta;
            Console.ForegroundColor = ConsoleColor.Black;
            var connection = new SqlConnection(connectionString);

            Console.Clear();
            Console.WriteLine("--------------------TAG--------------------\n\n\n");

            Console.WriteLine("\t1 - Criar um novo tag");
            Console.WriteLine("\t2 - Listar tags");
            Console.WriteLine("\t3 - Atualizar um tag");
            Console.WriteLine("\t4 - Apagar um tag");
            Console.WriteLine("\t0 - Voltar");
            short choise = short.Parse(Console.ReadLine());

            switch (choise)
            {
                case 0: MenuMain.Show(true); break;
                case 1: InsertTag(connection); break;
                case 2: ReadTags(connection); break;
                case 3: UpdateTag(connection); break;
                case 4: InsertTag(connection); break;
                default: TagRegistration.Show(); break;
            }
            //InsertTag(connection);
            

            MenuMain.Show(true);
        }

        public static void ReadTags(SqlConnection connection)
        {
            var repository = new Repository<Models.Tag>(connection);
            var tags = repository.Read();
            foreach(var tag in tags)
            {
                Console.WriteLine($"\t\tId: {tag.Id}");
                Console.WriteLine($"\t\tNome: {tag.Name}");
                Console.WriteLine($"\t\tURL: {tag.Slug}\n");
            }
            Console.ReadKey();
            TagRegistration.Show();
        }

        public static void InsertTag(SqlConnection connection)
        {
            var repository = new Repository<Models.Tag>(connection);
            var tag = CreateTag();

            connection.Open();
            repository.Create(tag);
            connection.Close();

            Console.WriteLine("Tag criado com sucesso!");
            Console.ReadKey();
            TagRegistration.Show();
        }

        public static void UpdateTag(SqlConnection connection)
        {
            var repository = new Repository<Models.Tag>(connection);
            var tag = CreateTag();

            Console.WriteLine("Insira o Id do tag a ser atualizado");
            tag.Id = short.Parse(Console.ReadLine());

            connection.Open();
            repository.Update(tag);
            connection.Close();

            Console.WriteLine("Tag Atualizado com sucesso!");
            Console.ReadKey();
            TagRegistration.Show();
        }

        public static void DeleteTag(SqlConnection connection)
        {
            var repository = new Repository<Models.Tag>(connection);
            
            Console.WriteLine("Insira o Id do tag a ser apagado");
            var id = short.Parse(Console.ReadLine());

            repository.Delete(id);
            connection.Close();

            Console.WriteLine("Tag Deletado com sucesso!");
            Console.ReadKey();
            TagRegistration.Show();
        }


        public static Tag CreateTag()
        {
            var tag = new Tag();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Informações do Tag");

                Console.Write("\tNome: ");
                tag.Name = Console.ReadLine();

                Console.Write("\tUrl dos Tags: ");
                tag.Slug = Console.ReadLine();

                Console.Clear();
                Console.WriteLine("\tConfirmar? 1 - SIM");

                Console.WriteLine($"\t\tNome: {tag.Name}");
                Console.WriteLine($"\t\tLink de usuário: {tag.Slug}");

                var choise = Console.ReadLine();
                if (choise == "1")
                    break;
            }
            return tag;
        }
    }
}
