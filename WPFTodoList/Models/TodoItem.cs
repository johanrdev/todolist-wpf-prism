using Prism.Mvvm;

namespace WPFTodoList.Models
{
    public class TodoItem : BindableBase
    {
        private int _id;
        private string _title;
        private bool _isCompleted;

        public int Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

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
