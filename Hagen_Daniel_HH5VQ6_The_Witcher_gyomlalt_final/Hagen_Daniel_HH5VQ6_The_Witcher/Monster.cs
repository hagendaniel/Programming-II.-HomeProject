using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hagen_Daniel_HH5VQ6_The_Witcher
{
    class Monster
    {
        protected string monsterType;
        protected int hazardLevel;


        public string MonsterType { get { return monsterType; } }
        public int HazardLevel { get { return hazardLevel; } }


        public Monster(string monsterType, int hazard)
        {
            if (hazard < 0 || hazard > 10) throw new OutOfHazardRangeException("Hazard level must be between 0 and 10");
            else
            {
                this.hazardLevel = hazard;
                this.monsterType = monsterType;
                
            }
        }
    }
}
