using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;

namespace WPFTodoList.Dialogs.ViewModels
{
    public class AddTodoDialogViewModel : BindableBase, IDialogAware
    {
        public string Title => "Add Todo";

        public event Action<IDialogResult> RequestClose;

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {

        }

        public void OnDialogOpened(IDialogParameters parameters)
        {

        }
    }
}
