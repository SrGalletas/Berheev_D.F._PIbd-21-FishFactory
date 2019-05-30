using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishFactoryModel
{
    /// <summary>
    /// Сколько компонентов, требуется при изготовлении изделия
    /// </summary>
    public class TypeOfCanned
    {
        public int Id { get; set; }
        public int CannedFoodId { get; set; } //ProductId
        public int TypeOfFishId { get; set; } //ComponentId
        public int Total { get; set; } //Count
        public virtual CannedFood CannedFoods { get; set; }
        public virtual TypeOfFish TypesOfFish { get; set; }
    }
}
