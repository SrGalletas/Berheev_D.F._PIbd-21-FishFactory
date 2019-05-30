using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FishFactoryModel
{
    /// <summary>
    /// Компонент, требуемый для изготовления изделия
    /// </summary>
    public class TypeOfFish
    {
        public int Id { get; set; }
        [Required]
        public string TypeOfFishName { get; set; } //ComponentName
        [ForeignKey("TypeOfFishId")]
        public virtual List<StorageFish> StorageFishes { get; set; }

        [ForeignKey("TypeOfFishId")]
        public virtual List<TypeOfCanned> TypeOfCanneds { get; set; }
    }
}
