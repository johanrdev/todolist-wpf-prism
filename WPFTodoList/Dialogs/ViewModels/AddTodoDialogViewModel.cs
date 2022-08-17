using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using WPFTodoList.Models;

namespace WPFTodoList.Dialogs.ViewModels
{
    public class AddTodoDialogViewModel : BindableBase, IDialogAware
    {
        private TodoItem _newTodo;
        private DelegateCommand _addCommand;

        public string Title => "Add Todo";

        public TodoItem NewTodo
        {
            get => _newTodo;
            set => SetProperty(ref _newTodo, value);
        }

        public DelegateCommand AddCommand =>
            _addCommand ?? new DelegateCommand(ExecuteAddCommand);

        public event Action<IDialogResult> RequestClose;

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {
            DialogResult result = new DialogResult(ButtonResult.Cancel);

            RequestClose?.Invoke(result);
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            NewTodo = new TodoItem();
            NewTodo.Id = parameters.GetValue<int>("NewId");
        }

        private void ExecuteAddCommand()
        {
            DialogParameters dialogParameters = new DialogParameters();
            dialogParameters.Add("NewTodo", NewTodo);

            DialogResult result = new DialogResult(ButtonResult.OK, dialogParameters);
            RequestClose?.Invoke(result);
        }
    }
}
