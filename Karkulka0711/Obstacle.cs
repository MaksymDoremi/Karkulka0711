using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Karkulka0711
{
    //enum for obstacle types
    enum ObstacleType 
    { 
        VYHLIDKA, 
        BLUDNY_KOREN, 
        KVETINOVA_LOUKA, 
        PASEKA_HRIBKU, 
        STRMY_SVAH,
        MOKRINA,
        VLK,
        BODLACI,
        HOME, //only once
        GRANNY //only once

    }

    internal class Obstacle
    {
        private ObstacleType _obstalceType;
        private int x;
        private int y;

        public Obstacle(int randomIndex, int y, int x)
        {
            SetObstacle(randomIndex);
            this.x = x;
            this.y = y;
        }

        public Obstacle(int y, int x)
        {
            //empty for home or granny
            this.x = x;
            this.y = y;
        }

        public int X
        {
            get { return x; }
            set { x = value; } 
        }

        public int Y
        {
            get
            {
                return y;
            }
            set
            {
                y = value;
            }
        }

        public ObstacleType GetObstacleType()
        {
            return _obstalceType;
        }

        public string StringObstacleType()
        {
            switch (_obstalceType)
            {

                case ObstacleType.HOME:
                    return "Home";
                case ObstacleType.GRANNY:
                    return "Granny";
                case ObstacleType.VYHLIDKA:
                    return "Vyhlidka";
                case ObstacleType.BLUDNY_KOREN:
                    return "Bludny Koren";
                case ObstacleType.KVETINOVA_LOUKA:
                    return "Kvetinova Louka";
                case ObstacleType.PASEKA_HRIBKU:
                    return "Paseka Hribku";
                case ObstacleType.STRMY_SVAH:
                    return "Strmy Svah";
                case ObstacleType.MOKRINA:
                    return "Mokrina";
                case ObstacleType.VLK:
                    return "Vlk";
                case ObstacleType.BODLACI:
                    return "Bodlaci";
                default:
                    return "?";
            }
        }
        public void SetObstacle(int index)
        {
            switch (index) 
            {
                case 1:
                    _obstalceType = ObstacleType.VYHLIDKA;
                    break;
                case 2:
                    _obstalceType = ObstacleType.BLUDNY_KOREN;
                    break;
                case 3:
                    _obstalceType = ObstacleType.KVETINOVA_LOUKA;
                    break;
                case 4:
                    _obstalceType = ObstacleType.PASEKA_HRIBKU;
                    break;
                case 5:
                    _obstalceType = ObstacleType.STRMY_SVAH;
                    break;
                case 6:
                    _obstalceType = ObstacleType.MOKRINA;
                    break;
                case 7:
                    _obstalceType = ObstacleType.VLK;
                    break;
                case 8:
                    _obstalceType = ObstacleType.BODLACI;
                    break;
            }
        }

        public void SetHome()
        {
            _obstalceType = ObstacleType.HOME;
        }

        public void SetGranny()
        {
            _obstalceType = ObstacleType.GRANNY;
        }

        public override string ToString()
        { 
            switch (_obstalceType)
            {
                //H is for home and G is for grandmother's home, ? is unknown for the obstacle
                case ObstacleType.HOME:
                    return "H";
                case ObstacleType.GRANNY:
                    return "G";
                default:
                    return "?";
            }
        }
    }
}
