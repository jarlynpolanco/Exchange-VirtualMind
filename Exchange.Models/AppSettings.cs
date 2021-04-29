using System.Collections.Generic;

namespace Exchange.Models
{
    public class AppSettings
    {
        public string ExchangeRateService { get; set; }
         
        public IList<ConnectionString> ConnectionStrings { get; set; }
    }
}
