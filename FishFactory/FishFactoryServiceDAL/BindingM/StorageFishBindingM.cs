using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishFactoryServiceDAL.BindingM
{
    public class StorageFishBindingM
    {
        public int Id { get; set; }
        public int StorageId { get; set; }
        public int TypeOfFishId { get; set; }
        public int Total { get; set; }
    }
}
