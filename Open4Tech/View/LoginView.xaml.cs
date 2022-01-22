using KidsVaccineReminder.Model;
using KidsVaccineReminder.ViewModel;
using System.Windows;

namespace KidsVaccineReminder.View
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        public LoginView()
        {
            InitializeComponent();
            DataContext = new LoginViewModel(this);
        }

        private void LoginControl_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
