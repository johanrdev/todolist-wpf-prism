using Prism.Mvvm;
using System;

namespace WPFTodoList.Models
{
    public class TodoItem : BindableBase
    {
        private int _id;
        private string _title;
        private bool _isCompleted;
        private DateTime _created;

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

        public DateTime Created
        {
            get => _created;
            private set => SetProperty(ref _created, value);
        }

        public TodoItem()
        {
            Created = DateTime.Now;
        }
    }
}
