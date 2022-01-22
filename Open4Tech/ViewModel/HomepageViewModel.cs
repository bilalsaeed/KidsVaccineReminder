using KidsVaccineReminder.Command;
using KidsVaccineReminder.Helper;
using KidsVaccineReminder.Model;
using KidsVaccineReminder.Properties;
using KidsVaccineReminder.ViewModel.BaseClass;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using MessageBox = System.Windows.Forms.MessageBox;

namespace KidsVaccineReminder.ViewModel
{
    public class Item
    {
        public string Name { get; set; }
        public string Icon { get; set; }
    }
    public class HomepageViewModel : BaseNotifier
    {
        #region Properties
        public Action CloseAction { get; set; }

        private Window window;

        public ICommand LogoutCommand { get; set; }

        public ICommand VaccineFormCommand { get; set; }

        public ICommand ChildRecordCommand { get; set; }

        private string _welcomeText;

        public List<Item> Items { get; set; } = new List<Item>();

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
            LogoutCommand = new RelayCommand(obj=> {
                Logout();
            });
            VaccineFormCommand = new RelayCommand(obj => {
                NavigateToVaccine(obj);
            });
            ChildRecordCommand = new RelayCommand(obj =>
            {
                NavigateToChildRecord();
            });
            WelcomeText = UserModel.Instance.Email;
            Items.Add(new Item() { Name = "Bilal", Icon = "bilal" });
            Items.Add(new Item() { Name = "Saeed", Icon = "Ameer" });
        }

        private void NavigateToChildRecord()
        {
            var childRecordViewModel = new ChildRecordViewModel(window);
            WindowManager.ChangeWindowContent(window, childRecordViewModel, Resources.LoginWindowTitle, Resources.ChildRecordForm);
        }
        #endregion

        #region Private Methods
        public void Logout()
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to log out?", "Log out", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                var loginViewModel = new LoginViewModel(window);
                WindowManager.ChangeWindowContent(window, loginViewModel, Resources.LoginWindowTitle, Resources.LoginControlPath);
            }
        }

        private void NavigateToVaccine(object obj)
        {
            var loginViewModel = new VaccineViewModel(window, obj);
            WindowManager.ChangeWindowContent(window, loginViewModel, Resources.LoginWindowTitle, Resources.VaccineView);
        }
        #endregion
    }
}
