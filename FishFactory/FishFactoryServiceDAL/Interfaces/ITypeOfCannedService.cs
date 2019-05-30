using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FishFactoryServiceDAL.BindingM;
using FishFactoryServiceDAL.ViewM;


namespace FishFactoryServiceDAL.Interfaces
{
    public interface ITypeOfCannedService
    {
        List<TypeOfCannedViewM> GetList();
        TypeOfCannedViewM GetElement(int id);
        void AddElement(TypeOfCannedBindingM model);
        void UpdElement(TypeOfCannedBindingM model);
        void DelElement(int id);
    }
}
