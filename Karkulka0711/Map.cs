using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Transactions;

namespace Karkulka0711
{
    internal class Map
    {

        private int _width; //X
        private int _height; //Y

        //two dimensional array for obstacles
        private Obstacle[,] map;

        //current obstacle
        private Obstacle current;

        //instance of the karkulka
        private RedHat karkulka;

        //essential booleans
        private bool karkulkaMeetWolf = false;
        private bool presmerovani = false;

        public Map(int _height, int _width, RedHat karkulkaInstance)
        {
            Height = _height;
            Width = _width;
            map = new Obstacle[Height, Width];

            karkulka = karkulkaInstance;

            //firstly generate map
            GenerateMap();
            //then insert home and granny just once
            GenerateHomeAndGranny();

            //set karkulka location
            karkulka.X = current.X;
            karkulka.Y = current.Y;
        }

        public int Width
        {
            get
            {
                return _width;
            }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException("width can't be less than zero");
                }
                _width = value;
            }
        }

        public int Height
        {
            get
            {
                return _height;
            }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException("height can't be less than zero");
                }
                _height = value;
            }
        }

        public bool Presmerovani
        {
            get { return presmerovani; }
            set { presmerovani = value; }
        }

        public void Up()
        {
            if (current.Y - 1 >= 0)
            {
                //current.Y = current.Y - 1;
                current = map[current.Y - 1, current.X];

            }

        }

        public void Down()
        {
            if (current.Y + 1 < Height)
            {
                //current.Y = current.Y + 1;
                current = map[current.Y + 1, current.X];

            }
        }

        public void Left()
        {
            if (current.X - 1 >= 0)
            {
                //current.X = current.X - 1;
                current = map[current.Y, current.X - 1];

            }
        }

        public void Right()
        {
            if (current.X + 1 < Width)
            {
                //current.X = current.X + 1;
                current = map[current.Y, current.X + 1];
            }

        }

        public object? CheckCurrent()
        {
            //throw method for vyhlidka
            if (current.GetObstacleType() == ObstacleType.VYHLIDKA)
            {
                return Vyhlidka();
            }
            //throw method for random replacement and set the boolean on true to prevent ReadKey in the Program.cs
            else if (current.GetObstacleType() == ObstacleType.BLUDNY_KOREN)
            {
                Presmerovani = true;
                return BludnyKoren();
            }
            //method for wolf
            else if (current.GetObstacleType() == ObstacleType.VLK)
            {
                return Vlk();
            }
            //place to gather gifts if needed
            else if (current.GetObstacleType() == ObstacleType.KVETINOVA_LOUKA || current.GetObstacleType() == ObstacleType.PASEKA_HRIBKU)
            {
                //if karkulka already met a wolf so she is gonna die
                if (!karkulkaMeetWolf)
                {

                    return "Karkulka pujde sbirat neco\n" + karkulka.GatherAGift(current);
                }
                else
                {
                    karkulka.Loose();
                    return "Karkulka se zdrzela na " + current.StringObstacleType() + " proto prohrala sorry";

                }
            }
            //karkulka's home
            else if (current.GetObstacleType() == ObstacleType.HOME)
            {
                return "Jses doma, jdi k babicce";
            }
            //here is the win situation, karkulka met granny
            else if (current.GetObstacleType() == ObstacleType.GRANNY)
            {
                karkulka.Win = true;
                return "Vyhra";
            }
            //else if for toher obstacles which generate questions to answer
            else
            {
                GiveQuestion();
                return null;
            }


        }

        public void GiveQuestion()
        {
            //teoretciky tady ma byt hodne otazek ale udelam pouze jednu
            Console.WriteLine("Otazka. Jaka je spravna odpoved?:\n1) 2+2=4\n2)2+2=0\n3) 3+4=2");
            if (Console.ReadLine().Equals("1"))
            {
                Console.WriteLine("Super jdi dal");
            }
            else
            {
                Console.WriteLine("Odpoved je spatna takze odevzdej darek svemu pomocnikovi a muzes jit dal");
                karkulka.GiveAGift();
            }

        }

        public string Vyhlidka()
        {
            return "Vyhlidka je super misto";
        }
        public string BludnyKoren()
        {
            Random rnd = new Random();
            //randomly set current
            int x = rnd.Next(0, Width);
            int y = rnd.Next(0, Height);

            current = map[y, x];

            return "Bludny koren presmeruje na " + current.StringObstacleType();
        }

        public string Vlk()
        {
            karkulkaMeetWolf = true;
            return "Potkala jsi vlka, tak pospes si honem";
        }

        //generate Obstacle
        public void GenerateMap()
        {
            Random rnd = new Random();
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    map[i, j] = new Obstacle(rnd.Next(1, 9), i, j);
                }
            }
        }

        public void GenerateHomeAndGranny()
        {
            Random rnd = new Random();

            int x = rnd.Next(0, Width);
            int y = rnd.Next(0, Height);

            Obstacle home = new Obstacle(y, x);
            home.SetHome();
            map[y, x] = home;

            //set current obstacle to home
            current = home;

            x = rnd.Next(0, Width);
            y = rnd.Next(0, Height);

            while (x == home.X && y == home.Y)
            {
                x = rnd.Next(0, Width);
                y = rnd.Next(0, Height);
            }

            Obstacle granny = new Obstacle(y, x);
            granny.SetGranny();

            map[y, x] = granny;
        }
        public Obstacle Current
        {
            get
            {
                return current;
            }
        }
        public override string ToString()
        {
            string result = "";
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    if (i == current.Y && j == current.X)
                    {
                        result += "K | ";
                    }
                    else
                    {
                        result += map[i, j] + " | ";
                    }

                }
                result += "\n";
            }
            return result;

        }
    }
}
