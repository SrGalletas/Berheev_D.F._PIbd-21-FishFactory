using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishFactoryServiceDAL.BindingM
{
    public class RequestBindingM
    {
        public int Id { get; set; }
        public int CustomerId { get; set; } //ClientId
        public int CannedFoodId { get; set; } //ProductId
        public int Total { get; set; } //Count
        public decimal Amount { get; set; } //Sum
    }
}
