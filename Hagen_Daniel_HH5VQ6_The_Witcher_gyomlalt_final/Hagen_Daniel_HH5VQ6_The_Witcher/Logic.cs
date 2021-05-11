using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hagen_Daniel_HH5VQ6_The_Witcher
{
    class Logic<TValue, TLabel> : CitiesGraphV2<ListOfContracts<Contract>, string>
    {

        public void Greedy(CitiesGraphV2<ListOfContracts<Contract>, string> map, string position, int days)
        {
            if (ValidCity(position) == true)
            {
                CitiesGraphV2<ListOfContracts<Contract>, string> currentMap = map;
                _Greedy(ref currentMap, GetVertexFromLabel(position), days);
            }
            else
            {
                throw new InvalidCityException("Starting position does not exists");
            }
        }

        public delegate void Journey(string cityname, Contract contractname);
        public event Journey OnTheSpotEvent;
        private void _Greedy(ref CitiesGraphV2<ListOfContracts<Contract>, string> currentMap, Vertex CurrentPosition, int days)
        {
            if (HasContract(CurrentPosition) == true && days > 0)
            {
                if (days > 2)
                {
                    if (BetterNeighbor(currentMap, CurrentPosition)) //TRUE - if the current position has a better contract then the neighbors
                    {
                        OnTheSpotEvent?.Invoke(CurrentPosition.label, CurrentPosition.GetCurrentContract());
                        currentMap.RemoveFirstContractFromVertex(CurrentPosition);
                        _Greedy(ref currentMap, CurrentPosition, days - 1);
                    }
                    else
                    {
                        OnTheSpotEvent?.Invoke(NextHop(currentMap, CurrentPosition).label, null);
                        _Greedy(ref currentMap, NextHop(currentMap, CurrentPosition), days - 1);
                    }
                }
                else if (days > 1)
                {
                    if (BetterNeighborLastTwoDays(currentMap, CurrentPosition)) //TRUE - if the current position has a better contract than the neighbors
                    {
                        OnTheSpotEvent?.Invoke(CurrentPosition.label, CurrentPosition.GetCurrentContract());
                        currentMap.RemoveFirstContractFromVertex(CurrentPosition);
                        _Greedy(ref currentMap, CurrentPosition, days - 1);

                    }
                    else
                    {
                        OnTheSpotEvent?.Invoke(NextHop(currentMap, CurrentPosition).label, null);
                        _Greedy(ref currentMap, NextHop(currentMap, CurrentPosition), days - 1);
                    }
                }
                else if (days > 0)
                {
                    if (HasContract(CurrentPosition) == true) // TRUE - if the current position have at least a contract
                    {
                        OnTheSpotEvent?.Invoke(CurrentPosition.label, CurrentPosition.GetCurrentContract());
                        currentMap.RemoveFirstContractFromVertex(CurrentPosition);
                        _Greedy(ref currentMap, CurrentPosition, days - 1);

                    }
                    else // We don't even have to try travelling, because that would just use up our last day
                    {
                        Console.WriteLine("\nThis is the end of your journey");
                    }
                }
            }
            else if (days > 1) //It should be at least two, because if we travel to an adjacent city it would become 0 in case of 1 - Also if the vurrent possition does not have any contract, it moves in direction of the best neighbor
            {
                OnTheSpotEvent?.Invoke(NextHop(currentMap, CurrentPosition).label, null);
                _Greedy(ref currentMap, NextHop(currentMap, CurrentPosition), days - 1);
            }
            else
            {
                Console.WriteLine("\nThis is the end of your journey.");
            }
        }
        private bool ValidCity(string cityname) // TRUE - If the given city exists
        {
            bool toReturn=false;
            foreach (Vertex v in vertices)
            {
                if (v.label == cityname) toReturn = true;
            }
            return toReturn;
        }
        private bool HasContract(Vertex position) // TRUE - If the given vertex has at least one contract
        {
            if (position.data.Count() < 1) return false;
            else return true;
        }
        private bool TwoForOne(Vertex position, Vertex q) // TRUE - If it makes sense to travel to another city in the last two days
        {                                                 // It examines if we rather travel the questionable(candidate) city and do a quest or do two quests at our current position 
            if (position.data.Count() > 1)
            {
                if (position.data[0].GoldReward / position.data[0].HazardLevel + position.data[1].GoldReward / position.data[1].HazardLevel > q.data[0].GoldReward / q.data[0].HazardLevel)
                {
                    return false;
                }
                else return true;
            }
            else if (position.data.Count() == 1)
            {
                if (position.data[0].GoldReward / position.data[0].HazardLevel > q.data[0].GoldReward / q.data[0].HazardLevel)
                {
                    return false;
                }
                else return true;
            }
            else return true;
        }
        private bool BetterPotential(Vertex position, Vertex questionablePosition) // TRUE - if the candidate city has better potentials
        {
            int posGold=0;
            int posHazard=0;
            int queGold=0;
            int queHazard=0;
            for (int i = 0; i < position.data.Count(); i++)
            {
                posGold += position.data[i].GoldReward;
                posHazard += position.data[i].HazardLevel;
            }
            for (int i = 0; i < questionablePosition.data.Count(); i++)
            {
                queGold += questionablePosition.data[i].GoldReward;
                queHazard += questionablePosition.data[i].HazardLevel;
            }
            if (posGold / posHazard < queGold / queHazard)
            {
                return true;
            }
            else { return false; }
        }
        private Vertex NextHop(CitiesGraphV2<ListOfContracts<Contract>, string> currentMap, Vertex position) // Returns the best option as a next stop - SAME as position IF position is the best yet. | Position's last neighbor if neither position, nor its neighbors have any contract
        {
            if (HasContract(position) == true)// IF IT HAS AT LEAST ONE CONTRACT
            {
                //Maximum search
                Vertex maxVertex = position;
                foreach (Vertex neighbor in position.neighbors)
                {
                    if (HasContract(neighbor) && ValueOfVertex(neighbor) > ValueOfVertex(maxVertex))
                    {
                        maxVertex = neighbor;
                    }
                }
                if (maxVertex != position && TwoForOne(position, maxVertex) == true) // Examines if it worth to travel to the found maxVertex (neighbor of the current position)
                {
                    if (BetterPotential(position, maxVertex) == true) return maxVertex;
                    else if (maxVertex.data[0].GoldReward / maxVertex.data[0].HazardLevel > position.data[0].GoldReward / position.data[0].HazardLevel) return maxVertex;
                    else return position;
                }
                else if (TwoForOne(position, maxVertex) == false)
                {
                    return position;
                }
                else return maxVertex;
            }
            else //IF IT DOES NOT BUT ONE OF ITS NEIGHBOR HAS AT LEAST A CONTRACT
            {
                Vertex maxVertex = ReturnANeighborWithContract(position);
                foreach (Vertex neighbor in position.neighbors)
                {
                    if (neighbor.data.Count() > 0)
                    {
                        if (ValueOfVertex(neighbor) > ValueOfVertex(maxVertex))
                        {
                            maxVertex = neighbor;
                        }
                    }
                }
                if (maxVertex == position) // Neither position, nor its neighbors' have any contracts
                {
                    maxVertex = position.neighbors[position.neighbors.Count()-1]; // It sets maxVertex to the last neighbor of position
                }
                return maxVertex;
            }
        } //csak akkor utazik bármelyik szomszédba, ha az a kieső nap veszteségét az ott lévő jutalmakkal kompenzálni tudja - azaz nagyobb a potenciál egy másik városban
        private Vertex ReturnANeighborWithContract(Vertex position) // It just gives a vertex with at least a contract --> We can use it in NextHop method, if that position does not have any contracts
        {
            Vertex vertexToReturn=position;
            for (int i = 0; i < position.neighbors.Count(); i++)
            {
                if (HasContract(position.neighbors[i]))
                {
                    vertexToReturn = position.neighbors[i];
                    break;
                }
            }
            return vertexToReturn; //if it is the same as position, than none of position's neigbors' have any contracts
        }

        private double ValueOfVertex(Vertex position) // It returns the real value of a city's contract (ratio of golds/hazards)
        {
            double sumOfGolds = 0;
            double sumOfHazards = 0;

            foreach (Contract contract in position.data)
            {
                sumOfGolds += contract.GoldReward;
                sumOfHazards += contract.HazardLevel;
            }

            return sumOfGolds / sumOfHazards;
        }
        private bool BetterNeighbor(CitiesGraphV2<ListOfContracts<Contract>, string> currentMap, Vertex position) // TRUE - If the current position has the best quest | FALSE - If the witcher should move on
        {
            return NextHop(currentMap, position).label == position.label;
        }
        private bool BetterNeighborLastTwoDays(CitiesGraphV2<ListOfContracts<Contract>, string> currentMap, Vertex position) // TRUE - If the best option is still choosing the highest gold quests (2) from current position
        {                                                                                                                    // FALSE - If the witcher is better to travel for a day and do the highest gold quest in the candidate city
            if (NextHop(currentMap, position).label == position.label)
            {
                return true;
            }
            else if (TwoForOne(position, NextHop(currentMap, position)))
            {
                return false;
            }
            else { return false; }
        }
        public void Adventure(string cityname, Contract contractname) // A simple printing method fired by the event
        {
            if (contractname == null)
            {
                Console.Write($"\nYou have to travel to {cityname}.");
            }
            else
            {
                Console.Write($"\nYou have to complete your next quest (defeat a(n) {contractname.MonsterType} for {contractname.GoldReward} golds with the hazard level of {contractname.HazardLevel}) in {cityname}.");

            }
        }
    }
}
