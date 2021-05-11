using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hagen_Daniel_HH5VQ6_The_Witcher
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //LIST OF CONTRACTS OF CITIES
                ListOfContracts<Contract> HengforsContracts = new ListOfContracts<Contract>();
                HengforsContracts.InsertSorted(new Contract("werewolf", 10, 10));
                HengforsContracts.InsertSorted(new Contract("vampire", 5, 3));
                HengforsContracts.InsertSorted(new Contract("incubus", 6, 4));
                HengforsContracts.InsertSorted(new Contract("Evil", 2, 13));
                HengforsContracts.InsertSorted(new Contract("Genius", 4, 20));
                HengforsContracts.InsertSorted(new Contract("Sargon", 6, 22));
                HengforsContracts.InsertSorted(new Contract("Balor", 1, 34));
                HengforsContracts.InsertSorted(new Contract("Mortis", 1, 10));

                ListOfContracts<Contract> BlavikenContracts = new ListOfContracts<Contract>();
                BlavikenContracts.InsertSorted(new Contract("Shiey", 9, 14));
                BlavikenContracts.InsertSorted(new Contract("Anton", 2, 20));
                BlavikenContracts.InsertSorted(new Contract("Bad Cat", 4, 14));
                BlavikenContracts.InsertSorted(new Contract("Urban Odyssey", 6, 17));
                BlavikenContracts.InsertSorted(new Contract("DanFreedom", 1, 30));
                BlavikenContracts.InsertSorted(new Contract("Urbex Lifts", 7, 77));
                
                ListOfContracts<Contract> GhelibolContracts = new ListOfContracts<Contract>();
                GhelibolContracts.InsertSorted(new Contract("Harald Baldr", 9, 8));
                GhelibolContracts.InsertSorted(new Contract("bald and bankrupt", 7, 6));
                GhelibolContracts.InsertSorted(new Contract("PPPeter", 4, 2));
                GhelibolContracts.InsertSorted(new Contract("TrevorJacob", 1, 10));

                ListOfContracts<Contract> RoggevenContracts = new ListOfContracts<Contract>();
                RoggevenContracts.InsertSorted(new Contract("Супер Сус", 3, 1));
                RoggevenContracts.InsertSorted(new Contract("KREOSAN", 2, 9));
                RoggevenContracts.InsertSorted(new Contract("RoggevenMonster3", 1, 8));
                RoggevenContracts.InsertSorted(new Contract("RoggevenMonster4", 1, 10));

                ListOfContracts<Contract> NovigradContracts = new ListOfContracts<Contract>();
                NovigradContracts.InsertSorted(new Contract("NovigradMonster1", 3, 5));
                NovigradContracts.InsertSorted(new Contract("NovigradMonster2", 5, 4));
                NovigradContracts.InsertSorted(new Contract("NovigradMonster3", 4, 3));
                NovigradContracts.InsertSorted(new Contract("NovigradMonster4", 1, 2));
                NovigradContracts.InsertSorted(new Contract("NovigradMonster5", 1, 13));

                ListOfContracts<Contract> TretogorContracts = new ListOfContracts<Contract>();
                TretogorContracts.InsertSorted(new Contract("TretogorMonster1", 1, 26));
                TretogorContracts.InsertSorted(new Contract("TretogorMonster2", 2, 2));
                TretogorContracts.InsertSorted(new Contract("TretogorMonster3", 3, 3));
                TretogorContracts.InsertSorted(new Contract("TretogorMonster4", 4, 4));
                TretogorContracts.InsertSorted(new Contract("TretogorMonster5", 5, 5));
                TretogorContracts.InsertSorted(new Contract("TretogorMonster6", 1, 12));

                ListOfContracts<Contract> OxenfurtContracts = new ListOfContracts<Contract>();
                OxenfurtContracts.InsertSorted(new Contract("OxenfurtMonster1", 1, 12));
                OxenfurtContracts.InsertSorted(new Contract("OxenfurtMonster2", 1, 11));

                ListOfContracts<Contract> RinbeContracts = new ListOfContracts<Contract>();
                RinbeContracts.InsertSorted(new Contract("RinbeMonster1", 1, 13));
                RinbeContracts.InsertSorted(new Contract("RinbeMonster2", 1, 14));                                  
                RinbeContracts.InsertSorted(new Contract("RinbeMonster3", 1, 10));



                // CREATING THE GRAPH

                Logic<ListOfContracts<Contract>, string> MapV2 = new Logic<ListOfContracts<Contract>, string>();
                MapV2.AddVertex(HengforsContracts, "Hengfors");
                MapV2.AddVertex(BlavikenContracts, "Blaviken");
                MapV2.AddVertex(GhelibolContracts, "Ghelibol");
                MapV2.AddVertex(RoggevenContracts, "Roggeven");
                MapV2.AddVertex(NovigradContracts, "Novigrad");
                MapV2.AddVertex(TretogorContracts, "Tretogor");
                MapV2.AddVertex(OxenfurtContracts, "Oxenfurt");
                MapV2.AddVertex(RinbeContracts, "Rinbe");

                MapV2.AddEdge("Hengfors", "Blaviken");
                MapV2.AddEdge("Blaviken", "Ghelibol");
                MapV2.AddEdge("Blaviken", "Roggeven");
                MapV2.AddEdge("Roggeven", "Ghelibol");
                MapV2.AddEdge("Roggeven", "Novigrad");
                MapV2.AddEdge("Roggeven", "Tretogor");
                MapV2.AddEdge("Novigrad", "Oxenfurt");
                MapV2.AddEdge("Oxenfurt", "Tretogor");
                MapV2.AddEdge("Tretogor", "Rinbe");

                Console.Write("Initial city: ");
                string startCity = Console.ReadLine();
                Console.Write("Days of adventure: ");
                int lengthOfAdventure = Convert.ToInt32(Console.ReadLine());

                MapV2.OnTheSpotEvent += MapV2.Adventure;

                try
                {
                    MapV2.Greedy(MapV2, startCity, lengthOfAdventure);
                }
                catch (InvalidCityException)
                {
                    Console.WriteLine("The city you want to start your journey does not exist.");
                }
            }
            catch (OutOfHazardRangeException)
            {
                Console.WriteLine("At least one of the quests is corrupted.");
            }
            Console.ReadKey();
        }
    }
}