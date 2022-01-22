using KidsVaccineReminder.Command;
using KidsVaccineReminder.Helper;
using KidsVaccineReminder.Model;
using KidsVaccineReminder.Properties;
using KidsVaccineReminder.ViewModel.BaseClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace KidsVaccineReminder.ViewModel
{
    public class ChildRecordViewModel:BaseNotifier
    {
        private Window window;
        public ChildRecord ChildRecord { get; set; }
        public ICommand SaveCommand { get; set; }
        #region Constructor
        public ChildRecordViewModel(Window window)
        {
            this.window = window;
            ChildRecord = new ChildRecord();

            SaveCommand = new RelayCommand(obj =>
            {
                SaveChildRecord();
            });

            GoHomeCommand = new RelayCommand(obj => {
                GoHome(window);
            });
            
        }
        #endregion

        private void SaveChildRecord()
        {

        }
    }
}
