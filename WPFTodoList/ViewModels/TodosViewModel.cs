using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using WPFTodoList.Models;

namespace WPFTodoList.ViewModels
{
    public class TodosViewModel : BindableBase
    {
        private TodoItem _selectedTodoItem;
        private DelegateCommand<object> _toggleCommand;

        public TodoItem SelectedTodoItem
        {
            get => _selectedTodoItem;
            set => SetProperty(ref _selectedTodoItem, value);
        }

        public DelegateCommand<object> ToggleCommand =>
            _toggleCommand ?? new DelegateCommand<object>(ExecuteToggleCommand);

        public ObservableCollection<TodoItem> Todos { get; set; }

        public TodosViewModel()
        {
            Todos = new ObservableCollection<TodoItem>
            {
                new TodoItem { Title = "My First Todo" },
                new TodoItem { Title = "My Second Todo" },
                new TodoItem { Title = "My Third Todo", IsCompleted = true }
            };

            SelectedTodoItem = Todos.FirstOrDefault();
        }

        private void ExecuteToggleCommand(object param)
        {
            if (param is TodoItem)
            {
                TodoItem todoItem = (TodoItem)param;

                Debug.WriteLine(todoItem.IsCompleted);

                SelectedTodoItem = todoItem;
            }
        }
    }
}
