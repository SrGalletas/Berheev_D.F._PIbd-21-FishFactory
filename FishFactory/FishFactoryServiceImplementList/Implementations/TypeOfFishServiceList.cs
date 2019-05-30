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
            List<TypeOfFishViewM> result = source.TypesOfFish.Select(rec => new
TypeOfFishViewM
            {
                Id = rec.Id,
                TypeOfFishName = rec.TypeOfFishName
            })
            .ToList();
            return result;
        }
        public TypeOfFishViewM GetElement(int id)
        {
            TypeOfFish element = source.TypesOfFish.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new TypeOfFishViewM
                {
                    Id = element.Id,
                    TypeOfFishName = element.TypeOfFishName
                };

            }
            throw new Exception("Элемент не найден");
        }
        public void AddElement(TypeOfFishBindingM model)
        {
            TypeOfFish element = source.TypesOfFish.FirstOrDefault(rec => rec.TypeOfFishName
            == model.TypeOfFishName);
            if (element != null)
            {
                throw new Exception("Уже есть компонент с таким названием");
            }
            int maxId = source.TypesOfFish.Count > 0 ? source.TypesOfFish.Max(rec =>
            rec.Id) : 0;
            source.TypesOfFish.Add(new TypeOfFish
            {
                Id = maxId + 1,
                TypeOfFishName = model.TypeOfFishName
            });
        }
        public void UpdElement(TypeOfFishBindingM model)
        {
            TypeOfFish element = source.TypesOfFish.FirstOrDefault(rec => rec.TypeOfFishName
            == model.TypeOfFishName && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть компонент с таким названием");
            }
            element = source.TypesOfFish.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден"); 
            }
            element.TypeOfFishName = model.TypeOfFishName;
        }
        public void DelElement(int id)
        {
            TypeOfFish element = source.TypesOfFish.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                source.TypesOfFish.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
    }
}
