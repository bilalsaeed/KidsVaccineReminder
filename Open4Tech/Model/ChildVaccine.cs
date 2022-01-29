using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KidsVaccineReminder.Model
{
    public class ChildVaccine
    {
        [Key]
        public int Id { get; set; }
        public string ValidAge { get; set; }
        public string VaccineName { get; set; }
        public DateTime? VaccinationDate { get; set; }
        public  string VaccinationSource { get; set; }
        public DateTime? NextVaccincationDate { get; set; }
        public string DoctorName { get; set; }
        public int ChildRecordId { get; set; }
        public int Vaccinated { get; set; } = 0;
    }
}
