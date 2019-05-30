using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishFactoryModel
{
    /// <summary>
    /// Изделие, изготавливаемое в магазине
    /// </summary>
    public class CannedFood
    {
        public int Id { get; set; }
        public string CannedFoodName { get; set; } //ProductName
        public decimal Cost { get; set; } //Price
    }
}
