using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishFactoryModel
{
    /// <summary>
    /// Сколько компонентов хранится на складе
    /// </summary>
    public class StorageFish
    {
        public int Id { get; set; }
        public int StorageId { get; set; }
        public int TypeOfFishId { get; set; }
        public int Total { get; set; }
        public virtual Storage Storages { get; set; }
        public virtual TypeOfFish TypesOfFish { get; set; }
    }
}
