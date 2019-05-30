using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FishFactoryModel;

namespace FishFactoryServiceImplementList
{
    class DataListSingleton
    {
        private static DataListSingleton instance;
        public List<Customer> Customers { get; set; }
        public List<TypeOfFish> TypesOfFish { get; set; }
        public List<Request> Requests { get; set; }
        public List<CannedFood> CannedFoods { get; set; }
        public List<TypeOfCanned> TypeOfCanneds { get; set; }
        private DataListSingleton()
        {
            Customers = new List<Customer>();
            TypesOfFish = new List<TypeOfFish>();
            Requests = new List<Request>();
            CannedFoods = new List<CannedFood>();
            TypeOfCanneds = new List<TypeOfCanned>();
        }
        public static DataListSingleton GetInstance()
        {
            if (instance == null)
            {
                instance = new DataListSingleton();
            }
            return instance;
        }
    }
}
