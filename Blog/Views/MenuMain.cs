using Blog.Library;
using System;

namespace Blog.Views
{
    public static class MenuMain
    {
        public static void Show(bool login = false)
        {
            DefaultConsole.Clear();
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.Black;
            DrawScreen.Draw(46, 20);

            WriteOptions(login);

            var option = short.Parse(Console.ReadLine());
            HandleMenuOption(option, login);

            Console.ReadKey();
            DefaultConsole.Clear();
        }

        public static void WriteOptions(bool login)
        {
            Console.SetCursorPosition(18, 2);
            Console.WriteLine("BLOGUINHO");
            Console.SetCursorPosition(2, 3);
            for (var x = 0; x <= 44; x++)
            {
                if (x < 46)
                    Console.Write("=");
                else Console.WriteLine("=");
            }

            Console.SetCursorPosition(3, 5);
            Console.WriteLine("Selecione uma opção");
            Console.SetCursorPosition(3, 7);
            Console.WriteLine("1 - Login");
            Console.SetCursorPosition(3, 8);
            Console.WriteLine("2 - Postagens");
            Console.SetCursorPosition(3, 9);
            Console.WriteLine("3 - Cadastrar um usuário");
            if (login)
            {
                Console.SetCursorPosition(3, 10);
                Console.WriteLine("4 - Cadastrar um perfil");
                Console.SetCursorPosition(3, 11);
                Console.WriteLine("5 - Cadastrar uma categoria");
                Console.SetCursorPosition(3, 12);
                Console.WriteLine("6 - Cadastrar uma tag");
                Console.SetCursorPosition(3, 12);
                Console.WriteLine("7 - Relatórios");
            }
            Console.SetCursorPosition(3, 17);
            Console.WriteLine("0 - Sair");
            Console.SetCursorPosition(3, 18);
            Console.Write("Opção: ");
        }
        public static void HandleMenuOption(short option, bool login)
        {
            switch (option)
            {
                case 0:
                    Console.SetCursorPosition(10, 18);
                    Console.WriteLine("Sair");
                    Console.SetCursorPosition(1, 23);
                    Console.ReadKey();
                    DefaultConsole.Clear();
                    System.Environment.Exit(0); break;                    
                case 1:
                    Console.SetCursorPosition(12, 18);
                    Logon.Show(); break;
                case 2:
                    Console.SetCursorPosition(12, 18);
                    PostRegistration.Show(); break;
                case 3:
                    Console.SetCursorPosition(12, 18);
                    UserRegistration.Show(); break;
                    break;
                case 4:
                    Console.SetCursorPosition(12, 18);
                    if(login)
                        RoleRegistration.Show(); break;
                case 5:
                    Console.SetCursorPosition(12, 18);
                    if (login)
                        CategoryRegistration.Show(); break;
                case 6:
                    Console.SetCursorPosition(12, 18);
                    if (login)
                        TagRegistration.Show();
                    else Show(); break;
                default:
                    Show(); break;
            }
        }
    }
    
}
