using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FishFactoryServiceDAL.BindingM;
using FishFactoryServiceDAL.ViewM;

namespace FishFactoryServiceDAL.Interfaces
{
    public interface IMainService
    {
        List<RequestViewM> GetList();
        void CreateRequest(RequestBindingM model); //CreateOrder
        void TakeRequestInWork(RequestBindingM model);
        void FinishRequest(RequestBindingM model);
        void PayRequest(RequestBindingM model);
    }
}
