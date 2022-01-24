using KidsVaccineReminder.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Net.Http;
using Newtonsoft.Json;

namespace KidsVaccineReminder.SMSSenders
{
    public class SMSGatewayME
    {
        private static string ApiKey = ConfigurationManager.AppSettings["SMSGatewayKey"].ToString();
        private static string ApiURL = ConfigurationManager.AppSettings["SMSGatewayAPIUrl"].ToString();
        private static int DeviceId = int.Parse(ConfigurationManager.AppSettings["DeviceID"].ToString());
        private static string SEND_SMS_ENDPOINT = "message/send";
        public static List<ResponseModel> SendSMS(List<SendSMSRequestModel> sendSMSRequestModel)
        {
            StringBuilder str = new StringBuilder();
            foreach (var i in sendSMSRequestModel)
                i.device_id = DeviceId;

            var json = JsonConvert.SerializeObject(sendSMSRequestModel);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            using(HttpClient client = new HttpClient())
            {
                var targetUri = new Uri(ApiURL + SEND_SMS_ENDPOINT, UriKind.Absolute);
                client.BaseAddress = new Uri(ApiURL + SEND_SMS_ENDPOINT);
                client.DefaultRequestHeaders.Add("Authorization", ApiKey);
                var response = client.PostAsync(targetUri, data).Result;
                str.Append(response.Content.ReadAsStringAsync().Result);

            }

            return JsonConvert.DeserializeObject<List<ResponseModel>>(str.ToString());
        }
    }
}
