using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Library
{
    class DrawScreen
    {
        public static void Draw(int columns, int lines)
        {
            Columns(columns);
            Lines(columns, lines);
            Columns(columns);
        }

        private static void Columns(int amount)
        {
            Console.Write("+");
            for (int i = 0; i <= amount; i++)
                Console.Write("-");

            Console.Write("+");
            Console.Write("\n");
        }
        private static void Lines(int columns, int lines)
        {
            for (int i = 0; i <= lines; i++)
            {
                Console.Write("|");
                for (int j = 0; j <= columns; j++)
                    Console.Write(" ");

                Console.Write("|");
                Console.Write("\n");
            }
        }
    }
}
