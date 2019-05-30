using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FishFactoryModel;
using FishFactoryServiceDAL.BindingM;
using FishFactoryServiceDAL.ViewM;
using FishFactoryServiceDAL.Interfaces;
using System.Linq;

namespace FishFactoryServiceImplementList.Implementations
{
    public class CustomerServiceList : ICustomerService
    {
        private DataListSingleton source;
        public CustomerServiceList()
        {
            source = DataListSingleton.GetInstance();
        }
        public List<CustomerViewM> GetList()
        {
            List<CustomerViewM> result = source.Customers.Select(rec => new CustomerViewM
            {
                Id = rec.Id,
                CustomerFIO = rec.CustomerFIO
            })

            .ToList();
            return result;
        }
        public CustomerViewM GetElement(int id)
        {
            Customer element = source.Customers.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new CustomerViewM
                {
                    Id = element.Id,
                    CustomerFIO = element.CustomerFIO
                };

            }
            throw new Exception("Элемент не найден");
        }
        public void AddElement(CustomerBindingM model)
        {
            Customer element = source.Customers.FirstOrDefault(rec => rec.CustomerFIO ==
    model.CustomerFIO);
            if (element != null)
            {
                throw new Exception("Уже есть клиент с таким ФИО");
            }
            int maxId = source.Customers.Count > 0 ? source.Customers.Max(rec => rec.Id) : 0;
            source.Customers.Add(new Customer
            {
                Id = maxId + 1,
                CustomerFIO = model.CustomerFIO
            });
        }
        public void UpdElement(CustomerBindingM model)
        {
            Customer element = source.Customers.FirstOrDefault(rec => rec.CustomerFIO ==
            model.CustomerFIO && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть клиент с таким ФИО");
            }
            element = source.Customers.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.CustomerFIO = model.CustomerFIO;
        }
        public void DelElement(int id)
        {
            Customer element = source.Customers.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                source.Customers.Remove(element);
            }
            else

            {
                throw new Exception("Элемент не найден");
            }
        }
    }
}
