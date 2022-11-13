using Karkulka0711;


namespace Karkulka
{
    internal class Program
    {
        public static void Main(string[] args)
        {

            //init
            RedHat karkulka = new RedHat();
            Map map = new Map(10, 10, karkulka);

            map.GenerateMap();
            map.GenerateHomeAndGranny();

            //game loop
            while (!karkulka.GameOver)
            {
                map.Presmerovani = false;
                Console.Clear();
                Console.WriteLine(map);
                Console.WriteLine(map.Current.StringObstacleType() + " " + map.Current.X + " " + map.Current.Y);

                Console.WriteLine(map.CheckCurrent());

                if (karkulka.Win)
                {
                    Console.WriteLine("Super babicka dostane svoje darky");
                    break;
                }
                if (karkulka.RanOutOfGifts)
                {
                    Console.WriteLine("Neni darku, prohral jsi");
                    break;
                }
                if (karkulka.GameOver)
                {
                    Console.WriteLine("Vlk sezral vsech");
                    break;
                }

                if (!map.Presmerovani)
                {
                    switch (Console.ReadKey().Key)
                    {
                        case ConsoleKey.W:
                            map.Up();
                            break;
                        case ConsoleKey.A:
                            map.Left();
                            break;
                        case ConsoleKey.S:
                            map.Down();
                            break;
                        case ConsoleKey.D:
                            map.Right();
                            break;
                    }
                }
                else
                {
                    Thread.Sleep(1000);
                }
            }
        }
    }
}