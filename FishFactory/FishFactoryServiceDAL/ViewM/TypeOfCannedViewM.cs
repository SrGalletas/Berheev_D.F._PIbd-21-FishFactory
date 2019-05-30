using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishFactoryServiceDAL.ViewM
{
    public class TypeOfCannedViewM
    {
        public int Id { get; set; }
        public int CannedFoodId { get; set; } //ProductId
        public int TypeOfFishId { get; set; } //ComponentId
        [DisplayName("Количество")]
        public int Total { get; set; } //Count
        [DisplayName("Компонент")]
        public string TypeOfFishName { get; set; }
    }
}
