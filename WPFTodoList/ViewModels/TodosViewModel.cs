using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Data;
using WPFTodoList.Models;

namespace WPFTodoList.ViewModels
{
    public class TodosViewModel : BindableBase
    {
        private ListCollectionView _viewSource;
        private TodoItem _selectedTodoItem;
        private string _searchString;
        private DelegateCommand<object> _toggleCommand;
        private DelegateCommand _filterCommand;

        public ListCollectionView ViewSource
        {
            get => _viewSource;
        }

        public TodoItem SelectedTodoItem
        {
            get => _selectedTodoItem;
            set => SetProperty(ref _selectedTodoItem, value);
        }

        public string SearchString
        {
            get => _searchString;
            set => SetProperty(ref _searchString, value);
        }

        public DelegateCommand<object> ToggleCommand =>
            _toggleCommand ?? new DelegateCommand<object>(ExecuteToggleCommand);

        public DelegateCommand FilterCommand =>
            _filterCommand ?? new DelegateCommand(ExecuteFilterCommand);

        public ObservableCollection<TodoItem> Todos { get; set; }

        public TodosViewModel()
        {
            Todos = new ObservableCollection<TodoItem>
            {
                new TodoItem { Title = "My First Todo", IsCompleted = false },
                new TodoItem { Title = "My Second Todo", IsCompleted = false },
                new TodoItem { Title = "My Third Todo", IsCompleted = false }
            };

            SelectedTodoItem = Todos.FirstOrDefault();

            _viewSource = (ListCollectionView)CollectionViewSource.GetDefaultView(Todos);
            _viewSource.SortDescriptions.Add(new SortDescription("IsCompleted", ListSortDirection.Ascending));
            _viewSource.Filter = item =>
            {
                if (item is TodoItem)
                {
                    TodoItem todoItem = item as TodoItem;

                    return string.IsNullOrEmpty(SearchString) || string.IsNullOrWhiteSpace(SearchString) ?
                        true : todoItem.Title.ToLower().Contains(SearchString.ToLower());
                }
                else
                {
                    return true;
                }
            };
        }

        private void ExecuteToggleCommand(object param)
        {
            if (param is TodoItem)
            {
                TodoItem todoItem = (TodoItem)param;

                SelectedTodoItem = todoItem;

                ViewSource.Refresh();
            }
        }

        private void ExecuteFilterCommand()
        {
            ViewSource.Refresh();
        }
    }
}
