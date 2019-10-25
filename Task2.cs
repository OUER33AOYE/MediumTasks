using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            Update instance = new Update();

            instance.update();

            Console.ReadLine();
        }
    }
    public class Screen
    {
        public List<Rectangle> AllRectangleObject = new List<Rectangle>();
        public char[,] pixels = new char[20, 60];
        public Cursor myCursor = new Cursor();
        private string info = "arrows for moving cursor, F - Action , T - Input text, B - Button , C - checkbox";
        public string output = "";
        public bool isEdit = false;
        public int startEditI, startEditJ;
        public void Initialize()
        {
            for (int i = 0; i < 20; i++)
            {
                pixels[i, 0] = '#';
                pixels[i, 59] = '#';
            }
            for (int i = 1; i < 59; i++)
            {
                pixels[0, i] = '#';
                pixels[19, i] = '#';
            }
        }
        public void edit()
        {
            for (int i = Math.Min(startEditI, myCursor.i); i <= Math.Max(startEditI, myCursor.i); i++)
            {
                for (int j = Math.Min(startEditJ, myCursor.j); j <= Math.Max(startEditJ, myCursor.j); j++)
                {
                    pixels[i, j] = '?';
                }
            }
        }
        public void endEdit(char typeOfRect)
        {
            isEdit = false;
            if (typeOfRect == 'T')
                AllRectangleObject.Add(new Text(Math.Max(startEditI, myCursor.i), Math.Max(startEditJ, myCursor.j), Math.Min(startEditI, myCursor.i), Math.Min(startEditJ, myCursor.j)));
            if (typeOfRect == 'B')
                AllRectangleObject.Add(new Button(Math.Max(startEditI, myCursor.i), Math.Max(startEditJ, myCursor.j), Math.Min(startEditI, myCursor.i), Math.Min(startEditJ, myCursor.j)));
            if (typeOfRect == 'c')
                AllRectangleObject.Add(new CheckBox(Math.Max(startEditI, myCursor.i), Math.Max(startEditJ, myCursor.j), Math.Min(startEditI, myCursor.i), Math.Min(startEditJ, myCursor.j)));
        }
        public void Show()
        {
            Console.Clear();
            ClearPixels();
            if (isEdit)
                edit();
            foreach (var v in AllRectangleObject)
            {
                v.Draw(pixels);
            }
            pixels[myCursor.i, myCursor.j] = 'C';
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 60; j++)
                {
                    Console.Write(pixels[i, j]);
                }
                Console.WriteLine();
            };
            Console.WriteLine(info);
            Console.WriteLine("Some output:");
            Console.WriteLine(output);
        }
        public void ClearPixels()
        {
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 60; j++)
                {
                    if (pixels[i, j] != '#')
                        pixels[i, j] = ' ';
                }
            };
        }
    }
    public class Update
    {
        Screen myScreen = new Screen();
        char editedChar;

        public void update()
        {
            myScreen.Initialize();
            myScreen.Show();
            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.DownArrow)
                {
                    if (myScreen.isEdit)
                    {
                        if (checkCollisionWithRect(myScreen.myCursor.i + 1, myScreen.myCursor.j) == false)
                            myScreen.myCursor.Down();
                    }
                    else myScreen.myCursor.Down();
                }
                if (key.Key == ConsoleKey.UpArrow)
                {
                    if (myScreen.isEdit)
                    {
                        if (checkCollisionWithRect(myScreen.myCursor.i - 1, myScreen.myCursor.j) == false)
                            myScreen.myCursor.Up();
                    }
                    else myScreen.myCursor.Up();
                }
                if (key.Key == ConsoleKey.LeftArrow)
                {
                    if (myScreen.isEdit)
                    {
                        if (checkCollisionWithRect(myScreen.myCursor.i, myScreen.myCursor.j - 1) == false)
                            myScreen.myCursor.Left();
                    }
                    else myScreen.myCursor.Left();
                }
                if (key.Key == ConsoleKey.RightArrow)
                {
                    if (myScreen.isEdit)
                    {
                        if (checkCollisionWithRect(myScreen.myCursor.i, myScreen.myCursor.j + 1) == false)
                            myScreen.myCursor.Right();
                    }
                    else myScreen.myCursor.Right();
                }
                if (key.Key == ConsoleKey.F)
                {
                    myScreen.output = String.Format("{0} {1}", myScreen.myCursor.i, myScreen.myCursor.j);
                    if (myScreen.isEdit)
                    {
                        myScreen.output = "";
                        myScreen.endEdit(editedChar);
                    }
                    else
                    {
                        Rectangle Rect = SelectSome(myScreen.myCursor.i, myScreen.myCursor.j);
                        if (Rect != null)
                        {

                            if (Rect.ToString() == "ConsoleApp2.Text")
                            {
                                Console.WriteLine("Write you text:");
                                ((Text)Rect).EditText(Console.ReadLine());
                                Rect.Draw(myScreen.pixels);
                            }
                            if (Rect.ToString() == "ConsoleApp2.Button")
                            {
                                myScreen.output = "Button was clicked!";
                            }
                            if (Rect.ToString() == "ConsoleApp2.CheckBox")
                            {
                                ((CheckBox)Rect).isOn ^= true;
                                myScreen.output = "CheckBox was changed!";
                            }
                        }
                    }
                }
                if (key.Key == ConsoleKey.T)
                {
                    KeyDown();
                    editedChar = 'T';
                }
                if (key.Key == ConsoleKey.B)
                {
                    KeyDown();
                    editedChar = 'B';
                }
                if (key.Key == ConsoleKey.C)
                {
                    KeyDown();
                    editedChar = 'c';
                }
                myScreen.Show();
                //Thread.Sleep(10);
            }
        }
        public void KeyDown()
        {
            myScreen.startEditI = myScreen.myCursor.i;
            myScreen.startEditJ = myScreen.myCursor.j;
            if (checkCollisionWithRect(myScreen.myCursor.i, myScreen.myCursor.j) == false)
            {
                if (myScreen.isEdit == false)
                {
                    myScreen.isEdit = true;
                    myScreen.output = "edit mode F - end edit";
                }
            }
        }
        public bool checkCollisionWithRect(int i, int j)
        {
            int maxI = Math.Max(myScreen.startEditI, i), minI = Math.Min(myScreen.startEditI, i);
            int maxJ = Math.Max(myScreen.startEditJ, j), minJ = Math.Min(myScreen.startEditJ, j);
            foreach (var v in myScreen.AllRectangleObject)
            {
                if ((maxI < v.minI || minI > v.maxI || maxJ < v.minJ || minJ > v.maxJ) == false)
                {
                    return true;
                }
            }
            return false;
        }
        public Rectangle SelectSome(int i, int j)
        {
            return myScreen.AllRectangleObject.Find(item => item.maxI >= i && item.minI <= i && item.maxJ >= j && item.minJ <= j);
        }
    }
    public class Cursor
    {
        public int i { get; private set; }
        public int j { get; private set; }
        public Cursor()
        {
            i = 1;
            j = 1;
        }
        public void Up()
        {
            if (i > 1) i--;
        }
        public void Down()
        {
            if (i < 18) i++;
        }
        public void Left()
        {
            if (j > 1) j--;
        }
        public void Right()
        {
            if (j < 58) j++;
        }

    }

    public class Rectangle
    {
        public int maxI, maxJ, minI, minJ;
        public virtual void Draw(char[,] field)
        {
            for (int i = minI; i <= maxI; i++)
            {
                for (int j = minJ; j <= maxJ; j++)
                {
                    field[i, j] = 'R';
                }
            }
        }

    }
    public class Text : Rectangle
    {
        int height, weight;
        char[,] textdata;
        public Text(int a, int b, int c, int d)
        {
            maxI = a; maxJ = b; minI = c; minJ = d;
            textdata = new char[height = a - c + 1, weight = b - d + 1];
            for (int i = 0; i < height; i++)
                for (int j = 0; j < weight; j++)
                    textdata[i, j] = '.';
        }
        public override void Draw(char[,] field)
        {
            for (int i = minI; i <= maxI; i++)
            {
                for (int j = minJ; j <= maxJ; j++)
                {
                    field[i, j] = textdata[i - minI, j - minJ];
                }
            }
        }
        public void EditText(string NewText)
        {
            for (int i = 0; i < height; i++)
                for (int j = 0; j < weight && i * weight + j < NewText.Length; j++)
                {
                    textdata[i, j] = NewText[i * weight + j];
                }
        }
    }
    public class CheckBox : Rectangle
    {
        public bool isOn;
        public CheckBox(int a, int b, int c, int d)
        {
            isOn = false;
            maxI = a; maxJ = b; minI = c; minJ = d;
        }
        public override void Draw(char[,] field)
        {
            for (int i = minI; i <= maxI; i++)
            {
                for (int j = minJ; j <= maxJ; j++)
                {
                    field[i, j] = isOn ? 'o' : 'x';
                }
            }
        }
    }
    public class Button : Rectangle
    {
        public Button(int a, int b, int c, int d)
        {
            maxI = a; maxJ = b; minI = c; minJ = d;
        }
        public override void Draw(char[,] field)
        {
            for (int i = minI; i <= maxI; i++)
            {
                for (int j = minJ; j <= maxJ; j++)
                {
                    field[i, j] = 'B';
                }
            }
        }
    }
}
