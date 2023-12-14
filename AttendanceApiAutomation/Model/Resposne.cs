using System;
using System.Collections.Generic;
using System.Text;

namespace AttendanceApiAutomation.Model
{
    class Resposne
    {
        public string submissionRef { get; set; }

        public Errors[] errors { get; set; }
    }
}
