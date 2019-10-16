using System;
using System.Collections.Generic;

namespace Task
{
    class Program
    {
        class ObjXY
        {
            public int objx { get; set; }
            public int objy { get; set; }
            public bool isalive { get; set; }
            public ObjXY(int x, int y)
            {
                objx = x;
                objy = y;
                isalive = true;
            }
        }
        class StoreForObjXY
        {
            ObjXY[] ArrayOfobjXY;
            public StoreForObjXY(ObjXY[] Objects) => ArrayOfobjXY = Objects;

            private Random random = new Random();

            public void SomeDead()
            {
                for (int i = 0; i < ArrayOfobjXY.Length - 1; i++)
                {
                    for (int j = i + 1; j < ArrayOfobjXY.Length; j++)
                    {
                        if (ArrayOfobjXY[i].objx == ArrayOfobjXY[j].objx && ArrayOfobjXY[i].objy == ArrayOfobjXY[j].objy)
                        {
                            ArrayOfobjXY[i].isalive = false;
                            ArrayOfobjXY[j].isalive = false;
                        }
                    }
                }
            }
            public void NextStep()
            {
                for (int i = 0; i < ArrayOfobjXY.Length; i++)
                {
                    int randomx = random.Next(-1, 1), randomy = random.Next(-1, 1);
                    ArrayOfobjXY[i].objx += (ArrayOfobjXY[i].objx + randomx) < 0 ? 0 : randomx;
                    ArrayOfobjXY[i].objy += (ArrayOfobjXY[i].objy + randomy) < 0 ? 0 : randomy;
                    if (ArrayOfobjXY[i].isalive)
                    {
                        Console.SetCursorPosition(ArrayOfobjXY[i].objx, ArrayOfobjXY[i].objy);
                        Console.Write(i + 1);
                    }
                }
            }
        }
        public static void Main(string[] args)
        {


            StoreForObjXY Data = new StoreForObjXY(new ObjXY[] { new ObjXY(5, 5), new ObjXY(10, 10), new ObjXY(15, 15) });
            Random random = new Random();

            while (true)
            {
                Data.SomeDead();
                Data.NextStep();
            }
        }
    }
}