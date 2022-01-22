using KidsVaccineReminder.ViewModel.BaseClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace KidsVaccineReminder.ViewModel
{
    public class VaccineViewModel : BaseNotifier
    {
        private Window window;
        public Item item = new Item();
        #region Constructor
        public VaccineViewModel(Window window, object item)
         {
            this.window = window;
            if(item is Item)
            {
                this.item = (Item)item;
            }
        }
        #endregion
    }
}
