using KidsVaccineReminder.Command;
using KidsVaccineReminder.Common;
using KidsVaccineReminder.DatabaseContext;
using KidsVaccineReminder.Helper;
using KidsVaccineReminder.Model;
using KidsVaccineReminder.Properties;
using KidsVaccineReminder.Repositories.Child;
using KidsVaccineReminder.ViewModel.BaseClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using MessageBox = System.Windows.Forms.MessageBox;
namespace KidsVaccineReminder.ViewModel
{
    public class ChildRecordViewModel : BaseNotifier
    {
        private Window window;
        private ChildRecord _childRecord;
        public ChildRecord ChildRecord
        {
            get
            {
                return _childRecord;
            }
            set
            {
                _childRecord = value;
                OnPropertyChanged("ChildRecord");
            }
        }
        public ICommand SaveCommand { get; set; }

        private AppDbContext _context = new AppDbContext();
        #region Constructor
        public ChildRecordViewModel(Window window, ChildRecord record = null)
        {
            this.window = window;

            if (record.IsObjectNullOrEmpty())
                ChildRecord = new ChildRecord();
            else
                ChildRecord = record;

            SaveCommand = new RelayCommand(obj =>
            {
                SaveChildRecord();
            });

            GoHomeCommand = new RelayCommand(obj =>
            {
                GoHome(window);
            });

        }
        #endregion

        private void SaveChildRecord()
        {
            //if (!this.ChildRecord.IsAnyNullOrEmpty())
            //{
            var childRepo = new ChildRepository(this._context);
            if (this.ChildRecord.Id == 0)
                childRepo.Add(this.ChildRecord);
            else
                childRepo.Update(this.ChildRecord);
            childRepo.Save();

            DialogResult dialogResult = MessageBox.Show("Do you want to add another record?", "Add a new child record", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                ChildRecord = new ChildRecord();
            }
            else
            {
                GoHome(window);
            }
            //}
            //else
            // {
            //     MessageBox.Show("Please fill complete form!");
            //  }
        }
    }
}
