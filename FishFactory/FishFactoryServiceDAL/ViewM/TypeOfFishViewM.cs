using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishFactoryServiceDAL.ViewM
{
    public class TypeOfFishViewM
    {
        public int Id { get; set; }
        [DisplayName("Название компонента")]
        public string TypeOfFishName { get; set; } //ComponentName
    }
}
