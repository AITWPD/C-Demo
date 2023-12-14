using System;
using System.Collections.Generic;
using System.Text;

namespace AttendanceApiAutomation.Model
{
    class DataStorage
    {
        public string clientId { get; set; }
        public string submissionRef { get; set; }
        public int urn { get; set; }
        public int totalNoOfPupils { get; set; }

        public List<SubmissionDataIncDQ> submissionData { get; set; }

        public void ClassNamePlaceholder()
        {
            submissionData = new List<SubmissionDataIncDQ>();
        }
    }
}
