# Universidad Católica del Uruguay
<img src="https://ucu.edu.uy/sites/all/themes/univer/logo.png">

## Facultad de Ingeniería y Tecnologías
### Programación II

## Conway's Game of Life
[John Conway](https://en.wikipedia.org/wiki/John_Horton_Conway) fue un matemático inglés muy conocido por sus aportes matemáticos en diversas áreas, pero lo que realmente lo hizo famoso fue su creación lúdica: [juego de la vida](https://en.wikipedia.org/wiki/Conway%27s_Game_of_Life).

![](https://upload.wikimedia.org/wikipedia/commons/e/e5/Gospers_glider_gun.gif)

El juego de la vida consiste en un autómata celular con unas pocas reglas muy simples.
El universo es una grilla ortogonal de dos dimensiones, donde cada espacio de la grilla representa una única célula.
Cada célula puede estar viva o muerta.
Cada una de estas células tendra 8 vecinos.
En cada iteración del tiempo (generación) una célula estará viva o muerta según la cantidad de vecinos vivos o muertos que tenga. Siguiendo estas reglas:
* Una célula viva con menos de 2 vecinos vivos muere, por soledad.
* Una célula viva con 2 o 3 vecinos vivos sobrevive a la siguiente generación.
* Una célula viva con más de 3 vecinos vivos muere, por sobrepoblación.
* Una célula muerta con exactamente 3 vecinos vivos se convierte en una célula viva, por reproducción

## Parte 1
Como tributo a Conway ¡hoy vamos a crear este juego en consola! Para ello te vamos a proveer de varios code [snippets](https://en.wikipedia.org/wiki/Snippet_(programming)) y será tu trabajo asignarlos a la clase correcta cumpliendo con Expert y SRP.

El objetivo será desarrollar este juego mediante objetos diferentes, cada uno con una resposnabilidad única (SRP). El tablero deberá ser cargado a partir de un archivo de texto y luego el avance del juego deberá ser impreso en pantalla mediante consola.

> :exclamation: Debes tener en cuenta que hoy se pide que el juego se lea desde un archivo y se imprima en consola, pero mañana podremos pedirles que se lea de una fuente diferente y se muestre en pantalla por otro medio :wink:

> :pencil2: Recuerda agregar comentarios a todas tus clases indicando si cumplen o no con SRP y Expert. Deberás justificar adecuadamente por que crees que cumple o no.

Si todo ha funcionado correctamente, tu resultado deberia verse algo similar a esto:

![](./assets/console-gif-GoL.gif)

> Hemos incluido en la carpeta *assets* un archivo de texto con este mismo ejemplo

## Code Snippets
A continuación se presentan fragmentos de código suelto (snippets) que podrás reutilizar en tu solución.

> :warning: **Atención!!** Estos fragmentos de código son genéricos y no funcionaran simplemente haciendo copy/paste. Si bien la estructura general y la mayoría del código no debería ser necesario modificarlo, deberan ser adaptados a tu solución propuesta.

### Lógica de juego
El siguiente code snippet contiene la lógica necesaria para procesar una generación del juego.
Se asume:
* Que el tablero es un array de 2 dimensiones de booleanos, donde ```true``` indica una célula viva y ```false``` indica una célula muerta.
* El objeto ```gameBoard``` contiene un tablero ya cargado con todos los valores asignados.

```csharp
bool[,] gameBoard = /* contenido del tablero */;
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
```

> ```bool[,]``` es la declaración de un vector o arreglo -array- multidimensional. Puedes ver [aquí](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/arrays/multidimensional-arrays) más información.

### Leer Archivo
Este snippet muestra como leer un archivo y crear un array bi-dimensional de booleanos (```bool[,]```) con el contenido. Se asume que cada linea del archivo corresponde a una fila de la matriz y cada caracter de la fila corresponde a un elemento de la fila correspondiente de la matriz. Tambien se asume que el archivo contiene solamente los caracteres ```1``` y ```0``` correspondientes a ```true``` y ```false``` respectivamente y que todas las filas son de igual largo.
Por ejemplo, el siguiente archivo de texto:
```
100
011
110
```

se convierte en la siguiente matriz:
```csharp
bool[3,3] {
    {true, false, false},
    {false, true, true},
    {true, true, false}
};
```

> Esta forma incluso tiene nombre y se llama glider
>
> ![](https://upload.wikimedia.org/wikipedia/commons/f/f2/Game_of_life_animated_glider.gif)

Snippet de código:

```csharp
string url = "ruta del archivo";
string content = File.ReadAllText(url);
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
```

> La clase ```File``` está definida en el espacio de nombres ```Sytem.IO```. Debes incluirlo utilizando una cláusula ```using```.

> Como el caracter ```\``` que se utilizan para escribir rutas a archivos -dependiendo del sistema operativo- tiene un signficiado especial en una cadena de caracteres -indica una secuencia de escape, por ejemplo ```\n``` indica una nueva línea- puedes especificar la ruta usando ```@"../../assets/board.txt"``` por ejemplo. Ten en cuenta que el archivo que te proveemos ```board.txt``` en la carpeta ```assets``` está en un ubicación diferente de la carpeta ```Program``` en la que se ejecuta tu programa, y debes indicar la ubicación del archivo utilizando una ruta relativa.

### Imprimir Tablero
Aqui se muestra como imprimir un tablero por consola. Observa que este código requiere invocar el snippet de la lógica de juego

```csharp
bool[,] b //variable que representa el tablero
int width //variabe que representa el ancho del tablero
int height //variabe que representa altura del tablero
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
    Thread.Sleep(300);
}
```

> La clase ```StringBuilder``` está definida en el espacio de nombres ```Sytem.Text```. Debes incluirlo utilizando una cláusula ```using```.

------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

namespace PII_Game_Of_Lifee
{
    class Program
    {
        static void Main(string[] args)
        {
string url = @"C:\board.txt";
string content = File.ReadAllText(url);
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
bool[,] gameBoard = board;
//------------------------------------------------
Tuple<bool[,],int,int, bool[,]> tupla= Logica(gameBoard);

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
tupla= Logica(b);

b = tupla.Item1; //variable que representa el tablero
width = tupla.Item2; //variabe que representa el ancho del tablero
height = tupla.Item3; //variabe que representa altura del tablero
    Thread.Sleep(300);
}
        }
    
    public static Tuple<bool[,],int,int, bool[,]> Logica(bool[,] board)
    {
bool[,] gameBoard = board;
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

Tuple<bool[,],int,int, bool[,]> tupla=new Tuple <bool[,],int,int, bool[,]>(gameBoard, boardWidth, boardHeight,gameBoard);
return tupla;
    }
    }
}