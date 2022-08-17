using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Diagnostics;
using WPFTodoList.Models;

namespace WPFTodoList.Dialogs.ViewModels
{
    public class EditTodoDialogViewModel : BindableBase, IDialogAware
    {
        private TodoItem _todo;
        private DelegateCommand _updateCommand;

        public string Title => "Edit Todo";

        public TodoItem Todo
        {
            get => _todo;
            set => SetProperty(ref _todo, value);
        }

        public DelegateCommand UpdateCommand =>
            _updateCommand ?? new DelegateCommand(ExecuteUpdateCommand);
        
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
            if (!parameters.ContainsKey("Todo")) return;

            TodoItem todoItem = parameters.GetValue<TodoItem>("Todo");

            Todo = new TodoItem
            {
                Id = todoItem.Id,
                Title = todoItem.Title,
                IsCompleted = todoItem.IsCompleted
            };
        }

        private void ExecuteUpdateCommand()
        {
            DialogParameters dialogParameters = new DialogParameters();
            dialogParameters.Add("EditedTodo", Todo);

            DialogResult result = new DialogResult(ButtonResult.OK, dialogParameters);

            RequestClose?.Invoke(result);
        }
    }
}
