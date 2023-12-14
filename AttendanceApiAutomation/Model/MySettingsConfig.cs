using System;
using System.Collections.Generic;
using System.Text;

namespace AttendanceApiAutomation.Model
{
    class MySettingsConfig
    {
        public string blobAccountName { get; set; }
        public string blobAccountKey { get; set; }
        public string basicUrl { get; set; }
        public string EapimUrl { get; set; }
        public string getNcYearGroup { get; set; }
        public string getAttCodeFuncKey { get; set; }
        public string getProviderFuncKey { get; set; }
        public string postAttFuncKey { get; set; }
    }
}
