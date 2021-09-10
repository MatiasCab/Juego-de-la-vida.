using System;
using System.IO;
using System.Text;
using System.Threading;
namespace PII_Game_Of_Life
{
    public class GameBoard
    {
        public bool[,] GetGameBoard{get; set;}
        
        public GameBoard(string URL)
        {
            string content = File.ReadAllText(URL);
            string[] contentLines = content.Split('\n');
            bool[,] board = new bool[contentLines.Length, contentLines[0].Length];
            for (int  y=0; y<contentLines.Length;y++)
            {
                for (int x=0; x<contentLines[y].Length; x++)
                {
                    if(contentLines[y][x]=='1')
                    {
                        board[x,y]=true;
                    }
                }
            }
            this.GetGameBoard = board;
        }
    }
}
