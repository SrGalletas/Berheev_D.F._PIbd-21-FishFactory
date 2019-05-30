using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishFactoryServiceDAL.ViewM
{
    public class RequestViewM
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        [DisplayName("ФИО Клиента")]
        public string CustomerFIO { get; set; }

        public int CannedFoodId { get; set; }
        [DisplayName("Продукт")]
        public string CannedFoodName { get; set; }
        [DisplayName("Количество")]
        public int Total { get; set; }
        [DisplayName("Сумма")]
        public decimal Amount { get; set; }
        [DisplayName("Статус")]
        public string Status { get; set; }
        [DisplayName("Дата создания")]
        public string DateCreate { get; set; }
        [DisplayName("Дата выполнения")]
        public string DateImplement { get; set; }
    }
}
