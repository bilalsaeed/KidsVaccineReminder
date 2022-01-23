using KidsVaccineReminder.DatabaseContext;
using KidsVaccineReminder.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KidsVaccineReminder.Repositories.Child
{
    public class ChildVaccineRepository : Repository<ChildVaccine>
    {
        public ChildVaccineRepository(AppDbContext context) : base(context)
        {
        }
    }
}
