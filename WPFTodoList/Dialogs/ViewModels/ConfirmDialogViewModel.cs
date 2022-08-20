using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;

namespace WPFTodoList.Dialogs.ViewModels
{
    public class ConfirmDialogViewModel : BindableBase, IDialogAware
    {
        private string _message;
        private DelegateCommand _confirmCommand;

        public DelegateCommand ConfirmCommand =>
            _confirmCommand ?? (_confirmCommand = new DelegateCommand(ExecuteConfirmCommand));

        public string Title { get; set; } = "Confirm";

        public string Message
        {
            get => _message;
            set => SetProperty(ref _message, value);
        }

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
            string message = "Do you wish to proceed?";

            if (parameters.ContainsKey("Message"))
            {
                message = parameters.GetValue<string>("Message");
            }

            Message = message;
        }

        private void ExecuteConfirmCommand()
        {
            DialogResult result = new DialogResult(ButtonResult.OK);

            RequestClose?.Invoke(result);
        }
    }
}
