using Blog.Models;
using Blog.Repositories;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Views
{
    class UserRegistration
    {
        private const string connectionString = @"Server=localhost,1433;Database=Blog;User ID = sa; Password=1q2w3e4r@#$";
        public static void Show()
        {
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.ForegroundColor = ConsoleColor.Black;

            Console.Clear();
            Console.WriteLine("--------------------CADASTRAR NOVO USUÁRIO--------------------\n\n\n");

            var connection = new SqlConnection(connectionString);
            var repository = new Repository<User>(connection);

            var user = CreateUser();

            connection.Open();           
            repository.Create(user);
            connection.Close();

            Console.WriteLine("Ir para o menu");
            Console.ReadKey();

            MenuMain.Show();
        }

        public static User CreateUser()
        {
            var user = new User();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Informações do Perfil");

                Console.Write("\tNome: ");
                user.Name = Console.ReadLine();

                Console.Write("\tEmail: ");
                user.Email = Console.ReadLine();

                Console.Write("\tBiografia: ");
                user.Bio = Console.ReadLine();

                Console.Write("\tUrl da Imagem: ");
                user.Image = Console.ReadLine();

                Console.Write("\tUrl do Usuario: ");
                user.Slug = Console.ReadLine();


                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("\tCadastro de Senha");
                    Console.Write("\t\tNova senha: ");
                    var password = Console.ReadLine();

                    Console.Write("\t\tConfirme a senha: ");
                    var passwordConfirmation = Console.ReadLine();

                    if (password == passwordConfirmation)
                    {
                        user.PasswordHash = password;
                        break;
                    }
                }

                Console.Clear();
                Console.WriteLine("\tConfirmar? 1 - SIM");

                Console.WriteLine($"\t\tNome: {user.Name}");
                Console.WriteLine($"\t\tEmail: {user.Email}");
                Console.WriteLine($"\t\tBiografia: {user.Bio}");
                Console.WriteLine($"\t\tUrl Imagem: {user.Image}");
                Console.WriteLine($"\t\tLink de usuário: {user.Slug}");
                Console.WriteLine($"\t\tSenha: {user.PasswordHash}");

                var choise = Console.ReadLine();
                if (choise == "1")
                    break;

            }
            return user;
        }
    }
}
