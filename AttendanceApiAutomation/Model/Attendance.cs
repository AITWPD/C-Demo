using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AttendanceApiAutomation.Model
{
    class AttendanceResp
    {
        public partial class AttendanceResponse
        {
            [JsonProperty("AttendanceCode")]
            public string AttendanceCode { get; set; }

            [JsonProperty("AttendanceDescription")]
            public string AttendanceDescription { get; set; }
        }

    }
}
