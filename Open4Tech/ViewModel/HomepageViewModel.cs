using KidsVaccineReminder.Command;
using KidsVaccineReminder.DatabaseContext;
using KidsVaccineReminder.Helper;
using KidsVaccineReminder.Model;
using KidsVaccineReminder.Properties;
using KidsVaccineReminder.Repositories.Child;
using KidsVaccineReminder.View;
using KidsVaccineReminder.ViewModel.BaseClass;
using MvvmDialogs;
using MvvmDialogs.DialogTypeLocators;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using MessageBox = System.Windows.Forms.MessageBox;

namespace KidsVaccineReminder.ViewModel
{
    public class HomepageViewModel : BaseNotifier
    {
        #region Properties
        public Action CloseAction { get; set; }

        private Window window;

        private AppDbContext _context = new AppDbContext();
        ChildRepository childRepo;
        public ICommand LogoutCommand { get; set; }

        public ICommand VaccineFormCommand { get; set; }

        public ICommand ChildRecordCommand { get; set; }

        public ICommand EditChildRecord { get; set; }

        public ICommand DeleteChildRecord { get; set; }

        private string _welcomeText;

        private List<ChildRecord> _item = new List<ChildRecord>();

        private readonly IDialogService dialogService;

        public List<ChildRecord> Items
        {
            get
            {
                return _item;
            }
            set
            {
                _item = value;
                base.OnPropertyChanged("Items");
            }
        }

        public string WelcomeText
        {
            get { return _welcomeText; }
            set
            {
                if (_welcomeText == value) return;
                _welcomeText = value;
                base.OnPropertyChanged("WelcomeText");
            }
        }
        #endregion

        #region Constructor
        public HomepageViewModel(Window window)
        {
            this.window = window;

            this.dialogService = new DialogService(dialogTypeLocator: new MyCustomDialogTypeLocator());
            LogoutCommand = new RelayCommand(obj =>
            {
                Logout();
            });
            VaccineFormCommand = new RelayCommand(obj =>
            {
                OpenChildVaccines(obj);
            });

            ChildRecordCommand = new RelayCommand(obj =>
            {
                NavigateToChildRecord();
            });

            EditChildRecord = new RelayCommand(childRecord =>
            {
                NavigateToChildRecord((ChildRecord)childRecord);
            });

            DeleteChildRecord = new RelayCommand(childRecord =>
            {
                DeleteChild((ChildRecord)childRecord);
            });

            WelcomeText = UserModel.Instance.Email;

            childRepo = new ChildRepository(this._context);
            Items = childRepo.GetAll().Result;
        }
        #endregion

        #region Private Methods
        private void NavigateToChildRecord(ChildRecord record = null)
        {
            var childRecordViewModel = new ChildRecordViewModel(window, record);
            WindowManager.ChangeWindowContent(window, childRecordViewModel, Resources.LoginWindowTitle, Resources.ChildRecordForm);
        }
        public void Logout()
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to log out?", "Log out", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                var loginViewModel = new LoginViewModel(window);
                WindowManager.ChangeWindowContent(window, loginViewModel, Resources.LoginWindowTitle, Resources.LoginControlPath);
            }
        }

        private void OpenChildVaccines(object childRecord)
        {
            var result = dialogService.ShowDialog(this, new ChildVaccineViewModel(dialogService,(ChildRecord)childRecord));
        }

        private void DeleteChild(ChildRecord record)
        {
            DialogResult dialogResult = MessageBox.Show("Do you want to delete this record?", "Delete!", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                childRepo.Delete(record);
                childRepo.Save();
                this.Items = childRepo.GetAll().Result;
            }
        }

        #endregion
    }

    public class MyCustomDialogTypeLocator : IDialogTypeLocator
    {
        public Type Locate(INotifyPropertyChanged viewModel)
        {
            Type viewModelType = viewModel.GetType();
            string viewModelTypeName = viewModelType.FullName;

            // Get dialog type name by removing the 'VM' suffix
            string dialogTypeName = viewModelTypeName.Substring(
                0,
                viewModelTypeName.Length - "ViewModel".Length);

            return viewModelType.Assembly.GetType(dialogTypeName);
        }
    }
}
