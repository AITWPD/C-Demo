using System;
using System.Collections.Generic;
using System.Text;

namespace AttendanceApiAutomation.Model
{
    
        public class Attendances1
        {
            public string attendanceCode = "B";
            public int value = 20;
        }

        public class SubmissionData
        {
            public string ncYearGroup = "E1";
            public DateTime date = DateTime.Now;
            public String schoolSession = "AM";
            public Attendances1[] attendances;

        }
    public class Temp1
    {
        public int urn = 12345;

        public SubmissionData[] submissionData;

    }


}
