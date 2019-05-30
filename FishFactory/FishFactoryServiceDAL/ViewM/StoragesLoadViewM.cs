using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishFactoryServiceDAL.ViewM
{
    public class StoragesLoadViewM
    {
        public string StorageNominal { get; set; }
        public int TotalTotal { get; set; }
        public IEnumerable<Tuple<string, int>> TypesOfFish { get; set; }
    }
}
