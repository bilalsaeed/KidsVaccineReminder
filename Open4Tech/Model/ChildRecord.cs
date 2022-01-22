using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KidsVaccineReminder.Model
{
    public class ChildRecord
    {
        public int Id { get; set; }
        public string EPICenterName { get; set; }
        public string DoctorName { get; set; }
        public string DoctorPhoneNumber { get; set; }
        public string CardNo { get; set; }
        public string ChildName { get; set; }
        public DateTime ChildDOB { get; set; }
        public string Gender { get; set; }
        public string MotherName { get; set; }
        public string FatherName { get; set; }
        public string FatherCNIC { get; set; }
        public string FatherPhoneNumber { get; set; }
        public string District { get; set; }
    }
}
