using HarbourControl.Models;
using HarbourControl.WeatherService;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace HarbourControl
{
    class Program
    {
        static List<Boat> boats = new List<Boat>();
        static List<Boat> BoatsWaitingAtPerimeter = new List<Boat>();   
        static Queue<Boat> boatsQueue = new Queue<Boat>();
        static Queue<Boat> boatsInPerimeter = new Queue<Boat>();

        private static IWeatherService weatherService = new WeatherService.WeatherService();

        public static bool CheckBoatsInPerimeter()
        {
            int countValue = 0;
            var boatInPerimeter = false;
            if (boatsInPerimeter.Count > 0)
            {
                foreach (var item in boatsInPerimeter)
                {
                    if (item.BoatType == boats[countValue].BoatType)
                    {
                        Console.WriteLine($"There is one {item.BoatType} inside the perimeter");
                        boatInPerimeter = true;
                    }
                    //countValue++;
                }
            }
            else
            {
                Random random = new Random();
                var index = random.Next(BoatsWaitingAtPerimeter.Count);
                boatsInPerimeter.Enqueue(BoatsWaitingAtPerimeter[index]);
            }

            return boatInPerimeter;
        }

        private static void DockBoat(Boat boat)
        {

            switch (boat.BoatType)
            {
                case "Speedboat":
                    var Speedboat = CalculateTime(30);

                    Thread.Sleep(10000);

                    boat.Docked = true;
                    break;

                case "CargoShip":
                    var CargoShip = CalculateTime(5);

                    Thread.Sleep(10000);

                    boat.Docked = true;
                    break;
                case "Sailboat":
                    var Sailboat = CalculateTime(15);

                    Thread.Sleep(10000);

                    boat.Docked = true;
                    break;
                default:
                    break;
            }
        }

        private static double CalculateTime(int speed)
        {
            var time = 0.0;
            return time = 10 / speed;
        }
        public static Queue<Boat> GetBoats()
        {
            Random random = new Random();

            for (int i = 0; i < boats.Count; i++)
            {
                boatsQueue.Enqueue(boats[random.Next(boats.Count)]);
            }

            foreach (var item in boatsQueue)
            {
                BoatsWaitingAtPerimeter.Add(item);
            }

            return boatsQueue;
        }
        public static List<Boat> GetBoatTypes()
        {
            boats.Add(new CargoShip("CargoShip",5));
            boats.Add(new Sailboat("Sailboat", 15));
            boats.Add(new Speedboat("Speedboat", 30));
            boats.Add(new Speedboat("Speedboat", 30));

            return boats;
        }
        static void Main(string[] args)
        {
            //first get the boats to the perimeter
            GetBoatTypes();
            GetBoats();

            //check wind speed
            var wind = weatherService.GetHarbourWindSpeed().Result;
            if (wind.speed< 10 || wind.speed>30)
            {
                // remove sailboats from queue
                boatsQueue.Dequeue().BoatType = "Sailboat";

                //GetBoats();

                var boatin = CheckBoatsInPerimeter();

                if (boatin)
                {
                    foreach (var boat in boatsInPerimeter)
                    {
                        DockBoat(boat);
                    }

                    // after boat is docked, let a new boat enter the perimeter
                    boatsInPerimeter.Clear();

                    CheckBoatsInPerimeter();
                }
                else
                {
                    // since no boat is in the perimeter, allow a boat to enter
                    CheckBoatsInPerimeter();
                }
            }
            else
            {
                // since wind speed allows boats to enter perimeter, allow boats to enter
               var boatin = CheckBoatsInPerimeter();

                if (boatin)
                {
                    foreach (var boat in boatsInPerimeter)
                    {
                        DockBoat(boat);
                    }

                    // after boat is docked, let a new boat enter the perimeter
                    boatsInPerimeter.Clear();

                    CheckBoatsInPerimeter();
                }
                else
                {
                    // since no boat is in the perimeter, allow a boat to enter
                    CheckBoatsInPerimeter();
                }

            }
            Console.ReadKey();
        }


    }
}
