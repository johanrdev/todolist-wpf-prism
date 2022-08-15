using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System.Collections.ObjectModel;
using System.Linq;
using WPFTodoList.Models;

namespace WPFTodoList.ViewModels
{
    public class NavigationViewModel : BindableBase
    {
        private IRegionManager _regionManager;
        private NavigationItem _selectedNavigationItem;
        private DelegateCommand<object> _navigateCommand;

        public NavigationItem SelectedNavigationItem
        {
            get => _selectedNavigationItem;
            set => SetProperty(ref _selectedNavigationItem, value);
        }

        public DelegateCommand<object> NavigateCommand => 
            _navigateCommand ?? (_navigateCommand = new DelegateCommand<object>(ExecuteNavigateCommand));

        public ObservableCollection<NavigationItem> NavigationItems { get; set; }

        public NavigationViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;

            NavigationItems = new ObservableCollection<NavigationItem>
            {
                new NavigationItem { DisplayName = "Home", Uri = "HomeView" },
                new NavigationItem { DisplayName = "Todos", Uri = "TodosView" },
                new NavigationItem { DisplayName = "About", Uri = "AboutView" }
            };

            SelectedNavigationItem = NavigationItems.FirstOrDefault();
        }

        private void ExecuteNavigateCommand(object param)
        {
            if (param is NavigationItem)
            {
                NavigationItem navItem = (NavigationItem)param;

                _regionManager.RequestNavigate("ContentRegion", navItem.Uri);
            }
        }
    }
}