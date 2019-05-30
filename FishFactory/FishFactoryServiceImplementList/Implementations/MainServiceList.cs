using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FishFactoryServiceDAL.BindingM;
using FishFactoryServiceDAL.ViewM;
using FishFactoryServiceDAL.Interfaces;
using FishFactoryModel;

namespace FishFactoryServiceImplementList.Implementations
{
    public class MainServiceList : IMainService
    {
        private DataListSingleton source;
        public MainServiceList()
        {
            source = DataListSingleton.GetInstance();
        }
        public List<RequestViewM> GetList()
        {
            List<RequestViewM> result = new List<RequestViewM>();
            for (int i = 0; i < source.Requests.Count; ++i)
            {
                string customerFIO = string.Empty;
                for (int j = 0; j < source.Requests.Count; ++j)
                {
                    if (source.Customers[j].Id == source.Requests[i].CustomerId)
                    {
                        customerFIO = source.Customers[j].CustomerFIO;
                        break;
                    }
                }

                string cannedFoodName = string.Empty;
                for (int j = 0; j < source.CannedFoods.Count; ++j)
                {
                    if (source.CannedFoods[j].Id == source.Requests[i].CannedFoodId)
                    {
                        cannedFoodName = source.CannedFoods[j].CannedFoodName;
                        break;
                    }
                }
                result.Add(new RequestViewM
                {
                    Id = source.Requests[i].Id,
                    CustomerId = source.Requests[i].CustomerId,
                    CustomerFIO = customerFIO,
                    CannedFoodId = source.Requests[i].CannedFoodId,
                    CannedFoodName = cannedFoodName,
                    Total = source.Requests[i].Total,
                    Amount = source.Requests[i].Amount,
                    DateCreate = source.Requests[i].DateCreate.ToLongDateString(),
                    DateImplement = source.Requests[i].DateImplement?.ToLongDateString(),
                    Status = source.Requests[i].Status.ToString()
                });
            }
            return result;
        }
        public void CreateRequest(RequestBindingM model)
        {
            int maxId = 0;
            for (int i = 0; i < source.Requests.Count; ++i)
            {
                if (source.Requests[i].Id > maxId)
                {
                    maxId = source.Requests[i].Id;
                }
            }
            source.Requests.Add(new Request
            {
                Id = maxId + 1,
                CustomerId = model.CustomerId,
                CannedFoodId = model.CannedFoodId,
                DateCreate = DateTime.Now,
                Total = model.Total,
                Amount = model.Amount,
                Status = RequestStatus.Принят
            });
        }
        public void TakeRequestInWork(RequestBindingM model)
        {
            int index = -1;
            for (int i = 0; i < source.Requests.Count; ++i)
            {
                if (source.Requests[i].Id == model.Id)
                {
                    index = i;
                    break;
                }
            }
            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }
            if (source.Requests[index].Status != RequestStatus.Принят)
            {
                throw new Exception("Заказ не в статусе \"Принят\"");
            }
            source.Requests[index].DateImplement = DateTime.Now;
            source.Requests[index].Status = RequestStatus.Выполняется;
        }
        public void FinishRequest(RequestBindingM model)
        {
            int index = -1;
            for (int i = 0; i < source.Requests.Count; ++i)
            {
                if (source.Requests[i].Id == model.Id)
                {
                    index = i;
                    break;
                }
            }
            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }
            if (source.Requests[index].Status != RequestStatus.Выполняется)
            {
                throw new Exception("Заказ не в статусе \"Выполняется\"");
            }
            source.Requests[index].Status = RequestStatus.Готов;
        }
        public void PayRequest(RequestBindingM model)
        {
            int index = -1;
            for (int i = 0; i < source.Requests.Count; ++i)
            {
                if (source.Requests[i].Id == model.Id)
                {
                    index = i;
                    break;
                }
            }
            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }
            if (source.Requests[index].Status != RequestStatus.Готов)
            {
                throw new Exception("Заказ не в статусе \"Готов\"");
            }
            source.Requests[index].Status = RequestStatus.Оплачен;
        }
    }
}