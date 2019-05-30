using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishFactoryServiceDAL.ViewM
{
    public class CustomerRequestsM
    {
        public string CustomerName { get; set; }
        public string DateCreate { get; set; }
        public string CannedFoodName { get; set; }
        public int Total { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }
    }
}
