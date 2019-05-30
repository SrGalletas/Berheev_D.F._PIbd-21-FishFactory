using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishFactoryServiceDAL.ViewM
{
    public class CannedFoodViewM
    {
        public int Id { get; set; }
        [DisplayName("Название продукта")]
        public string CannedFoodName { get; set; }
        [DisplayName("Цена")]
        public decimal Cost { get; set; } //Price
        public List<TypeOfCannedViewM> TypeOfCanneds { get; set; }
    }
}
