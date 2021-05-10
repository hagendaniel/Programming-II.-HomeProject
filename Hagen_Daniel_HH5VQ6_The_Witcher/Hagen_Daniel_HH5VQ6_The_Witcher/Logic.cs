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
                //string[] labels = new string[days]; 
                _Greedy(ref currentMap/*,ref labels*/, GetVertexFromLabel(position), days);
                //return labels;
            }
            else
            {
                throw new InvalidCityException("Starting position does not exists");
            }
        }

        public delegate void Journey(string cityname, Contract contractname);
        //public delegate void OnTheWay(string cityname);
        public event Journey OnTheSpotEvent;
        //public event OnTheWay OnTheWayEvent;
        private void _Greedy(ref CitiesGraphV2<ListOfContracts<Contract>, string> currentMap/*,ref string[] labels*/, Vertex CurrentPosition, int days)
        {
            if (HasContract(CurrentPosition) == true)
            {
                if (days > 2)
                {
                    if (BetterNeighbor(currentMap, CurrentPosition) /*&&*/ ) //if the current position has a better contract then the neighbors - //&& CURRENTPOSITIONNEK MÉG VAN CONTRACTJA - NEM EZ NEM KELL
                    {
                        //labels[days - 1] = CurrentPosition.label;
                        //B lehetőség 
                        OnTheSpotEvent?.Invoke(CurrentPosition.label, CurrentPosition.GetCurrentContract());
                        currentMap.RemoveFirstContractFromVertex(CurrentPosition);
                        //CurrentPosition.RemoveFirstContract(); - TALÁN EZ A SZAR - DUPLÁN TÖRÖLTE AZ ELSŐ KONTRAKTOKAT

                        _Greedy(ref currentMap/*, ref labels*/, CurrentPosition, days - 1);

                    }
                    else
                    {
                        // Get a better description
                        //labels[days - 1] = "OnTheWayToTheNextCity";
                        _Greedy(ref currentMap/*, ref labels*/, NextHop(currentMap, CurrentPosition), days - 1);
                        OnTheSpotEvent?.Invoke(CurrentPosition.label, null);
                        //OnTheWayEvent?.Invoke(CurrentPosition.label);
                        //Console.WriteLine($"You have to travel to {CurrentPosition.label}");
                    }
                }
                else if (days > 0)
                {
                    if (BetterNeighborLastTwoDays(currentMap, CurrentPosition) /*&& HasContract(CurrentPosition)==true*/) //if the current position has a better contract then the neighbors - //&& CURRENTPOSITIONNEK MÉG VAN CONTRACTJA - NEM EZ NEM KELL
                    {
                        //labels[days - 1] = CurrentPosition.label;
                        //B lehetőség 
                        OnTheSpotEvent?.Invoke(CurrentPosition.label, CurrentPosition.GetCurrentContract());
                        currentMap.RemoveFirstContractFromVertex(CurrentPosition);
                        //CurrentPosition.RemoveFirstContract(); - TALÁN EZ A SZAR - DUPLÁN TÖRÖLTE AZ ELSŐ KONTRAKTOKAT

                        _Greedy(ref currentMap/*, ref labels*/, CurrentPosition, days - 1);

                    }
                    else
                    {
                        // Get a better description
                        //labels[days - 1] = "OnTheWayToTheNextCity";
                        _Greedy(ref currentMap/*, ref labels*/, NextHop(currentMap, CurrentPosition), days - 1);
                        //OnTheWayEvent?.Invoke(CurrentPosition.label);
                        //Console.WriteLine($"You have to travel to {CurrentPosition.label}");
                        OnTheSpotEvent?.Invoke(CurrentPosition.label, null);
                    }
                }
            }
            else
            {
                _Greedy(ref currentMap/*, ref labels*/, NextHop(currentMap, CurrentPosition), days - 1);
                //OnTheWayEvent?.Invoke(CurrentPosition.label);
                //Console.WriteLine($"You have to travel to {CurrentPosition.label}");
                OnTheSpotEvent?.Invoke(CurrentPosition.label, null);
            }
        }

        private bool ValidCity(string cityname)
        {
            bool retur=false;
            foreach (Vertex v in vertices)
            {
                if (v.label == cityname) retur = true;
            }
            return retur;
        }
        private bool HasContract(Vertex position)
        {
            /*bool toReturn = false;
            try
            {
                if (position.data.Current != null) toReturn = true;
            }
            catch (NullReferenceException)
            {
                if (position.data.Current == null) toReturn = false;
            }
            return toReturn;*/
            //if (position.data.Current != null) return true;
            //else return false;
            if (position.data.Count() < 1) return false;
            else return true;
        }

        //egy olyan metódus ami megmondja, hogy a kiszemelt váors elsője nagyobb-e mint a jelenleg város első kettője - CSAK AKKOR HASZNÁLJUK HOGYHA MÁRCSAK 2 NAP UGRÁSUNK VAN
        //TRUE HA MEGÉRI TOVÁBBÁLLNI
        private bool TwoForOne(Vertex position, Vertex q)
        {
            if (position.data.Count() > 1)
            {
                if (position.data[0].GoldReward / position.data[0].HazardLevel + position.data[1].GoldReward / position.data[1].HazardLevel > q.data[0].GoldReward / q.data[0].HazardLevel)
                {
                    return false;
                }
                else return true;
            }
            else return true;
        }
        private bool BetterPotential(Vertex position, Vertex questionablePosition) //Returns true if the candidate city has better potentials
        {
            int posGold=0;
            int posHazard=0;
            int queGold=0;
            int queHazard=0;
            /*foreach (Contract c in position.data)
            {
                posGold += c.GoldReward;
                posHazard += c.HazardLevel;
            }*/
            for (int i = 0; i < position.data.Count(); i++)
            {
                posGold += position.data[i].GoldReward;
                posHazard += position.data[i].HazardLevel;
            }
            /*foreach (Contract c in questionablePosition.data)
            {
                queGold += c.GoldReward;
                queHazard += c.HazardLevel;
            }*/
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
            //return posGold / posHazard > queGold / queHazard; 
        }
        private Vertex NextHop(CitiesGraphV2<ListOfContracts<Contract>, string> currentMap, Vertex position)
        {
            if (HasContract(position) == true)//IF IT HAS AT LEAST ONE CONTRACT
            {
                Vertex maxVertex = position;
                //List<Vertex> neighbors = GetNeighborVertices(position.label); /////////////////////////TESZTELÉS
                foreach (Vertex neighbor in /*neighbors*/position.neighbors) /////////////////////////////////////////////////
                {/////////////////////////////////////////////////////////////////////////////
                    if (HasContract(neighbor) && ValueOfVertex(neighbor) > ValueOfVertex(maxVertex))////////////////////////////
                    {///////////////////////////////////////////////////////////////////////
                        maxVertex = neighbor;////////////////////////////////////////////////////////
                    }///////////////////////////////////////////////////////////////////////
                }
                if (maxVertex != position)
                {
                    if (BetterPotential(position, maxVertex) == true) return maxVertex;
                    else return position;
                }
                else return maxVertex;
            }
            else //if (HasANeighborWithContract(position) != position) //IF IT DOES NOT BUT ONE OF ITS NEIGHBOR HAVE AT LEAST A CONTRACT
            {
                Vertex maxVertex = ReturnANeighborWithContract(position);
                //List<Vertex> neighbors = GetNeighborVertices(position.label); /////////////////////////TESZTELÉS
                foreach (Vertex neighbor in /*neighbors*/position.neighbors) /////////////////////////////////////////////////
                {/////////////////////////////////////////////////////////////////////////////
                    if (ValueOfVertex(neighbor) > ValueOfVertex(maxVertex))////////////////////////////
                    {///////////////////////////////////////////////////////////////////////
                        maxVertex = neighbor;////////////////////////////////////////////////////////
                    }///////////////////////////////////////////////////////////////////////
                }
                return maxVertex;
            }
        } //csak akkor utazik bármelyik szomszédba, ha az a kieső nap veszteségét az ott lévő jutalmakkal kompenzálni tudja - azaz nagyobb a potenciál egy másik városban

        private Vertex ReturnANeighborWithContract(Vertex position)
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
            return vertexToReturn;
        }

        private double ValueOfVertex(Vertex position)
        {
            double sumOfGold = 0;
            double sumOfHazards = 0;

            foreach (Contract contract in position.data)
            {
                sumOfGold += contract.GoldReward;
                sumOfHazards += contract.HazardLevel;
            }

            return sumOfGold / sumOfHazards;
        }

        private bool BetterNeighbor(CitiesGraphV2<ListOfContracts<Contract>, string> currentMap, Vertex position)
        {
            return NextHop(currentMap, position).label == position.label; //If the nextneighbor which compares position vertex contracts with position's neighbor contracts returnes the same vertex, it return true
        }
        private bool BetterNeighborLastTwoDays(CitiesGraphV2<ListOfContracts<Contract>, string> currentMap, Vertex position)
        {
            if (NextHop(currentMap, position).label == position.label)
            {
                return true; //If the nextneighbor which compares position vertex contracts with position's neighbor contracts returnes the same vertex, it return true
            }
            else if (TwoForOne(position, NextHop(currentMap, position)))
            {
                return false;
            }
            else { return false; }
        }

        public void DoQuest(string cityname, Contract contractname)
        {
            if (contractname == null)
            {
                Console.Write($"\n You have to travel to {cityname}.");
            }
            else
            {
                Console.Write($"\nYou have to complete your next quest (defeat a(n) {contractname.MonsterType} for {contractname.GoldReward} golds with the hazard level of {contractname.HazardLevel}) in {cityname}.");

            }
        }
        //public void TravelTo(string cityname)
        //{
        //    Console.Write($"\nYou have to travel to {cityname}.");
        //}

        ////public void Adventure(/*CitiesGraphV2<ListOfContracts<Contract>, string> map,*/ TLabel position/*, int days*/)
        ////{
        ////    Vertex CurrentPosition = GetVertexFromLabel(position);
        ////    ListOfContracts<Contract> CurrentContracts = new ListOfContracts<Contract>();
        ////    ListOfContracts<Contract> NeighbourContracts = new ListOfContracts<Contract>();
        ////    ListOfContracts<Contract> ContractsAlreadyDone = new ListOfContracts<Contract>();
        ////    //az összes szomszéd összes contractja bemegy egy listába - a listát rendezem csökkenőbe, ha utazni kell a legnagyobb fele indul - nem ez nem is kell
        ////    List<Vertex> NeighborVertices = GetNeighborVertices(CurrentPosition.label);
        ////    foreach (Vertex v in NeighborVertices)
        ////    {
        ////        ListOfContracts<Contract> ContractsOfVertexVCity = v.data as ListOfContracts<Contract>;
        ////        {
        ////        foreach (Contract c in ContractsOfVertexVCity)
        ////            NeighbourContracts.InsertSorted(c);
        ////        }
        ////    }
        ////    //az összes contract az aktuális helyen
        ////    foreach (Contract c in CurrentPosition.data as ListOfContracts<Contract>)
        ////    {
        ////        CurrentContracts.InsertSorted(c);
        ////    }

        ////    if (StayOrLeave(CurrentContracts, NeighbourContracts, ContractsAlreadyDone) == true) //so we're staying
        ////    {
        ////        // Doing the best contract
        ////        Contract BestContract = BestContractOfACity(CurrentContracts, ContractsAlreadyDone);
        ////        Console.WriteLine($"You have to complete {BestContract} at your current position.");
        ////        //CONTRACTOT EZUTÁN TÖRÖLNI A LÁNCOLT LISTÁJÁBÓL - EHELYETT ALREADYDONE AZAZ NEM VÁLASZTHATÓ
        ////        ContractsAlreadyDone.InsertSorted(BestContract);
        ////    }
        ////    else
        ////    {
        ////        Contract BestContractNearby = BestContractOfACity(NeighbourContracts, ContractsAlreadyDone);
        ////        Vertex Destination = WhichWayYouGo(BestContractNearby, CurrentPosition);
        ////        Console.WriteLine($"You have to travel to {Destination} and complete {BestContractNearby}");
        ////        //ALREADYDONE AZAZ NEM VÁLASZTHATÓ
        ////        ContractsAlreadyDone.InsertSorted(BestContractNearby);
        ////        CurrentPosition = Destination;
        ////    }
        ////}

        ////private Vertex WhichWayYouGo(Contract contractToCompleteAfterTravelling, Vertex position)
        ////{
        ////    List<Vertex> NeighborVertices = GetNeighborVertices(position.label);
        ////    Vertex theWayYouGo=null;
        ////    foreach (Vertex v in NeighborVertices)
        ////    {
        ////        foreach (Contract c in v.data as ListOfContracts<Contract>)
        ////        {
        ////            if (c == contractToCompleteAfterTravelling) theWayYouGo = v ;
        ////        }
        ////    }
        ////    return theWayYouGo;
        ////}

        ////private bool IsAvailable(Contract contract, ListOfContracts<Contract> banned)
        ////{
        ////    int i = 0;
        ////    foreach (Contract c in banned)
        ////    {
        ////        if (c == contract) i++;
        ////    }
        ////    if (i != 0) return false;
        ////    else return true;
        ////}

        ////private bool StayOrLeave (ListOfContracts<Contract> CurrentContracts, ListOfContracts<Contract> NeighborContracts, ListOfContracts<Contract> No) //Complete a quest here or travel to the best offer | true = stay
        ////{
        ////    Contract BestContractofCurrentPosition = BestContractOfACity(CurrentContracts, No);
        ////    Contract BestContractofANeighborPosition = BestContractOfACity(NeighborContracts, No);
        ////    if (BestContractofCurrentPosition.GoldReward > BestContractofANeighborPosition.GoldReward)
        ////    {
        ////        return true;
        ////    }
        ////    else if (BestContractofCurrentPosition.GoldReward > BestContractofANeighborPosition.GoldReward / 2)
        ////    {
        ////        return true;
        ////    }
        ////    else// if (BestContractofCurrentPosition.GoldReward < BestContractofANeighborPosition.GoldReward / 2)
        ////    {
        ////        return false;
        ////    }
        ////}

        ////private Contract BestContractOfACity(ListOfContracts<Contract> Contracts, ListOfContracts<Contract> No)
        ////{
        ////    Contract best = Contracts[0];
        ////    foreach (Contract c in Contracts)
        ////    {
        ////        if ((IsAvailable(c,No)==true)&&(c.GoldReward > best.GoldReward))
        ////        {
        ////            best = c;
        ////        }
        ////    }
        ////    return best;
        ////}


    }
}
