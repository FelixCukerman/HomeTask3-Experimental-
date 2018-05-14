using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeTask3_Experimental_.Parking
{
    class Car
    {
        readonly int id;
        public int Id
        {
            get { return id; }
        }

        double cash;
        public double Cash 
        {
            get { return cash;  }
            set { cash = value; }
        }

        readonly CarType category;
        public CarType Category 
        {
            get { return category; }
        }

        public Car(int id, double cash, CarType category)
        {
            this.id = id;
            this.cash = cash;

            this.category = category;
        }

        public void AddCash(double cash)
        {
            this.cash += cash;
        }

        public static void ShowTypeCar()
        {
            Array car = Enum.GetValues(typeof(CarType));
            Array colorList = Enum.GetValues(typeof(ConsoleColor));
            for(int i = 0; i < car.Length; i++)
            {
                Console.ForegroundColor = (ConsoleColor)colorList.GetValue(i+3);
                Console.Write("[{0}] ", car.GetValue(i));
            }
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }

    enum CarType
    {
        Passenger = 1,
        Truck,
        Bus,
        Motorcycle
    }
}