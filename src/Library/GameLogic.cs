
using System;
using System.IO;
using System.Text;
using System.Threading;

namespace PII_Game_Of_Life
{
    public class GameLogic
    {
        
        public static Tuple<bool[,],int,int> Logic(bool[,] gameBoard)
        {
            int boardWidth = gameBoard.GetLength(0);
            int boardHeight = gameBoard.GetLength(1);

            bool[,] cloneboard = new bool[boardWidth, boardHeight];
            for (int x = 0; x < boardWidth; x++)
            {
                for (int y = 0; y < boardHeight; y++)
                {
                    int aliveNeighbors = 0;
                    for (int i = x-1; i<=x+1;i++)
                    {
                        for (int j = y-1;j<=y+1;j++)
                        {
                            if(i>=0 && i<boardWidth && j>=0 && j < boardHeight && gameBoard[i,j])
                            {
                                aliveNeighbors++;
                            }
                        }
                    }
                    if(gameBoard[x,y])
                    {
                        aliveNeighbors--;
                    }
                    if (gameBoard[x,y] && aliveNeighbors < 2)
                    {
                        //Celula muere por baja población
                        cloneboard[x,y] = false;
                    }
                    else if (gameBoard[x,y] && aliveNeighbors > 3)
                    {
                        //Celula muere por sobrepoblación
                        cloneboard[x,y] = false;
                    }
                    else if (!gameBoard[x,y] && aliveNeighbors == 3)
                    {
                        //Celula nace por reproducción
                        cloneboard[x,y] = true;
                    }
                    else
                    {
                        //Celula mantiene el estado que tenía
                        cloneboard[x,y] = gameBoard[x,y];
                    }
                }
            }
            gameBoard = cloneboard;
            cloneboard = new bool[boardWidth, boardHeight];

            Tuple<bool[,],int,int> tupla=new Tuple <bool[,],int,int>(gameBoard, boardWidth, boardHeight);
            return tupla;
    }
}
}