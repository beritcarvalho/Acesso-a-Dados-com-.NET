using Blog.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            DrawScreen.Draw(46, 14);

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
            Console.WriteLine("2 - Ver Postagens");
            Console.SetCursorPosition(3, 9);
            Console.WriteLine("3 - Cadastrar um usuário");
            if (login)
            {
                Console.SetCursorPosition(3, 10);
                Console.WriteLine("4 - Cadastrar um perfil");
            }
            Console.SetCursorPosition(3, 12);
            Console.WriteLine("0 - Sair");
            Console.SetCursorPosition(3, 13);
            Console.Write("Opção: ");
        }
        public static void HandleMenuOption(short option, bool login)
        {
            switch (option)
            {
                case 0:
                    Console.SetCursorPosition(10, 14);
                    Console.WriteLine("Sair");
                    Console.SetCursorPosition(1, 17);
                    Console.ReadKey();
                    DefaultConsole.Clear();
                    System.Environment.Exit(0); break;                    
                case 1:
                    Console.SetCursorPosition(12, 13);
                    Logon.Show(); break;
                case 2:
                    Console.SetCursorPosition(12, 13);
                    Console.WriteLine("Ver Postagens"); break;
                case 3:
                    Console.SetCursorPosition(12, 13);
                    Console.WriteLine("Cadastrar um usuário"); break;
                case 4:
                    Console.SetCursorPosition(12, 13);
                    if(login)
                        Console.WriteLine("Cadastrar um perfil"); 
                    else Show(); break;
                default:
                    Show(); break;
            }
        }
    }
    
}
