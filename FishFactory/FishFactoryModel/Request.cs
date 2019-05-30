using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishFactoryModel
{
    /// <summary>
    /// Заказ клиента
    /// </summary>
    public class Request
    {
        public int Id { get; set; }
        public int CustomerId { get; set; } //ClientId
        public int CannedFoodId { get; set; } //ProductId
        public int Total { get; set; } //Count
        public decimal Amount { get; set; } //Sum
        public RequestStatus Status { get; set; } //OrderStatus
        public DateTime DateCreate { get; set; }
        public DateTime? DateImplement { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual CannedFood CannedFood { get; set; }
    }
}
