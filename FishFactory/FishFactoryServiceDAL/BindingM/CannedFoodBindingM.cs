using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishFactoryServiceDAL.BindingM
{
    public class CannedFoodBindingM
    {
        public int Id { get; set; }
        public string CannedFoodName { get; set; }
        public decimal Cost { get; set; } //Price
        public List<TypeOfCannedBindingM> TypeOfCanneds { get; set; }
    }
}
