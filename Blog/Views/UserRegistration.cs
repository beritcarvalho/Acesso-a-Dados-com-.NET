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

            connection.Open();
            var repository = new Repository<User>(connection);
            connection.Close();
            Console.ReadKey();

            MenuMain.Show();
        }
    }
}
