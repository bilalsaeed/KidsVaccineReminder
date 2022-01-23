using KidsVaccineReminder.DatabaseContext;
using KidsVaccineReminder.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KidsVaccineReminder.Repositories.Child
{
    public class ChildRepository: Repository<ChildRecord>
    {
        public ChildRepository(AppDbContext context) : base(context)
        {
        }
    }
}
