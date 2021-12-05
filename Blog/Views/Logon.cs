using Blog.Repositories;
using System;
using Blog;
using Blog.Contexts;
using System.Data.SqlClient;
using Blog.Models;
using System.Linq;
using Blog.Library;
using System.Text;

namespace Blog.Views
{
    public static class Logon
    {
        private const string connectionString = @"Server=localhost,1433;Database=Blog;User ID = sa; Password=1q2w3e4r@#$";
        public static void Show()
        {
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.Black;

            var connection = new SqlConnection(connectionString);
            
            connection.Open();
            var repository = new Repository<User>(connection);

                        var login = false;
            while (true)
            {
                Console.Clear();
                Console.WriteLine("--------------------LOGIN--------------------\n\n\n");

                var email = "";
                var password = "";

                Console.Write("\tEmail: ");
                email = Console.ReadLine();
                Console.WriteLine("\tSenha: ");
                password = LerSenha();

                var users = repository.Read();
                var user = users.Where(u => u.Email == email).FirstOrDefault();
                var validadion = new Validation();

                if (user != null)
                {                    
                    if (validadion.Validate(user, password))
                    {
                        login = true;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("\n\tSenha inválida");
                    }
                }
                else
                    Console.WriteLine("\n\tUsuário não encontrado");

                if(user == null || !validadion.Validate(user, password))
                {
                    Console.Write("\n\tTentar novamente? 1 - Não: ");
                    var choise = Console.ReadLine();
                    if (choise == "1")
                        break;                     
                }
            }
            connection.Close();
            MenuMain.Show(login);
        }

        public static string LerSenha()
        {
            StringBuilder pw = new StringBuilder();
            bool caracterApagado = false;

            while (true)
            {
                ConsoleKeyInfo cki = Console.ReadKey(true);

                if (cki.Key == ConsoleKey.Enter)
                {
                    Console.WriteLine();
                    break;
                }

                if (deletarTexto(cki))
                {
                    if (pw.Length != 0)
                    {
                        Console.Write("\b \b");
                        pw.Length--;

                        caracterApagado = true;
                    }
                }
                else
                {
                    caracterApagado = false;
                }

                if (!caracterApagado && verificarCaracterValido(cki))
                {
                    Console.Write('•');
                    pw.Append(cki.KeyChar);
                }
            }

            return pw.ToString();
        }

        private static bool verificarCaracterValido(ConsoleKeyInfo tecla)
        {
            if (char.IsLetterOrDigit(tecla.KeyChar) || char.IsPunctuation(tecla.KeyChar) ||
                char.IsSymbol(tecla.KeyChar))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        private static bool deletarTexto(ConsoleKeyInfo tecla)
        {
            if (tecla.Key == ConsoleKey.Backspace || tecla.Key == ConsoleKey.Delete)
                return true;
            else
                return false;
        }

    }



}
