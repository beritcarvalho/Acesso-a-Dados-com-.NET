using Blog.Models;
using Blog.Repositories;
using Dapper.Contrib.Extensions;
using System;
using System.Data.SqlClient;

namespace Blog
{
    class Program
    {
        private const string CONNECTION_STRING = @"Server=localhost,1433;Database=Blog;User ID = sa; Password=1q2w3e4r@#$";
        static void Main(string[] args)
        {
            var connection = new SqlConnection(CONNECTION_STRING);
            var repository = new Repository<User>(connection);
            connection.Open();
            ReadUsers(connection);
            //repository.Delete(16);
            repository.Create(PrepararObjetoInsercao());

            connection.Close();           
        }

        public static void ReadUsers(SqlConnection connection)
        {
            var repository = new UserRepository(connection);
            var items = repository.ReadWithRoles();
            
            foreach (var item in items)
            {
                Console.WriteLine(item.Name);
                foreach(var role in item.Roles)
                {
                    Console.WriteLine($"\t - {role.Name}");
                }
            }
                                     
        }       
        public static void UpdateUser(SqlConnection connection)
        {
            var user = new User()
            {
                Name = "Lux Stemmaguarda",
                Email = "lux@lux.com",
                PasswordHash = "A DAMA DA LUZ",
                Bio = @"
                        Luxanna, ou Lux, como prefere ser chamada, cresceu na cidade demaciana
                        de Alta Pratânia, juntamente com seu irmão mais velho, Garen. Ambos 
                        nasceram na prestigiosa família Stemmaguarda, que tem servido por
                        gerações como protetores dos reis de Demacia. Seu avô salvou a vida do rei
                        na Batalha da Presa da Tempestade e sua tia Tianna foi nomeada comandante
                        do regimento de elite Vanguarda Destemida antes do nascimento de Lux.",
                Image = "https://static.wikia.nocookie.net/lolesports_gamepedia_en/images/7/7e/Skin_Loading_Screen_Classic_Lux.jpg/revision/latest/scale-to-width-down/150?cb=20191214234926",
                Slug = "lux-stemmaguardan"
            };
                var repository = new Repository<User>(connection);
                repository.Update(user);
                Console.WriteLine("Cadastro Atualizado com sucesso!");
        }
        

        public static void DeleteUser(SqlConnection connection)
        {
            var repository = new Repository<User>(connection);
            var user = repository.Read(6);
            repository.Delete(user);
            Console.WriteLine("Exclusão realizada com sucesso!");        
        }

        public static void ReadRoles(SqlConnection connection)
        {
            var repository = new RoleRepository(connection);
            var roles = repository.Read();

            foreach(var role in roles)
                Console.WriteLine(role.Name);            
        }

        public static User PrepararObjetoInsercao()
        {
            var user = new User()
            {
                Name = "teste Stemmaguarda",
                Email = "garen@garenn.com",
                PasswordHash = "O PODER DE DEMACIAn",
                Bio = @"
                        Nascido na nobre família Stemmaguarda, junto com sua irmã mais nova, Lux, Garen
                        sabia desde jovem que esperavam que ele defendesse o trono de Demacia com sua vida.
                        Seu pai, Pieter, era um oficial militar condecorado, enquanto sua tia, Tianna,
                        era a Capitã - Espada da elite Vanguarda Destemida, ambos eram reconhecidos e
                        muito respeitados pelo rei Jarvan III.Supunha - se que Garen viria eventualmente a 
                        servir o filho do rei da mesma maneira.",
                Image = "https://static.senpai.gg/lol/img/champion/tiles/Garen_0.webp",
                Slug = "garennn-stemmaguardann"
            };

            return user;
        }
    }
}
