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
            List<RequestViewM> result = source.Requests
            .Select(rec => new RequestViewM
            {
                Id = rec.Id,
                CustomerId = rec.CustomerId,
                CannedFoodId = rec.CannedFoodId,
                DateCreate = rec.DateCreate.ToLongDateString(),
                DateImplement = rec.DateImplement?.ToLongDateString(),
                Status = rec.Status.ToString(),
                Total = rec.Total,
                Amount = rec.Amount,
                CustomerFIO = source.Customers.FirstOrDefault(recC => recC.Id ==
                rec.CustomerId)?.CustomerFIO,
                CannedFoodName = source.CannedFoods.FirstOrDefault(recP => recP.Id ==
                rec.CannedFoodId)?.CannedFoodName,
            })
            .ToList();
            return result;
        }
        public void CreateRequest(RequestBindingM model)
        {
            int maxId = source.Requests.Count > 0 ? source.Requests.Max(rec => rec.Id) : 0;
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
            Request element = source.Requests.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            if (element.Status != RequestStatus.Принят)
            {
                throw new Exception("Заказ не в статусе \"Принят\"");
            }
            // смотрим по количеству компонентов на складах
            var typeOfCanneds = source.TypeOfCanneds.Where(rec => rec.CannedFoodId
            == element.CannedFoodId);
            foreach (var typeOfCanned in typeOfCanneds)
            {
                int countOnStorages = source.StorageFishes
                .Where(rec => rec.TypeOfFishId ==
                typeOfCanned.TypeOfFishId)
                .Sum(rec => rec.Total);
                if (countOnStorages < typeOfCanned.Total * element.Total)
                {
                    var typeOfFishName = source.TypesOfFish.FirstOrDefault(rec => rec.Id ==
                    typeOfCanned.TypeOfFishId);
                    throw new Exception("Не достаточно компонента " +
                    typeOfFishName?.TypeOfFishName + " требуется " + (typeOfCanned.Total * element.Total) +
                    ", в наличии " + countOnStorages);
                }
            }
            // списываем
            foreach (var typeOfCanned in typeOfCanneds)
            {
                int countOnStorages = typeOfCanned.Total * element.Total;
                var storageFishes = source.StorageFishes.Where(rec => rec.TypeOfFishId
                == typeOfCanned.TypeOfFishId);
                foreach (var storageTypeOfFish in storageFishes)
                {
                    // компонентов на одном слкаде может не хватать
                    if (storageTypeOfFish.Total >= countOnStorages)
                    {
                        storageTypeOfFish.Total -= countOnStorages;
                        break;
                    }
                    else
                    {
                        countOnStorages -= storageTypeOfFish.Total;
                        storageTypeOfFish.Total = 0;
                    }
                }
            }
            element.DateImplement = DateTime.Now;
            element.Status = RequestStatus.Выполняется;
        }
        public void FinishRequest(RequestBindingM model)
        {
            Request element = source.Requests.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            if (element.Status != RequestStatus.Выполняется)
            {
                throw new Exception("Заказ не в статусе \"Выполняется\"");
            }
            element.Status = RequestStatus.Готов;
        }
        public void PayRequest(RequestBindingM model)
        {
            Request element = source.Requests.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            if (element.Status != RequestStatus.Готов)
            {
                throw new Exception("Заказ не в статусе \"Готов\"");
            }
            element.Status = RequestStatus.Оплачен;
        }
        public void PutTypeOfFishOnStorage(StorageFishBindingM model)
        {
            StorageFish element = source.StorageFishes.FirstOrDefault(rec =>
            rec.StorageId == model.StorageId && rec.TypeOfFishId == model.TypeOfFishId);
            if (element != null)
            {
                element.Total += model.Total;
            }
            else
            {
                int maxId = source.StorageFishes.Count > 0 ?
                source.StorageFishes.Max(rec => rec.Id) : 0;
                source.StorageFishes.Add(new StorageFish
                {
                    Id = ++maxId,
                    StorageId = model.StorageId,
                    TypeOfFishId = model.TypeOfFishId,
                    Total = model.Total
                });
            }
        }
    }
}