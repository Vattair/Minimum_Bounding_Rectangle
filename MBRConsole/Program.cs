using System;
using System.Collections.Generic;


class Program
{
    static void Main(string[] args)
    {
        //number of test
        int t = int.Parse(Console.ReadLine());
        for (int i = 0; i < t; i++)
        {
            List<IFigura> listaFigur = new List<IFigura>();
            //number of objects
            int n = int.Parse(Console.ReadLine());
            for (int i2 = 0; i2 < n; i2++)
            {
                //split string to string array
                string[] obj = Console.ReadLine().Split();

                switch (obj[0])
                {
                    //point
                    case "p":
                        listaFigur.Add(new Punkt(int.Parse(obj[1]), int.Parse(obj[2])));
                        break;
                    //circle
                    case "c":
                        listaFigur.Add(new Kolo(int.Parse(obj[1]), int.Parse(obj[2]), int.Parse(obj[3])));
                        break;
                    //line-segment
                    case "l":
                        listaFigur.Add(new Odcinek(int.Parse(obj[1]), int.Parse(obj[2]), int.Parse(obj[3]), int.Parse(obj[4])));
                        break;
                    default:
                        Console.WriteLine($"Nieznany typ objektu {obj[0]}");
                        break;
                }
            }
            Console.ReadLine();
            Console.WriteLine(MinimumBoundingRectangle(listaFigur));
        }
    }

    private static Prostokat MinimumBoundingRectangle(List<IFigura> listaFigur)
    {
        //start with first figure bounding rectangle
        int x1 = listaFigur[0].BoundingRectangle.X1;
        int y1 = listaFigur[0].BoundingRectangle.Y1;
        int x2 = listaFigur[0].BoundingRectangle.X2;
        int y2 = listaFigur[0].BoundingRectangle.Y2;
        for (int i = 1; i < listaFigur.Count; i++)
        {
            x1 = Math.Min(x1, listaFigur[i].BoundingRectangle.X1);
            y1 = Math.Min(y1, listaFigur[i].BoundingRectangle.Y1);
            x2 = Math.Max(x2, listaFigur[i].BoundingRectangle.X2);
            y2 = Math.Max(y2, listaFigur[i].BoundingRectangle.Y2);
        }
        return new Prostokat(x1, y1, x2, y2);
    }




    interface IFigura
    {
        Prostokat BoundingRectangle { get; }
    }
    class Prostokat : IFigura
    {
        public readonly int X1, Y1, X2, Y2;

        public Prostokat(int x1, int y1, int x2, int y2)
        {
            X1 = x1;
            Y1 = y1;
            X2 = x2;
            Y2 = y2;
        }

        public Prostokat BoundingRectangle => new Prostokat(X1, Y1, X2, Y2);

        public override string ToString()
        {
            return $"{X1} {Y1} {X2} {Y2}";
        }
    }
    class Kolo : IFigura
    {
        public readonly int X, Y, R;

        public Kolo(int x, int y, int r)
        {
            X = x;
            Y = y;
            R = r;
        }

        public Prostokat BoundingRectangle => new Prostokat(X - R, Y - R, X + R, Y + R);
    }
    class Odcinek : IFigura
    {
        public readonly int X1, Y1, X2, Y2;

        public Odcinek(int x1, int y1, int x2, int y2)
        {
            if(x1 > x2)
            {
                X1 = x2;
                X2 = x1;
                   
            }
            else
            {
                X1 = x1;
                X2 = x2;
            }
            if (y1 > y2)
            {
                Y1 = y2;
                Y2 = y1;

            }
            else
            {
                Y1 = y1;
                Y2 = y2;
            }

        }

        public Prostokat BoundingRectangle => new Prostokat(X1, Y1, X2, Y2);
    }
    class Punkt : IFigura
    {
        public readonly int X, Y;


        public Punkt(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Prostokat BoundingRectangle => new Prostokat(X, Y, X, Y);
    }
}
