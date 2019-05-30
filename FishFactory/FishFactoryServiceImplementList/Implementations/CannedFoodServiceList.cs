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
            List<CannedFoodViewM> result = new List<CannedFoodViewM>();
            for (int i = 0; i < source.CannedFoods.Count; ++i)
            {
                // требуется дополнительно получить список компонентов для изделия и их количество
                List<TypeOfCannedViewM> typeOfCanneds = new List<TypeOfCannedViewM>();
                for (int j = 0; j < source.TypeOfCanneds.Count; ++j)
                {
                    if (source.TypeOfCanneds[j].CannedFoodId == source.CannedFoods[i].Id)
                    {
                        string typeOfFishName = string.Empty;
                        for (int k = 0; k < source.TypesOfFish.Count; ++k)
                        {
                            if (source.TypeOfCanneds[j].TypeOfFishId ==
                            source.TypesOfFish[k].Id)
                            {
                                typeOfFishName = source.TypesOfFish[k].TypeOfFishName;
                                break;
                            }
                        }
                        typeOfCanneds.Add(new TypeOfCannedViewM
                        {
                            Id = source.TypeOfCanneds[j].Id,
                            CannedFoodId = source.TypeOfCanneds[j].CannedFoodId,
                            TypeOfFishId = source.TypeOfCanneds[j].TypeOfFishId,
                            TypeOfFishName = typeOfFishName,
                            Total = source.TypeOfCanneds[j].Total
                        });
                    }
                }
                result.Add(new CannedFoodViewM
                {
                    Id = source.CannedFoods[i].Id,
                    CannedFoodName = source.CannedFoods[i].CannedFoodName,
                    Cost = source.CannedFoods[i].Cost,
                    TypeOfCanneds = typeOfCanneds
                });
            }
            return result;
        }
        public CannedFoodViewM GetElement(int id)
        {
            for (int i = 0; i < source.CannedFoods.Count; ++i)
            {
                // требуется дополнительно получить список компонентов для изделия и их количество
                List<TypeOfCannedViewM> typesOfCanned = new
                List<TypeOfCannedViewM>();
                for (int j = 0; j < source.TypeOfCanneds.Count; ++j)
                {
                    if (source.TypeOfCanneds[j].CannedFoodId == source.CannedFoods[i].Id)
                    {
                        string typeOfFishName = string.Empty;
                        for (int k = 0; k < source.TypesOfFish.Count; ++k)
                        {
                            if (source.TypeOfCanneds[j].TypeOfFishId ==
                            source.TypesOfFish[k].Id)
                            {
                                typeOfFishName = source.TypesOfFish[k].TypeOfFishName;
                                break;
                            }
                        }
                        typesOfCanned.Add(new TypeOfCannedViewM
                        {
                            Id = source.TypeOfCanneds[j].Id,
                            CannedFoodId = source.TypeOfCanneds[j].CannedFoodId,
                            TypeOfFishId = source.TypeOfCanneds[j].TypeOfFishId,
                            TypeOfFishName = typeOfFishName,
                            Total = source.TypeOfCanneds[j].Total
                        });
                    }
                }
                if (source.CannedFoods[i].Id == id)
                {
                    return new CannedFoodViewM
                    {
                        Id = source.CannedFoods[i].Id,
                        CannedFoodName = source.CannedFoods[i].CannedFoodName,
                        Cost = source.CannedFoods[i].Cost,
                        TypeOfCanneds = typesOfCanned
                    };
                }
            }
            throw new Exception("Элемент не найден");
        }
        public void AddElement(CannedFoodBindingM model)
        {
            int maxId = 0;
            for (int i = 0; i < source.CannedFoods.Count; ++i)
            {
                if (source.CannedFoods[i].Id > maxId)
                {
                    maxId = source.CannedFoods[i].Id;
                }
                if (source.CannedFoods[i].CannedFoodName == model.CannedFoodName)
                {
                    throw new Exception("Уже есть изделие с таким названием");
                }
            }
            source.CannedFoods.Add(new CannedFood
            {
                Id = maxId + 1,
                CannedFoodName = model.CannedFoodName,
                Cost = model.Cost
            });
            // компоненты для изделия
            int maxPCId = 0;
            for (int i = 0; i < source.TypeOfCanneds.Count; ++i)
            {
                if (source.TypeOfCanneds[i].Id > maxPCId)
                {
                    maxPCId = source.TypeOfCanneds[i].Id;
                }
            }
            // убираем дубли по компонентам
            for (int i = 0; i < model.TypeOfCanneds.Count; ++i)
            {
                for (int j = 1; j < model.TypeOfCanneds.Count; ++j)
                {
                    if (model.TypeOfCanneds[i].TypeOfFishId ==
                    model.TypeOfCanneds[j].TypeOfFishId)
                    {
                        model.TypeOfCanneds[i].Total +=
                        model.TypeOfCanneds[j].Total;
                        model.TypeOfCanneds.RemoveAt(j--);
                    }
                }
            }
            // добавляем компоненты
            for (int i = 0; i < model.TypeOfCanneds.Count; ++i)
            {
                source.TypeOfCanneds.Add(new TypeOfCanned
                {
                    Id = ++maxPCId,
                    CannedFoodId = maxId + 1,
                    TypeOfFishId = model.TypeOfCanneds[i].TypeOfFishId,
                    Total = model.TypeOfCanneds[i].Total
                });
            }
        }
        public void UpdElement(CannedFoodBindingM model)
        {
            int index = -1;
            for (int i = 0; i < source.CannedFoods.Count; ++i)
            {
                if (source.CannedFoods[i].Id == model.Id)
                {
                    index = i;
                }
                if (source.CannedFoods[i].CannedFoodName == model.CannedFoodName &&
                source.CannedFoods[i].Id != model.Id)
                {
                    throw new Exception("Уже есть изделие с таким названием");
                }
            }
            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }
            source.CannedFoods[index].CannedFoodName = model.CannedFoodName;
            source.CannedFoods[index].Cost = model.Cost;
            int maxPCId = 0;
            for (int i = 0; i < source.TypeOfCanneds.Count; ++i)
            {
                if (source.TypeOfCanneds[i].Id > maxPCId)
                {
                    maxPCId = source.TypeOfCanneds[i].Id;
                }
            } // обновляем существуюущие компоненты
            for (int i = 0; i < source.TypeOfCanneds.Count; ++i)
            {
                if (source.TypeOfCanneds[i].CannedFoodId == model.Id)
                {
                    bool flag = true;
                    for (int j = 0; j < model.TypeOfCanneds.Count; ++j)
                    {
                        // если встретили, то изменяем количество
                        if (source.TypeOfCanneds[i].Id ==
                        model.TypeOfCanneds[j].Id)
                        {
                            source.TypeOfCanneds[i].Total =
                            model.TypeOfCanneds[j].Total;
                            flag = false;
                            break;
                        }
                    }
                    // если не встретили, то удаляем
                    if (flag)
                    {
                        source.TypeOfCanneds.RemoveAt(i--);
                    }
                }
            }
            // новые записи
            for (int i = 0; i < model.TypeOfCanneds.Count; ++i)
            {
                if (model.TypeOfCanneds[i].Id == 0)
                {
                    // ищем дубли
                    for (int j = 0; j < source.TypeOfCanneds.Count; ++j)
                    {
                        if (source.TypeOfCanneds[j].CannedFoodId == model.Id &&
                        source.TypeOfCanneds[j].TypeOfFishId ==
                        model.TypeOfCanneds[i].TypeOfFishId)
                        {
                            source.TypeOfCanneds[j].Total +=
                            model.TypeOfCanneds[i].Total;
                            model.TypeOfCanneds[i].Id =
                            source.TypeOfCanneds[j].Id;
                            break;
                        }
                    }
                    // если не нашли дубли, то новая запись
                    if (model.TypeOfCanneds[i].Id == 0)
                    {
                        source.TypeOfCanneds.Add(new TypeOfCanned
                        {
                            Id = ++maxPCId,
                            CannedFoodId = model.Id,
                            TypeOfFishId = model.TypeOfCanneds[i].TypeOfFishId,
                            Total = model.TypeOfCanneds[i].Total
                        });
                    }
                }
            }
        }
        public void DelElement(int id)
        {
            // удаяем записи по компонентам при удалении изделия
            for (int i = 0; i < source.TypeOfCanneds.Count; ++i)
            {
                if (source.TypeOfCanneds[i].CannedFoodId == id)
                {
                    source.TypeOfCanneds.RemoveAt(i--);
                }
            }
            for (int i = 0; i < source.CannedFoods.Count; ++i)
            {
                if (source.CannedFoods[i].Id == id)
                {
                    source.CannedFoods.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }
    }
}
