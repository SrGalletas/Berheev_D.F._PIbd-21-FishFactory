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
    /// Изделие, изготавливаемое в магазине
    /// </summary>
    public class CannedFood
    {
        public int Id { get; set; }
        [Required]
        public string CannedFoodName { get; set; } //ProductName
        [Required]
        public decimal Cost { get; set; } //Price

        [ForeignKey("CannedFoodId")]
        public virtual List<Request> Requests { get; set; }
        [ForeignKey("CannedFoodId")]
        public virtual List<TypeOfCanned> TypeOfCanneds { get; set; }

    }
}
