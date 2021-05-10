using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hagen_Daniel_HH5VQ6_The_Witcher
{
    class Contract : Monster, IComparable<Contract>
    {
        protected int goldReward;

        public int GoldReward { get { return goldReward; } }
        public Contract(string monsterType, int hazard, int gold) : base (monsterType, hazard)
        {
            this.goldReward = gold;
        }

        public int CompareTo(Contract other)
        {
            return goldReward.Equals(other.GoldReward) ? -(hazardLevel.CompareTo(other.HazardLevel)) : goldReward.CompareTo(other.GoldReward); 
        }
    }
}
