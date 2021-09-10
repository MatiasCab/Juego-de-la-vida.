using System;
using System.IO;
using System.Text;
using System.Threading;

namespace PII_Game_Of_Life
{
    class Program
    {
        static void Main(string[] args)
        {
            GameBoard board =new GameBoard("el texo");
            GamePrinter.ConsolePrinter(GameLogic.Logic(board.GetGameBoard));
        }
    }
}
