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
              //Creating Contracts for a city

            ListOfContracts<Contract> placeContract = new ListOfContracts<Contract>();
            // placeContract.InsertToFront(new Contract("werewolf", 15, 10)); HAZARDEXCEPTION TEST

            //LIST OF CONTRACTS OF CITIES

            ListOfContracts<Contract> HengforsContracts = new ListOfContracts<Contract>();
            HengforsContracts.InsertSorted(new Contract("werewolf", 10, 10));
            HengforsContracts.InsertSorted(new Contract("vampire", 5, 3));
            HengforsContracts.InsertSorted(new Contract("incubus", 6, 4));
            HengforsContracts.InsertSorted(new Contract("Evil", 2, 13));
            HengforsContracts.InsertSorted(new Contract("Genius", 4, 20));
            HengforsContracts.InsertSorted(new Contract("Sargon", 6, 22));
            HengforsContracts.InsertSorted(new Contract("Balor", 1, 34));
            /*HengforsContracts.InsertSorted(new Contract("werewolf", 1, 10));
            HengforsContracts.InsertSorted(new Contract("vampire", 1, 3));
            HengforsContracts.InsertSorted(new Contract("incubus", 1, 4));
            HengforsContracts.InsertSorted(new Contract("Evil", 1, 13));
            HengforsContracts.InsertSorted(new Contract("Genius", 1, 20));
            HengforsContracts.InsertSorted(new Contract("Sargon", 1, 22));
            HengforsContracts.InsertSorted(new Contract("Balor", 1, 34));*/

            ListOfContracts<Contract> BlavikenContracts = new ListOfContracts<Contract>();
            BlavikenContracts.InsertSorted(new Contract("Shiey", 9, 14));
            BlavikenContracts.InsertSorted(new Contract("Anton", 2, 20));
            BlavikenContracts.InsertSorted(new Contract("Bad Cat", 4, 14));
            BlavikenContracts.InsertSorted(new Contract("Odyssey", 6, 17));
            /*BlavikenContracts.InsertSorted(new Contract("Shiey", 1, 14));
            BlavikenContracts.InsertSorted(new Contract("Anton", 1, 20));
            BlavikenContracts.InsertSorted(new Contract("Bad Cat", 1, 14));
            BlavikenContracts.InsertSorted(new Contract("Odyssey", 1, 17));*/
            //BlavikenContracts.InsertSorted(new Contract("DanFreedom", 1, 30));

            ListOfContracts<Contract> GhelibolContracts = new ListOfContracts<Contract>();
            GhelibolContracts.InsertSorted(new Contract("GhelibolContract1", 9, 8));
            GhelibolContracts.InsertSorted(new Contract("GhelibolContract2", 7, 6));
            GhelibolContracts.InsertSorted(new Contract("GhelibolContract3", 4, 2));
            /*GhelibolContracts.InsertSorted(new Contract("GhelibolContract1", 1, 8));
            GhelibolContracts.InsertSorted(new Contract("GhelibolContract2", 1, 6));
            GhelibolContracts.InsertSorted(new Contract("GhelibolContract3", 1, 30));*/ //NA EZ LÁTVÁNYOS PÉLDA BTW 31 - 32

            ListOfContracts<Contract> RoggevenContracts = new ListOfContracts<Contract>();
            RoggevenContracts.InsertSorted(new Contract("RoggevenContract1", 3, 1));
            RoggevenContracts.InsertSorted(new Contract("RoggevenContract2", 2, 9));
            RoggevenContracts.InsertSorted(new Contract("RoggevenContract3", 1, 8));
            /*RoggevenContracts.InsertSorted(new Contract("RoggevenContract1", 1, 1));
            RoggevenContracts.InsertSorted(new Contract("RoggevenContract2", 1, 9));
            RoggevenContracts.InsertSorted(new Contract("RoggevenContract3", 1, 8));*/

            ListOfContracts<Contract> NovigradContracts = new ListOfContracts<Contract>();
            NovigradContracts.InsertSorted(new Contract("NovigradContract1", 3, 5));
            NovigradContracts.InsertSorted(new Contract("NovigradContract2", 5, 4));
            NovigradContracts.InsertSorted(new Contract("NovigradContract3", 4, 3));
            NovigradContracts.InsertSorted(new Contract("NovigradContract4", 1, 2));
            /*NovigradContracts.InsertSorted(new Contract("NovigradContract1", 1, 5));
            NovigradContracts.InsertSorted(new Contract("NovigradContract2", 1, 4));
            NovigradContracts.InsertSorted(new Contract("NovigradContract3", 1, 3));
            NovigradContracts.InsertSorted(new Contract("NovigradContract4", 1, 2));*/

            ListOfContracts<Contract> TretogorContracts = new ListOfContracts<Contract>();
            TretogorContracts.InsertSorted(new Contract("TretogorContract1", 1, 1));
            TretogorContracts.InsertSorted(new Contract("TretogorContract2", 2, 2));
            TretogorContracts.InsertSorted(new Contract("TretogorContract3", 3, 3));
            TretogorContracts.InsertSorted(new Contract("TretogorContract4", 4, 4));
            TretogorContracts.InsertSorted(new Contract("TretogorContract5", 5, 5));
            /*TretogorContracts.InsertSorted(new Contract("TretogorContract1", 1, 1));
            TretogorContracts.InsertSorted(new Contract("TretogorContract2", 1, 2));
            TretogorContracts.InsertSorted(new Contract("TretogorContract3", 1, 3));
            TretogorContracts.InsertSorted(new Contract("TretogorContract4", 1, 4));
            TretogorContracts.InsertSorted(new Contract("TretogorContract5", 1, 5));*/

            ListOfContracts<Contract> OxenfurtContracts = new ListOfContracts<Contract>();
            OxenfurtContracts.InsertSorted(new Contract("OxenfurtContract1", 1, 12));

            ListOfContracts<Contract> RinbeContracts = new ListOfContracts<Contract>();
            RinbeContracts.InsertSorted(new Contract("RinbeContract1", 1, 13));


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
            Console.Write("Days of adventure:");
            int lengthOfAdventure = Convert.ToInt32(Console.ReadLine());

            MapV2.OnTheSpotEvent += MapV2.DoQuest;

            try
            {
                MapV2.Greedy(MapV2, startCity, lengthOfAdventure);
            }
            catch (InvalidCityException)
            {
                Console.WriteLine("The city you want to start your journey does not exist.");
            }
            //MapV2.Greedy(MapV2, "Rinbe", 8);
            Console.ReadKey();
        }
    }
}