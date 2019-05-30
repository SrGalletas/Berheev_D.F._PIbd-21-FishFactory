using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FishFactoryServiceDAL.BindingM;
using FishFactoryServiceDAL.ViewM;

namespace FishFactoryServiceDAL.Interfaces
{
    public interface ICannedFoodService
    {
        List<CannedFoodViewM> GetList();
        CannedFoodViewM GetElement(int id);
        void AddElement(CannedFoodBindingM model);
        void UpdElement(CannedFoodBindingM model);
        void DelElement(int id);
    }
}
