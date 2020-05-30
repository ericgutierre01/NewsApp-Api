using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsAppApi.Helpers
{
    public class AppSettings
    {
        public string PathImg { get; set; }
        public string DomainImg { get; set; }
        public string FCM_SERVER_API_KEY { get; set; }
        public string FCM_SENDER_ID { get; set; }
    }
}
