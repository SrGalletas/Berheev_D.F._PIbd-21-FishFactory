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
    public class TypeOfFishServiceList : ITypeOfFishService
    {
        private DataListSingleton source;
        public TypeOfFishServiceList()
        {
            source = DataListSingleton.GetInstance();
        }
        public List<TypeOfFishViewM> GetList()
        {
            List<TypeOfFishViewM> result = new List<TypeOfFishViewM>();
            for (int i = 0; i < source.TypesOfFish.Count; ++i)
            {
                result.Add(new TypeOfFishViewM
                {
                    Id = source.TypesOfFish[i].Id,
                    TypeOfFishName = source.TypesOfFish[i].TypeOfFishName
                });
            }
            return result;
        }
        public TypeOfFishViewM GetElement(int id)
        {
            for (int i = 0; i < source.TypesOfFish.Count; ++i)
            {
                if (source.TypesOfFish[i].Id == id)
                {
                    return new TypeOfFishViewM
                    {
                        Id = source.TypesOfFish[i].Id,
                        TypeOfFishName = source.TypesOfFish[i].TypeOfFishName
                    };
                }
            }
            throw new Exception("Элемент не найден");
        }
        public void AddElement(TypeOfFishBindingM model)
        {
            int maxId = 0;
            for (int i = 0; i < source.TypesOfFish.Count; ++i)
            {
                if (source.TypesOfFish[i].Id > maxId)
                {
                    maxId = source.TypesOfFish[i].Id;
                }
                if (source.TypesOfFish[i].TypeOfFishName == model.TypeOfFishName)
                {
                    throw new Exception("Уже есть компонент с таким названием");
                }
            }
            source.TypesOfFish.Add(new TypeOfFish
            {
                Id = maxId + 1,
                TypeOfFishName = model.TypeOfFishName
            });
        }
        public void UpdElement(TypeOfFishBindingM model)
        {
            int index = -1;

            for (int i = 0; i < source.TypesOfFish.Count; ++i)
            {
                if (source.TypesOfFish[i].Id == model.Id)
                {
                    index = i;
                }
                if (source.TypesOfFish[i].TypeOfFishName == model.TypeOfFishName &&
                source.TypesOfFish[i].Id != model.Id)
                {
                    throw new Exception("Уже есть компонент с таким названием");
                }
            }
            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }
            source.TypesOfFish[index].TypeOfFishName = model.TypeOfFishName;
        }
        public void DelElement(int id)
        {
            for (int i = 0; i < source.TypesOfFish.Count; ++i)
            {
                if (source.TypesOfFish[i].Id == id)
                {
                    source.TypesOfFish.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }
    }
}
