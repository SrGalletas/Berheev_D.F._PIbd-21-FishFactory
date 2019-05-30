using FishFactoryModel;
using FishFactoryServiceDAL.BindingM;
using FishFactoryServiceDAL.Interfaces;
using FishFactoryServiceDAL.ViewM;
using FishFactoryServiceImplementDataBase;
using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;


namespace AbstractGarmentFactoryServiceImplementDataBase.Implementations
{
    public class MainServiceDb : IMainService
    {
        private AbstractDbEnvironment context;

        public MainServiceDb(AbstractDbEnvironment context)
        {
            this.context = context;
        }

        public List<RequestViewM> GetList()
        {
            List<RequestViewM> result = context.Requests.Select(rec => new RequestViewM
            {
                Id = rec.Id,
                CustomerId = rec.CustomerId,
                CannedFoodId = rec.CannedFoodId,
                DateCreate = SqlFunctions.DateName("dd", rec.DateCreate) + " " + SqlFunctions.DateName("mm", rec.DateCreate) + " " + SqlFunctions.DateName("yyyy", rec.DateCreate),
                DateImplement = rec.DateImplement == null ? "" : SqlFunctions.DateName("dd", rec.DateImplement.Value) + " " + SqlFunctions.DateName("mm", rec.DateImplement.Value) + " " + SqlFunctions.DateName("yyyy", rec.DateImplement.Value),
                Status = rec.Status.ToString(),
                Amount = rec.Amount,
                Total = rec.Total,
                CustomerFIO = rec.Customer.CustomerFIO,
                CannedFoodName = rec.CannedFood.CannedFoodName
            }).ToList();
            return result;
        }

        public void CreateRequest(RequestBindingM model)
        {
            context.Requests.Add(new Request
            {
                CustomerId = model.CustomerId,
                CannedFoodId = model.CannedFoodId,
                DateCreate = DateTime.Now,
                Amount = model.Amount,
                Total = model.Total,
                Status = RequestStatus.Принят
            });
            context.SaveChanges();
        }

        public void TakeRequestInWork(RequestBindingM model)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {

                    Request element = context.Requests.FirstOrDefault(rec => rec.Id == model.Id);
                    if (element == null)
                    {
                        throw new Exception("Элемент не найден");
                    }
                    if (element.Status != RequestStatus.Принят)
                    {
                        throw new Exception("Заказ не в статусе \"Принят\"");
                    }
                    var typeOfCanneds = context.TypeOfCanneds.Include(rec => rec.TypesOfFish).Where(rec => rec.CannedFoodId == element.CannedFoodId);
                    // списываем      
                    foreach (var typeOfCanned in typeOfCanneds)
                    {
                        int totalOnStorage = typeOfCanned.Total * element.Total;
                        var storageFishes = context.StorageFishes.Where(rec => rec.TypeOfFishId == typeOfCanned.TypeOfFishId);
                        foreach (var storageFish in storageFishes)
                        {
                            // компонентов на одном слкаде может не хватать  
                            if (storageFish.Total >= totalOnStorage)
                            {
                                storageFish.Total -= totalOnStorage;
                                totalOnStorage = 0;
                                context.SaveChanges();
                                break;
                            }
                            else
                            {
                                totalOnStorage -= typeOfCanned.Total;
                                storageFish.Total = 0;
                                context.SaveChanges();
                            }
                        }
                        if (totalOnStorage > 0)
                        {
                            throw new Exception("Не достаточно компонента " + typeOfCanned.TypesOfFish.TypeOfFishName + " требуется " + typeOfCanned.Total + ", не хватает " + totalOnStorage);
                        }
                    }
                    element.DateImplement = DateTime.Now;
                    element.Status = RequestStatus.Выполняется;
                    context.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public void FinishRequest(RequestBindingM model)
        {
            Request element = context.Requests.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            if (element.Status != RequestStatus.Выполняется)
            {
                throw new Exception("Заказ не в статусе \"Выполняется\"");
            }
            element.Status = RequestStatus.Готов;
            context.SaveChanges();
        }

        public void PayRequest(RequestBindingM model)
        {
            Request element = context.Requests.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            if (element.Status != RequestStatus.Готов)
            {
                throw new Exception("Заказ не в статусе \"Готов\"");
            }
            element.Status = RequestStatus.Оплачен;
            context.SaveChanges();
        }

        public void PutTypeOfFishOnStorage(StorageFishBindingM model)
        {
            StorageFish element = context.StorageFishes.FirstOrDefault(rec => rec.StorageId == model.StorageId && rec.TypeOfFishId == model.TypeOfFishId);
            if (element != null)
            {
                element.Total += model.Total;
            }
            else
            {
                context.StorageFishes.Add(new StorageFish
                {
                    StorageId = model.StorageId,
                    TypeOfFishId = model.TypeOfFishId,
                    Total = model.Total
                });
            }
            context.SaveChanges();
        }
    }
}
