using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karkulka0711
{
    internal class RedHat
    {
        private int x; //column
        private int y; //row

        private bool _gameOver = false;
        private bool _win = false;
        private bool _ranOutOfGifts = false;

        private bool sbiralaKvetiny = false;
        private bool sbiralaHouby = false;

        private List<string> gifts = new List<string>();

        public RedHat()
        {
            gifts.Add("vino");
            gifts.Add("babovka");
        }

        public int X
        {
            get { return x; }
            set { x = value; }
        }

        public int Y
        {
            get { return y; }
            set { y = value; }
        }

        public bool GameOver
        {
            get { return _gameOver; }
            set { _gameOver = value; }
        }

        public bool RanOutOfGifts
        {
            get { return _ranOutOfGifts;}
            set { _ranOutOfGifts = value;}
        }

        public bool Win
        {
            get { return _win; }
            set { _win = value; }
        }

        
        public void Loose()
        {
            GameOver = true;
        }

        public void GiveAGift()
        {
            if (gifts.Count > 0)
            {
                string gift = gifts[0];
                gifts.RemoveAt(0);
                Console.WriteLine("karkulka odevzda " + gift + " svemu pomocnikovi");
            }
            else
            {
                RanOutOfGifts = true;
                
                Console.WriteLine("Nezbylo zadneho darku");
            }

        }

        public string GatherAGift(Obstacle obstacle)
        {
            if (gifts.Count < 2)
            {
                if (obstacle.GetObstacleType() == ObstacleType.KVETINOVA_LOUKA && !sbiralaKvetiny)
                {
                    gifts.Add("kvetiny");
                    sbiralaKvetiny = true;
                    return "karkulka nasbirala kvetiny";
                }
                else if (obstacle.GetObstacleType() == ObstacleType.PASEKA_HRIBKU && !sbiralaHouby)
                {
                    gifts.Add("houby");
                    sbiralaHouby = true;
                    return "Karkulka nasbirala houby";
                }
            }
            return "uz ma u sebe 2 darky takze uz nic nenasbira nebo uz nesmi sbirat";

        }
    }
}
