using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishFactoryModel
{
    /// <summary>
    /// Компонент, требуемый для изготовления изделия
    /// </summary>
    public class TypeOfFish
    {
        public int Id { get; set; }
        public string TypeOfFishName { get; set; } //ComponentName
    }
}
