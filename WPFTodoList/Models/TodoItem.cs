using Prism.Mvvm;

namespace WPFTodoList.Models
{
    public class TodoItem : BindableBase
    {
        private string _title;
        private bool _isCompleted;

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        public bool IsCompleted
        {
            get => _isCompleted;
            set => SetProperty(ref _isCompleted, value);
        }
    }
}
