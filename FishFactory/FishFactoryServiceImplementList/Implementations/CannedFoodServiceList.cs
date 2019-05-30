using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FishFactoryModel;
using FishFactoryServiceDAL.BindingM;
using FishFactoryServiceDAL.ViewM;
using FishFactoryServiceDAL.Interfaces;

namespace FishFactoryServiceImplementList.Implementations
{
    public class CannedFoodServiceList : ICannedFoodService
    {
        private DataListSingleton source;
        public CannedFoodServiceList()
        {
            source = DataListSingleton.GetInstance();
        }
        public List<CannedFoodViewM> GetList()
        {
            List<CannedFoodViewM> result = source.CannedFoods
                .Select(rec => new CannedFoodViewM
                {
                    Id = rec.Id,
                    CannedFoodName = rec.CannedFoodName,
                    Cost = rec.Cost,
                    TypeOfCanned = source.TypeOfCanneds
                            .Where(recPC => recPC.CannedFoodId == rec.Id)
                            .Select(recPC => new TypeOfCannedViewM
                            {
                                Id = recPC.Id,
                                CannedFoodId = recPC.CannedFoodId,
                                TypeOfFishId = recPC.TypeOfFishId,
                                TypeOfFishName = source.TypesOfFish.FirstOrDefault(recC =>
                                recC.Id == recPC.TypeOfFishId)?.TypeOfFishName,
                                Total = recPC.Total
                            })
                        .ToList()
                })
                .ToList();
            return result;
        }
        public CannedFoodViewM GetElement(int id)
        {
            CannedFood element = source.CannedFoods.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new CannedFoodViewM
                {
                    Id = element.Id,
                    CannedFoodName = element.CannedFoodName,
                    Cost = element.Cost,
                    TypeOfCanned = source.TypeOfCanneds
                .Where(recPC => recPC.CannedFoodId == element.Id)
                .Select(recPC => new TypeOfCannedViewM
                {
                    Id = recPC.Id,
                    CannedFoodId = recPC.CannedFoodId,
                    TypeOfFishId = recPC.TypeOfFishId,
                    TypeOfFishName = source.TypesOfFish.FirstOrDefault(recC =>
    recC.Id == recPC.TypeOfFishId)?.TypeOfFishName,
                    Total = recPC.Total
                })
                .ToList()
                };
            }
            throw new Exception("Элемент не найден");
        }
        public void AddElement(CannedFoodBindingM model)
        {
            CannedFood element = source.CannedFoods.FirstOrDefault(rec => rec.CannedFoodName ==
            model.CannedFoodName);
            if (element != null)
            {
                throw new Exception("Уже есть изделие с таким названием");
            }
            int maxId = source.CannedFoods.Count > 0 ? source.CannedFoods.Max(rec => rec.Id) :
            0;
            source.CannedFoods.Add(new CannedFood
            {
                Id = maxId + 1,
                CannedFoodName = model.CannedFoodName,
                Cost = model.Cost
            });
            // компоненты для изделия
            int maxPCId = source.TypeOfCanneds.Count > 0 ?
            source.TypeOfCanneds.Max(rec => rec.Id) : 0;
            // убираем дубли по компонентам
            var groupTypesOfFish = model.TypeOfCanneds
            .GroupBy(rec => rec.TypeOfFishId)
            .Select(rec => new
            {
                TypeOfFishId = rec.Key,
                Total = rec.Sum(r => r.Total)
            });
            // добавляем компоненты
            foreach (var groupTypeOfFish in groupTypesOfFish)
            {
                source.TypeOfCanneds.Add(new TypeOfCanned
                {
                    Id = ++maxPCId,
                    CannedFoodId = maxId + 1,

                    TypeOfFishId = groupTypeOfFish.TypeOfFishId,
                    Total = groupTypeOfFish.Total
                });
            }
        }
        public void UpdElement(CannedFoodBindingM model)
        {
            CannedFood element = source.CannedFoods.FirstOrDefault(rec => rec.CannedFoodName ==
            model.CannedFoodName && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть изделие с таким названием");
            }
            element = source.CannedFoods.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.CannedFoodName = model.CannedFoodName;
            element.Cost = model.Cost;
            int maxPCId = source.TypeOfCanneds.Count > 0 ?
            source.TypeOfCanneds.Max(rec => rec.Id) : 0;
            // обновляем существуюущие компоненты
            var compIds = model.TypeOfCanneds.Select(rec =>
            rec.TypeOfFishId).Distinct();
            var updateTypesOfFish = source.TypeOfCanneds.Where(rec => rec.CannedFoodId ==
            model.Id && compIds.Contains(rec.TypeOfFishId));
            foreach (var updateTypeOfFish in updateTypesOfFish)
            {
                updateTypeOfFish.Total = model.TypeOfCanneds.FirstOrDefault(rec =>
                rec.Id == updateTypeOfFish.Id).Total;
            }
            source.TypeOfCanneds.RemoveAll(rec => rec.CannedFoodId == model.Id &&
            !compIds.Contains(rec.TypeOfFishId));
            // новые записи
            var groupTypesOfFish = model.TypeOfCanneds
            .Where(rec => rec.Id == 0)
            .GroupBy(rec => rec.TypeOfFishId)
            .Select(rec => new
            {
                TypeOfFishId = rec.Key,
                Count = rec.Sum(r => r.Total)
            });
            foreach (var groupTypeOfFish in groupTypesOfFish)
            {
                TypeOfCanned elementPC = source.TypeOfCanneds.FirstOrDefault(rec
                => rec.CannedFoodId == model.Id && rec.TypeOfFishId == groupTypeOfFish.TypeOfFishId);
                if (elementPC != null)
                {
                    elementPC.Total += groupTypeOfFish.Count;
                }
                else
                {
                    source.TypeOfCanneds.Add(new TypeOfCanned
                    {
                        Id = ++maxPCId,
                        CannedFoodId = model.Id,
                        TypeOfFishId = groupTypeOfFish.TypeOfFishId,
                        Total = groupTypeOfFish.Count
                    });
                }
            }
        }
        public void DelElement(int id)
        {
            CannedFood element = source.CannedFoods.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                // удаяем записи по компонентам при удалении изделия
                source.TypeOfCanneds.RemoveAll(rec => rec.CannedFoodId == id);
                source.CannedFoods.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
    }
}
