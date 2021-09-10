using System;
using System;
using System.IO;
using System.Text;
using System.Threading;

namespace PII_Game_Of_Life
{
    public class GamePrinter
    {
        public static void ConsolePrinter(Tuple<bool[,],int,int> tupla)
        {
            bool[,] b = tupla.Item1; //variable que representa el tablero
            int width = tupla.Item2; //variabe que representa el ancho del tablero
            int height = tupla.Item3; //variabe que representa altura del tablero
            while (true)
            {
                Console.Clear();
                StringBuilder s = new StringBuilder();
                for (int y = 0; y<height;y++)
                {
                    for (int x = 0; x<width; x++)
                    {
                        if(b[x,y])
                        {
                            s.Append("|X|");
                        }
                        else
                        {
                            s.Append("___");
                        }
                    }
                    s.Append("\n");
                }
                Console.WriteLine(s.ToString());
                //=================================================
                //Invocar método para calcular siguiente generación
                //=================================================
            tupla= GameLogic.Logic(b);

            b = tupla.Item1; //variable que representa el tablero
            width = tupla.Item2; //variabe que representa el ancho del tablero
            height = tupla.Item3; //variabe que representa altura del tablero
            Thread.Sleep(300);
        }
        }
    }
}