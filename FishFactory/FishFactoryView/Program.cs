using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using FishFactoryServiceDAL.Interfaces;
using FishFactoryServiceImplementList.Implementations;
using Unity;
using Unity.Lifetime;

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
            currentContainer.RegisterType<ICustomerService, CustomerServiceList>(new
            HierarchicalLifetimeManager());
            currentContainer.RegisterType<ITypeOfFishService, TypeOfFishServiceList>(new
HierarchicalLifetimeManager());
            currentContainer.RegisterType<ICannedFoodService, CannedFoodServiceList>(new
            HierarchicalLifetimeManager());
            currentContainer.RegisterType<IMainService, MainServiceList>(new
            HierarchicalLifetimeManager());
            currentContainer.RegisterType<IStorageService, StorageServiceList>(new
            HierarchicalLifetimeManager());
            return currentContainer;
        }
    }
}
