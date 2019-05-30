using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishFactoryServiceDAL.ViewM
{
    public class StorageFishViewM
    {
        public int Id { get; set; }
        public int StorageId { get; set; }
        public int TypeOfFishId { get; set; }
        [DisplayName("Название компонента")]
        public string TypeOfFishName { get; set; }
        [DisplayName("Количество")]
        public int Total { get; set; }
    }
}
