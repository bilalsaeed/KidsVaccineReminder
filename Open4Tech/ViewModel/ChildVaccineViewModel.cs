using KidsVaccineReminder.Command;
using KidsVaccineReminder.DatabaseContext;
using KidsVaccineReminder.Model;
using KidsVaccineReminder.Repositories.Child;
using KidsVaccineReminder.ViewModel.BaseClass;
using MvvmDialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using KidsVaccineReminder.Common;
namespace KidsVaccineReminder.ViewModel
{
    public class ChildVaccineViewModel : BaseNotifier, IModalDialogViewModel
    {
        private bool? dialogResult;

        public ICommand CloseWindow { get; set; }
        public ICommand SaveAllCommand { get; set; }
        public ChildRecord ChildRecord { get; set; }

        private AppDbContext _context = new AppDbContext();
        private ChildVaccineRepository repo;
        private ObservableCollection<KidsVaccineReminder.Model.ChildVaccine> _vaccines;
        public ObservableCollection<KidsVaccineReminder.Model.ChildVaccine> Vaccines
        {
            get => _vaccines;
            set
            {
                _vaccines = value;
                base.OnPropertyChanged("Vaccines");
            }
        }

        public string Title { get; set; }

        #region Constructor
        public ChildVaccineViewModel(IDialogService dialogService, ChildRecord childRecord)
        {
            repo = new ChildVaccineRepository(this._context);
            CloseWindow = new RelayCommand(obj =>
            {
                DialogResult = true;
                dialogService.Close(this);
            });

            SaveAllCommand = new RelayCommand(obj => {
                SaveAll();
            });

            ChildRecord = childRecord;
            Title = $"Enter vaccine schedule of {childRecord.ChildName}";
            Vaccines = new ObservableCollection<Model.ChildVaccine>( repo.GetByCriteria(a => a.ChildRecordId == ChildRecord.Id).Result);

        }
        public bool? DialogResult
        {
            get => dialogResult;
            private set => dialogResult = value;
        }

        private void SaveAll()
        {
            var notNulls = Vaccines.Where(a => !a.IsAnyNullOrEmpty());
            if(notNulls.Count() < 1 || notNulls.Count() != Vaccines.Count)
            {
                MessageBox.Show("You must fill complete form");
                return;
            }
            foreach(var cv in Vaccines)
            {
                cv.ChildRecordId = ChildRecord.Id;
                if (cv.Id == 0)
                    repo.Add(cv);
                else
                    repo.Update(cv);
            }
            repo.Save();
        }
        #endregion
    }
}
