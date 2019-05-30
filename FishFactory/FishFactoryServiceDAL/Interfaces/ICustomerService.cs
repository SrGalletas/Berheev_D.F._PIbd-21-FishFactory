using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FishFactoryServiceDAL.BindingM;
using FishFactoryServiceDAL.ViewM;

namespace FishFactoryServiceDAL.Interfaces
{
    public interface ICustomerService
    {
        List<CustomerViewM> GetList();
        CustomerViewM GetElement(int id);
        void AddElement(CustomerBindingM model);
        void UpdElement(CustomerBindingM model);
        void DelElement(int id);
    }
}

