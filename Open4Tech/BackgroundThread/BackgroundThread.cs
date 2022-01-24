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
        public BackgroundSendMessage()
        {
            this._context = new AppDbContext();
            timer.Interval = TimeSpan.FromSeconds(5);
            timer.Tick += timer_Tick;
        }
        public void Start()
        {
            timer.Start();
        }
        void timer_Tick(object sender, EventArgs e)
        {
            DateTime offsetHour =new DateTime(DateTime.Now.Year,DateTime.Now.Month,DateTime.Now.Day + 2);
            var repo = (from child in _context.ChildRecord
                        join vaccine in _context.ChildVaccine.Where(a => a.NextVaccincationDate.Value == offsetHour)
                        on child.Id equals vaccine.ChildRecordId
                        select new
                        {
                            child.ChildName,
                            child.FatherName,
                            child.FatherPhoneNumber,
                            NextVaccincationDate = vaccine.NextVaccincationDate.Value,
                            VaccinationDay = vaccine.NextVaccincationDate.Value
                        }).ToList();

            var sendSmsList = repo.Select(item => new SendSMSRequestModel()
            {
                message = $"Your vaccine is due on {item.NextVaccincationDate.ToString("d")}",
                phone_number = item.FatherPhoneNumber
            }).ToList();
            SMSGatewayME.SendSMS(sendSmsList);
        }
    }
}
