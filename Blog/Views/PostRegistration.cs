using Blog.Models;
using Blog.Repositories;
using System;
using System.Data.SqlClient;

namespace Blog.Views
{
    class PostRegistration
    {
        public static void Show()
        {
            var repository = new Repository<Post>(Database.Connection);
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.ForegroundColor = ConsoleColor.Black;

            Console.Clear();
            Console.WriteLine("--------------------CADASTRAR NOVO POST--------------------\n\n\n");

            Console.WriteLine("\t1 - Criar um novo post");
            Console.WriteLine("\t2 - Listar posts");
            Console.WriteLine("\t3 - Atualizar um post");
            Console.WriteLine("\t4 - Apagar um post");
            Console.WriteLine("\t0 - Voltar");
            short choise = short.Parse(Console.ReadLine());

            switch (choise)
            {
                case 0: MenuMain.Show(true); break;
                case 1: InsertPost(repository); break;
                case 2: ReadPosts(repository); break;
                case 3: UpdatePost(repository); break;
                case 4: InsertPost(repository); break;
                default: PostRegistration.Show(); break;
            }
            //InsertPost(connection);


            MenuMain.Show(true);




            InsertPost(repository);

            MenuMain.Show();
        }


        public static void InsertPost(Repository<Post> repository)
        {
            var post = CreatePost();
            post.CreateDate = DateTime.Now;
            repository.Create(post);


            Console.WriteLine("Post criado com sucesso!");
            Console.ReadKey();

        }
        public static Post CreatePost()
        {
            var repositoryCategory = new Repository<Category>(Database.Connection);
            var repositoryUser = new Repository<Category>(Database.Connection);


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
                if (choiseCategory == "1")
                {
                    var categories = repositoryCategory.Read();
                    foreach (var category in categories)
                    {
                        Console.WriteLine($"{category.Name} => {category.Id}");
                    }
                }

                Console.Write("\tSelecionado Id de Categoria: ");
                post.CategoryId = int.Parse(Console.ReadLine());

                Console.Write("\tId do Autor ");
                if (Logon.Id != 0)
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

                Console.WriteLine("\tConfirmar?");
                var choise = Console.ReadLine();
                if (choise == "1")
                    break;
                else if (choise == "0")
                {
                    MenuMain.Show(true);
                }

            }

            return post;
        }

        public static void ReadPosts(Repository<Post> repository)
        {
            var posts = repository.Read();
            foreach (var post in posts)
            {
                Console.WriteLine($"\t\tId: {post.Id}");
                Console.WriteLine($"\t\tTítulo: {post.Title}");
                Console.WriteLine($"\t\tSumário: {post.Summary}");
                Console.WriteLine($"\t\tConteúdo: {post.Body}");
                Console.WriteLine($"\t\tSlug: {post.Slug}");
                Console.WriteLine($"\t\tData de Criação: {post.CreateDate}");
                Console.WriteLine($"\t\tÚltima Atualização: {post.LastUpdateDate}");
                Console.WriteLine($"\t\tId de Categoria: {post.CategoryId}");
                Console.WriteLine($"\t\tId do Author: {post.AuthorId}\n");

    }
            Console.ReadKey();
            PostRegistration.Show();
        }

        public static void UpdatePost(Repository<Post> repository)
        {            
            var post = CreatePost();

            Console.WriteLine("Insira o Id do post a ser atualizado");
            post.Id = short.Parse(Console.ReadLine());

            repository.Update(post);

            Console.WriteLine("Post Atualizado com sucesso!");
            Console.ReadKey();
            PostRegistration.Show();
        }

        public static void DeletePost(Repository<Post> repository)
        {
    
            Console.WriteLine("Insira o Id do post a ser apagado");
            var id = short.Parse(Console.ReadLine());

            repository.Delete(id);

            Console.WriteLine("Post Deletado com sucesso!");
            Console.ReadKey();
            PostRegistration.Show();
        }

    }
}

