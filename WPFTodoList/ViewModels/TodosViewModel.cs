using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
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
        private IDialogService _dialogService;
        private ListCollectionView _viewSource;
        private TodoItem _selectedTodoItem;
        private string _searchString;
        private DelegateCommand<object> _toggleCommand;
        private DelegateCommand _filterCommand;
        private DelegateCommand _openAddTodoDialogCommand;
        private DelegateCommand<object> _openEditTodoDialogCommand;

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

        public DelegateCommand OpenAddTodoDialogCommand =>
            _openAddTodoDialogCommand ?? new DelegateCommand(ExecuteOpenAddTodoDialogCommand);

        public DelegateCommand<object> OpenEditTodoDialogCommand =>
            _openEditTodoDialogCommand ?? new DelegateCommand<object>(ExecuteOpenEditTodoDialogCommand);

        public ObservableCollection<TodoItem> Todos { get; set; }

        public TodosViewModel(IDialogService dialogService)
        {
            _dialogService = dialogService;

            Todos = new ObservableCollection<TodoItem>
            {
                new TodoItem { Id = 1, Title = "My First Todo", IsCompleted = false },
                new TodoItem { Id = 2, Title = "My Second Todo", IsCompleted = false },
                new TodoItem { Id = 3, Title = "My Third Todo", IsCompleted = true },
                new TodoItem { Id = 4, Title = "My Fourth Todo", IsCompleted = false },
                new TodoItem { Id = 5, Title = "My Fifth Todo", IsCompleted = false }
            };

            SelectedTodoItem = Todos.FirstOrDefault();

            InitViewSource();
        }

        private void InitViewSource()
        {
            _viewSource = (ListCollectionView)CollectionViewSource.GetDefaultView(Todos);
            _viewSource.SortDescriptions.Add(new SortDescription("Title", ListSortDirection.Ascending));
            _viewSource.IsLiveSorting = true;
            _viewSource.LiveSortingProperties.Add("Title");
            _viewSource.Filter = ViewSourceFilter;
        }

        private bool ViewSourceFilter(object item)
        {
            if (item is TodoItem)
            {
                TodoItem todoItem = item as TodoItem;

                return string.IsNullOrEmpty(SearchString) || string.IsNullOrWhiteSpace(SearchString) ?
                    !todoItem.IsCompleted : !todoItem.IsCompleted && todoItem.Title.ToLower().Contains(SearchString.ToLower());
            }
            else
            {
                return true;
            }
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

        private void ExecuteOpenAddTodoDialogCommand()
        {
            _dialogService.ShowDialog("AddTodoDialog", null, result =>
            {
                if (result.Result == ButtonResult.OK)
                {
                    TodoItem newTodo = result.Parameters.GetValue<TodoItem>("NewTodo");

                    Todos.Add(newTodo);

                    ViewSource.Refresh();
                }
            });
        }

        private void ExecuteOpenEditTodoDialogCommand(object param)
        {
            if (param is TodoItem)
            {
                TodoItem todoItem = (TodoItem)param;

                SelectedTodoItem = todoItem;

                DialogParameters dialogParameters = new DialogParameters();
                dialogParameters.Add("Todo", todoItem);

                _dialogService.ShowDialog("EditTodoDialog", dialogParameters, result =>
                {
                    if (result.Result == ButtonResult.OK)
                    {
                        TodoItem updatedTodoItem = result.Parameters.GetValue<TodoItem>("EditedTodo");
                        TodoItem existingTodoItem = Todos.Where(t => t.Id == updatedTodoItem.Id).FirstOrDefault();

                        existingTodoItem.Title = updatedTodoItem.Title;
                    }
                });
            }
        }
    }
}
