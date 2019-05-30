using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FishFactoryServiceDAL.BindingM;
using FishFactoryServiceDAL.ViewM;

namespace FishFactoryServiceDAL.Interfaces
{
    public interface ITypeOfFishService
    {
        List<TypeOfFishViewM> GetList();
        TypeOfFishViewM GetElement(int id);
        void AddElement(TypeOfFishBindingM model);
        void UpdElement(TypeOfFishBindingM model);
        void DelElement(int id);
    }
}
