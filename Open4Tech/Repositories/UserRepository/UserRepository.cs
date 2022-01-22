using KidsVaccineReminder.DatabaseContext;
using KidsVaccineReminder.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KidsVaccineReminder.Repositories.UserRepository
{
    public class UserRepository : Repository<UserModel>
    {
        public UserRepository(AppDbContext context) : base(context)
        {
        }
    }
}
