using KidsVaccineReminder.Model;
using System.Windows;
using System.Windows.Input;
using System.ComponentModel;
using KidsVaccineReminder.Helper;
using KidsVaccineReminder.Properties;
using System.Net.Mail;
using System;
using KidsVaccineReminder.Command;
using System.IO;
using System.Threading.Tasks;
using KidsVaccineReminder.ViewModel.BaseClass;
using KidsVaccineReminder.DatabaseContext;
using KidsVaccineReminder.Repositories.UserRepository;

namespace KidsVaccineReminder.ViewModel
{
    class LoginViewModel : BaseNotifier
    {
        #region Properties

        private Window window;

        public ICommand LoginCommand { get; set; }

        public ICommand RegisterCommand { get; set; }

        public ICommand ForgotPasswordCommand { get; set; }

        public Action CloseAction { get; set; }
        public UserModel UserModel { get; } = new UserModel();
        private AppDbContext context;


        #endregion

        #region Constructor
        public LoginViewModel(Window window)
        {
            this.window = window;
            LoginCommand = new RelayCommand(obj => { Login(); });
            this.context = new AppDbContext();
        }
        #endregion


        public void Login()
        {
            UserRepository userRepository = new UserRepository(context);
            UserModel.Instance.Email = this.UserModel.Email;
            if (this.UserModel.Email == null || UserModel.Instance.Password == null)
            {
                MessageBox.Show("Both email and password should be filled in.");
                return;
            }
            var user = userRepository.Get(a => a.Email == this.UserModel.Email).Result;
            if (user != null)
            {
                var homepageViewModel = new HomepageViewModel(window);
                WindowManager.ChangeWindowContent(window, homepageViewModel, Resources.HomepageWindowTitle, Resources.HomepageControlPath);

                if (homepageViewModel.CloseAction == null)
                {
                    homepageViewModel.CloseAction = () => window.Close();
                }
            }

            else
            {


                userRepository.Add(new UserModel() { Email = UserModel.Email, Password = UserModel.Instance.Password });
                userRepository.Save();
            }
        }
    }
}
