using System;
using System.Collections.Generic;
using System.Text;

namespace AttendanceApiAutomation.Model
{
    class SubmissionResponse
    {
        public string submissionRef { get; set; }

        public List<Errors> errors { get; set; }

        public SubmissionResponse()
        {
            errors = new List<Errors>();
        }
    }
}
