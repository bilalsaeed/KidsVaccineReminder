using KidsVaccineReminder.Helper;
using KidsVaccineReminder.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace KidsVaccineReminder.ViewModel.BaseClass
{
    public abstract class BaseNotifier : INotifyPropertyChanged
    {
        public ICommand GoHomeCommand { get; set; }
        protected virtual void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public virtual void GoHome(Window window)
        {
            var homepageViewModel = new HomepageViewModel(window);
            WindowManager.ChangeWindowContent(window, homepageViewModel, Resources.HomepageWindowTitle, Resources.HomepageControlPath);
        }
    }
}
