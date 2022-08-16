using Prism.Mvvm;
using System.Collections.ObjectModel;
using WPFTodoList.Models;

namespace WPFTodoList.ViewModels
{
    public class TodosViewModel : BindableBase
    {
        public ObservableCollection<TodoItem> Todos { get; set; }

        public TodosViewModel()
        {
            Todos = new ObservableCollection<TodoItem>
            {
                new TodoItem { Title = "My First Todo" },
                new TodoItem { Title = "My Second Todo" },
                new TodoItem { Title = "My Third Todo" }
            };
        }
    }
}
