using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using WPFTodoList.Dialogs.Views;
using WPFTodoList.Views;

namespace WPFTodoList
{
    public class MainModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            IRegionManager regionManager = containerProvider.Resolve<IRegionManager>();

            regionManager.RegisterViewWithRegion("ContentRegion", typeof(HomeView));
            regionManager.RegisterViewWithRegion("NavigationRegion", typeof(Navigation));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<HomeView>();
            containerRegistry.RegisterForNavigation<TodosView>();
            containerRegistry.RegisterForNavigation<AboutView>();

            containerRegistry.RegisterDialog<AddTodoDialog>("AddTodoDialog");
        }
    }
}
