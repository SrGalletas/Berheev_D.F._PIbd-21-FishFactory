using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using FishFactoryServiceDAL.Interfaces;
using FishFactoryServiceImplementList.Implementations;
using FishFactoryServiceImplementDataBase;
using Unity;
using Unity.Lifetime;
using AbstractGarmentFactoryServiceImplementDataBase.Implementations;
using FishFactoryServiceImplementDataBase.Implementations;

namespace FishFactoryView
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var container = BuildUnityContainer();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(container.Resolve<FormMain>());
        }
        public static IUnityContainer BuildUnityContainer()
        {
            var currentContainer = new UnityContainer();
            currentContainer.RegisterType<IReptService, ReptServiceDb>(new
            HierarchicalLifetimeManager());
            currentContainer.RegisterType<ICustomerService, CustomerServiceDb>(new
            HierarchicalLifetimeManager());
            currentContainer.RegisterType<ITypeOfFishService, TypeOfFishServiceDb>(new
HierarchicalLifetimeManager());
            currentContainer.RegisterType<ICannedFoodService, CannedFoodServiceDb>(new
            HierarchicalLifetimeManager());
            currentContainer.RegisterType<IMainService, MainServiceDb>(new
            HierarchicalLifetimeManager());
            currentContainer.RegisterType<IStorageService, StorageServiceDb>(new
            HierarchicalLifetimeManager());
            
            currentContainer.RegisterType<DbContext, AbstractDbEnvironment>(new HierarchicalLifetimeManager());
            return currentContainer;
            
        }
    }
}
