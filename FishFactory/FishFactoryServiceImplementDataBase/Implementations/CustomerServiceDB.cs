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
    public class CustomerServiceDb : ICustomerService
    {
        private AbstractDbEnvironment context;

        public CustomerServiceDb(AbstractDbEnvironment context)
        {
            this.context = context;
        }

        public List<CustomerViewM> GetList()
        {
            List<CustomerViewM> result = context.Customers.Select(rec => new CustomerViewM
            {
                Id = rec.Id,
                CustomerFIO = rec.CustomerFIO
            })
            .ToList();
            return result;
        }

        public CustomerViewM GetElement(int id)
        {
            Customer element = context.Customers.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new CustomerViewM
                {
                    Id = element.Id,
                    CustomerFIO = element.CustomerFIO };
            }
            throw new Exception("Элемент не найден");
        }

        public void AddElement(CustomerBindingM model)
        {
            Customer element = context.Customers.FirstOrDefault(rec => rec.CustomerFIO == model.CustomerFIO);
            if (element != null)
            {
                throw new Exception("Уже есть клиент с таким ФИО");
            }
            context.Customers.Add(new Customer
            {
                CustomerFIO = model.CustomerFIO
            });
            context.SaveChanges();
        }

        public void UpdElement(CustomerBindingM model)
        {
            Customer element = context.Customers.FirstOrDefault(rec => rec.CustomerFIO == model.CustomerFIO && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть клиент с таким ФИО");
            }
            element = context.Customers.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.CustomerFIO = model.CustomerFIO;
            context.SaveChanges();
        }

        public void DelElement(int id)
        {
            Customer element = context.Customers.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                context.Customers.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
    }
}
