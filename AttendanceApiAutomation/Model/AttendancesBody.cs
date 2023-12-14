using System;
using System.Collections.Generic;
using System.Text;


namespace AttendanceApiAutomation.Model
{
    class SubmissionDataIncDQ
    {
        //public SubmissionDataIncDQ()
        //{
        //    this.dqCheckResults = new DqCheckResults[1];
        //}
        public string ncYearGroup { get; set; }
        public DateTime date { get; set; }
        public String schoolSession { get; set; }
        public Attendances1[] attendances { get; set; }

        public List<DqCheckResults> dqCheckResults { get; set; }

        public SubmissionDataIncDQ()
        {
            dqCheckResults = new List<DqCheckResults>();
        }
        public bool ShouldSerializedqCheckResults()
        {
            return dqCheckResults.Count > 0;
        }

        public static implicit operator SubmissionDataIncDQ(SubmissionData v)
        {
            throw new NotImplementedException();
        }
    }

    class AttendancesBody
    {
        public int URN { get; set; }

        public List<SubmissionData> SubmissionData { get; set; }

        public AttendancesBody()
        {
            SubmissionData = new List<SubmissionData>();
        }
    }
}
