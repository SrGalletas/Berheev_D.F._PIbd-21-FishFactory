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
    public class TypeOfFishServiceDb : ITypeOfFishService
    {
        private AbstractDbEnvironment context;

        public TypeOfFishServiceDb(AbstractDbEnvironment context)
        {
            this.context = context;
        }

        public List<TypeOfFishViewM> GetList()
        {
            List<TypeOfFishViewM> result = context.TypesOfFish.Select(rec => new TypeOfFishViewM
            {
                Id = rec.Id,
                TypeOfFishName = rec.TypeOfFishName
            })
            .ToList();
            return result;
        }

        public TypeOfFishViewM GetElement(int id)
        {
            TypeOfFish element = context.TypesOfFish.FirstOrDefault(rec => rec.Id == id);
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
            TypeOfFish element = context.TypesOfFish.FirstOrDefault(rec => rec.TypeOfFishName == model.TypeOfFishName);
            if (element != null)
            {
                throw new Exception("Уже есть такое изделие");
            }
            context.TypesOfFish.Add(new TypeOfFish
            {
                TypeOfFishName = model.TypeOfFishName
            });
            context.SaveChanges();
        }

        public void UpdElement(TypeOfFishBindingM model)
        {
            TypeOfFish element = context.TypesOfFish.FirstOrDefault(rec => rec.TypeOfFishName == model.TypeOfFishName && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть такой компонент");
            }
            element = context.TypesOfFish.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.TypeOfFishName = model.TypeOfFishName;
            context.SaveChanges();
        }

        public void DelElement(int id)
        {
            TypeOfFish element = context.TypesOfFish.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                context.TypesOfFish.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
    }
}
