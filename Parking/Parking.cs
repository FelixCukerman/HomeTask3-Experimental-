using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace HomeTask3_Experimental_.Parking
{
    class Parking
    {
        private static readonly Lazy<Parking> lazy = new Lazy<Parking>(() => new Parking());
        private List<Car> area;
        public List<Car> AllCar
        {
            get 
            {
                if(area == null)
                {
                    area = new List<Car>();
                } 
                return area; 
            }
        }
        protected Parking()
        {
            area = new List<Car>();
            area.Add(new Car(1, 100, "Passenger"));
        }

        public static Parking Create
        {
            get
            {
                return lazy.Value;
            }
        }
    }
}