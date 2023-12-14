using System;
using System.Collections.Generic;
using System.Text;

namespace AttendanceApiAutomation.Model
{
    class Provider
    {
        public int urn { set; get; }
        public string name { set; get; }
        public string address { set; get; }
        public string phaseOfEducation { set; get; }
        public string schoolType { set; get; }
    }
}
