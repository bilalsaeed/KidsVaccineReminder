using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KidsVaccineReminder.DTO
{
    public class SendSMSRequestModel
    {
        public string phone_number { get; set; }
        public string message { get; set; }
        public int device_id { get; set; }

    }

    public class Log
    {
        public string status { get; set; }
        public DateTime occurred_at { get; set; }
    }

    public class ResponseModel
    {
        public int id { get; set; }
        public int device_id { get; set; }
        public string phone_number { get; set; }
        public string message { get; set; }
        public string status { get; set; }
        public List<Log> log { get; set; }
        public DateTime created_at { get; set; }
    }
}
