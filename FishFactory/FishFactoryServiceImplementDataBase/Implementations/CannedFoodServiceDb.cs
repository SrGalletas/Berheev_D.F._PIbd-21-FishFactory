using FishFactoryModel;
using FishFactoryServiceDAL.BindingM;
using FishFactoryServiceDAL.Interfaces;
using FishFactoryServiceDAL.ViewM;
using FishFactoryServiceImplementDataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractGarmentFactoryServiceImplementDataBase.Implementations
{
    public class CannedFoodServiceDb : ICannedFoodService
    {
        private AbstractDbEnvironment context;

        public CannedFoodServiceDb(AbstractDbEnvironment context)
        {
            this.context = context;
        }

        public List<CannedFoodViewM> GetList()
        {
            List<CannedFoodViewM> result = context.CannedFoods.Select(rec => new CannedFoodViewM
            {
                Id = rec.Id,
                CannedFoodName = rec.CannedFoodName,
                Cost = rec.Cost,
                TypeOfCanned = context.TypeOfCanneds
                .Where(recPC => recPC.CannedFoodId == rec.Id)
                .Select(recPC => new TypeOfCannedViewM
                {
                    Id = recPC.Id,
                    CannedFoodId = recPC.CannedFoodId,
                    TypeOfFishId = recPC.TypeOfFishId,
                    TypeOfFishName = recPC.TypesOfFish.TypeOfFishName,
                    Total = recPC.Total
                }).ToList()
            }).ToList();
            return result;
        }

        public CannedFoodViewM GetElement(int id)
        {
            CannedFood element = context.CannedFoods.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new CannedFoodViewM
                {
                    Id = element.Id,
                    CannedFoodName = element.CannedFoodName,
                    Cost = element.Cost,
                    TypeOfCanned = context.TypeOfCanneds.Where(recPC => recPC.CannedFoodId == element.Id).Select(recPC => new TypeOfCannedViewM
                    {
                        Id = recPC.Id,
                        CannedFoodId = recPC.CannedFoodId,
                        TypeOfFishId = recPC.TypeOfFishId,
                        TypeOfFishName = recPC.TypesOfFish.TypeOfFishName,
                        Total = recPC.Total
                    }).ToList()
                };
            }
            throw new Exception("Элемент не найден");
        }

        public void AddElement(CannedFoodBindingM model)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    CannedFood element = context.CannedFoods.FirstOrDefault(rec => rec.CannedFoodName == model.CannedFoodName);
                    if (element != null)
                    {
                        throw new Exception("Уже есть изделие с таким названием");
                    }
                    element = new CannedFood
                    {
                        CannedFoodName = model.CannedFoodName,
                        Cost = model.Cost
                    };
                    context.CannedFoods.Add(element);
                    context.SaveChanges();
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
                        context.TypeOfCanneds.Add(new TypeOfCanned
                        {
                            CannedFoodId = element.Id,
                            TypeOfFishId = groupTypeOfFish.TypeOfFishId,
                            Total = groupTypeOfFish.Total
                        });
                        context.SaveChanges();
                    }
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw; 
                }
            }
        }

        public void UpdElement(CannedFoodBindingM model)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    CannedFood element = context.CannedFoods.FirstOrDefault(rec => rec.CannedFoodName == model.CannedFoodName && rec.Id != model.Id);
                    if (element != null)
                    { throw new Exception("Уже есть изделие с таким названием");
                    }
                    element = context.CannedFoods.FirstOrDefault(rec => rec.Id == model.Id);
                    if (element == null)
                    {
                        throw new Exception("Элемент не найден");
                    }
                    element.CannedFoodName = model.CannedFoodName;
                    element.Cost = model.Cost;
                    context.SaveChanges();

                    // обновляем существуюущие компоненты     
                    var compIds = model.TypeOfCanneds.Select(rec => rec.TypeOfFishId).Distinct();
                    var updateTypesOfFish = context.TypeOfCanneds.Where(rec => rec.CannedFoodId == model.Id && compIds.Contains(rec.TypeOfFishId));
                    foreach (var updateTypeOfFish in updateTypesOfFish)
                    {
                        updateTypeOfFish.Total = model.TypeOfCanneds.FirstOrDefault(rec => rec.Id == updateTypeOfFish.Id).Total;
                    }
                    context.SaveChanges();
                    context.TypeOfCanneds.RemoveRange(context.TypeOfCanneds.Where(rec => rec.CannedFoodId == model.Id && !compIds.Contains(rec.TypeOfFishId)));
                    context.SaveChanges();
                    // новые записи                
                    var groupTypesOfFish = model.TypeOfCanneds  
                        .Where(rec => rec.Id == 0)             
                        .GroupBy(rec => rec.TypeOfFishId)         
                        .Select(rec => new
                        {
                            TypeOfFishId = rec.Key,
                            Total = rec.Sum(r => r.Total)
                        });
                    foreach (var groupTypeOfFish in groupTypesOfFish)
                    {
                        TypeOfCanned elementPC = context.TypeOfCanneds.FirstOrDefault(rec => rec.CannedFoodId == model.Id && rec.TypeOfFishId == groupTypeOfFish.TypeOfFishId);
                        if (elementPC != null)
                        {
                            elementPC.Total += groupTypeOfFish.Total;
                            context.SaveChanges();
                        }
                        else
                        {
                            context.TypeOfCanneds.Add(new TypeOfCanned
                            {
                                CannedFoodId = model.Id,
                                TypeOfFishId = groupTypeOfFish.TypeOfFishId,
                                Total = groupTypeOfFish.Total
                            });
                            context.SaveChanges();
                        }
                    }
                    transaction.Commit();
                            }
                            catch (Exception)
                            {
                                transaction.Rollback();
                                throw;
                            }
                        }
                    } 
 
        public void DelElement(int id)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    CannedFood element = context.CannedFoods.FirstOrDefault(rec => rec.Id == id);
                    if (element != null)
                    {
                        // удаяем записи по компонентам при удалении изделия 
                        context.TypeOfCanneds.RemoveRange(context.TypeOfCanneds.Where(rec => rec.CannedFoodId == id));
                        context.CannedFoods.Remove(element);
                        context.SaveChanges();
                    }
                    else
                    {
                        throw new Exception("Элемент не найден");
                    }
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }   
    }
}
