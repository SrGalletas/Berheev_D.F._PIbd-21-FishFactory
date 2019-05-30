using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FishFactoryServiceDAL.BindingM;
using FishFactoryServiceDAL.ViewM;

namespace FishFactoryServiceDAL.Interfaces
{
    public interface IStorageService
    {
        List<StorageViewM> GetList();
        StorageViewM GetElement(int id);
        void AddElement(StorageBindingM model);
        void UpdElement(StorageBindingM model);
        void DelElement(int id);
    }
}
