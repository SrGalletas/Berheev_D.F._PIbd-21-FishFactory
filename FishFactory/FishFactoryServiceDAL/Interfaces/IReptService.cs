using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FishFactoryServiceDAL.BindingM;
using FishFactoryServiceDAL.ViewM;

namespace FishFactoryServiceDAL.Interfaces
{
    public interface IReptService
    {
        void SaveCannedFoodCost(ReptBindingM model);
        List<StoragesLoadViewM> GetStoragesLoad();
        void SaveStoragesLoad(ReptBindingM model);
        List<CustomerRequestsM> GetCustomerRequests(ReptBindingM model);
        void SaveCustomerRequests(ReptBindingM model);
    }
}
