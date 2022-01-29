using KidsVaccineReminder.DatabaseContext;
using KidsVaccineReminder.DTO;
using KidsVaccineReminder.SMSSenders;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace KidsVaccineReminder.BackgroundThread
{
    public class BackgroundSendMessage
    {
        private AppDbContext _context;
        private DispatcherTimer timer = new DispatcherTimer();
        private readonly string _messageText = "Mohtaram {0}, App kay Bachay {1} ki vaccination due hai , baraye mehrbani {1} ko baroz {2} tareekh {3} ko hi Chiniot Hospital rabta ker kay bachay ki vaccination kerwa lain. Shukria , CGH";
        public BackgroundSendMessage()
        {
            this._context = new AppDbContext();
            timer.Interval = TimeSpan.FromSeconds(40);
            timer.Tick += timer_Tick;
        }
        public void Start()
        {
            timer.Start();
        }
        void timer_Tick(object sender, EventArgs e)
        {
            DateTime offsetHour = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day + 2);
            var repo = (from child in _context.ChildRecord
                        join vaccine in _context.ChildVaccine.Where(a => a.NextVaccincationDate.Value == offsetHour && a.Vaccinated == 0)
                        on child.Id equals vaccine.ChildRecordId
                        select new
                        {
                            childVaccineId = vaccine.Id,
                            child.ChildName,
                            child.FatherName,
                            child.FatherPhoneNumber,
                            NextVaccincationDate = vaccine.NextVaccincationDate.Value,
                            VaccinationDay = vaccine.NextVaccincationDate.Value
                        }).ToList();

            var sendSmsList = repo.Select(item => new SendSMSRequestModel()
            {
                message = ParseMessage(item.FatherName,item.ChildName,item.VaccinationDay),
                phone_number = item.FatherPhoneNumber
            }).ToList();
            var output = SMSGatewayME.SendSMS(sendSmsList);
            if (output.Count > 0)
            {
                var ids = repo.Select(a => a.childVaccineId);
                var updateVaccineStatus = this._context.ChildVaccine.Where(a => ids.Contains(a.Id));
                foreach (var r in updateVaccineStatus)
                    r.Vaccinated = 1;
                this._context.SaveChanges();
            }
        }

        private string ParseMessage(string fatherName, string childName, DateTime vaccineDate)
        {
            return string.Format(_messageText, fatherName, childName, GetUrduWeekName(vaccineDate.DayOfWeek), vaccineDate.ToString("dd/MM/yyyy"));
        }

        private string GetUrduWeekName(DayOfWeek dayOfWeek)
        {
            switch (dayOfWeek)
            {
                case DayOfWeek.Monday:
                    return "Peer";
                case DayOfWeek.Tuesday:
                    return "Mangal";
                case DayOfWeek.Wednesday:
                    return "Budh";
                case DayOfWeek.Thursday:
                    return "Jumerat";
                case DayOfWeek.Friday:
                    return "Jumma";
                case DayOfWeek.Saturday:
                    return "Hafta";
                case DayOfWeek.Sunday:
                    return "Itwar";
                default:
                    return "Peer";

            }
        }
    }
}
