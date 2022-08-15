using Prism.Mvvm;

namespace WPFTodoList.Models
{
    public class NavigationItem : BindableBase
    {
        private string _displayName;

        public string DisplayName
        {
            get => _displayName;
            set => SetProperty(ref _displayName, value);
        }


        private string _uri;
        public string Uri
        {
            get => _uri;
            set => SetProperty(ref _uri, value);
        }
    }
}