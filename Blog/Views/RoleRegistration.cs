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
    class RoleRegistration
    {
        private const string connectionString = @"Server=localhost,1433;Database=Blog;User ID = sa; Password=1q2w3e4r@#$";
        public static void Show()
        {
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.ForegroundColor = ConsoleColor.Black;
            var connection = new SqlConnection(connectionString);

            Console.Clear();
            Console.WriteLine("--------------------PERFIL--------------------\n\n\n");

            Console.WriteLine("\t1 - Criar um novo perfil");
            Console.WriteLine("\t2 - Listar perfis");
            Console.WriteLine("\t3 - Atualizar um perfil");
            Console.WriteLine("\t4 - Apagar um perfil");
            Console.WriteLine("\t0 - Voltar");
            short choise = short.Parse(Console.ReadLine());

            switch (choise)
            {
                case 0: MenuMain.Show(true); break;
                case 1: InsertRole(connection); break;
                case 2: ReadRoles(connection); break;
                case 3: UpdateRole(connection); break;
                case 4: InsertRole(connection); break;
                default: RoleRegistration.Show(); break;
            }
            //InsertRole(connection);
            

            MenuMain.Show(true);
        }

        public static void ReadRoles(SqlConnection connection)
        {
            var repository = new Repository<Models.Role>(connection);
            var roles = repository.Read();
            foreach(var role in roles)
            {
                Console.WriteLine($"\t\tId: {role.Id}");
                Console.WriteLine($"\t\tNome: {role.Name}");
                Console.WriteLine($"\t\tURL: {role.Slug}\n");
            }
            Console.ReadKey();
            RoleRegistration.Show();
        }

        public static void InsertRole(SqlConnection connection)
        {
            var repository = new Repository<Models.Role>(connection);
            var role = CreateRole();

            connection.Open();
            repository.Create(role);
            connection.Close();

            Console.WriteLine("Perfil criado com sucesso!");
            Console.ReadKey();
            RoleRegistration.Show();
        }

        public static void UpdateRole(SqlConnection connection)
        {
            var repository = new Repository<Models.Role>(connection);
            var role = CreateRole();

            Console.WriteLine("Insira o Id do perfil a ser atualizado");
            role.Id = short.Parse(Console.ReadLine());

            connection.Open();
            repository.Update(role);
            connection.Close();

            Console.WriteLine("Perfil Atualizado com sucesso!");
            Console.ReadKey();
            RoleRegistration.Show();
        }

        public static void DeleteRole(SqlConnection connection)
        {
            var repository = new Repository<Models.Role>(connection);
            
            Console.WriteLine("Insira o Id do perfil a ser apagado");
            var id = short.Parse(Console.ReadLine());

            repository.Delete(id);
            connection.Close();

            Console.WriteLine("Perfil Deletado com sucesso!");
            Console.ReadKey();
            RoleRegistration.Show();
        }


        public static Role CreateRole()
        {
            var role = new Role();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Informações do Perfil");

                Console.Write("\tNome: ");
                role.Name = Console.ReadLine();

                Console.Write("\tUrl dos Perfis: ");
                role.Slug = Console.ReadLine();

                Console.Clear();
                Console.WriteLine("\tConfirmar? 1 - SIM");

                Console.WriteLine($"\t\tNome: {role.Name}");
                Console.WriteLine($"\t\tLink de usuário: {role.Slug}");

                var choise = Console.ReadLine();
                if (choise == "1")
                    break;
            }
            return role;
        }
    }
}
