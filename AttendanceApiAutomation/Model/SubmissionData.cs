using System;
using System.Collections.Generic;
using System.Text;

namespace AttendanceApiAutomation.Model
{
    class SubmissionDataPayLoad
    {
        public int urn { get; set; }
        public int totalNoOfPupils { get; set; }
        public List<SubmissionData> submissionData { get; set; }
    }
}
