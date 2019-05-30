using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishFactoryServiceDAL.BindingM
{
    public class TypeOfCannedBindingM
    {
        public int Id { get; set; }
        public int CannedFoodId { get; set; } //ProductId
        public int TypeOfFishId { get; set; } //ComponentId
        public int Total { get; set; } //Count
    }
}
