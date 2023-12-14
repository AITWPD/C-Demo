using System;
using TechTalk.SpecFlow;
using AttendanceApiAutomation.Base;
using AttendanceApiAutomation.Model;
using AttendanceApiAutomation.Utilities;
using RestSharp;
using Newtonsoft.Json;

namespace AttendanceApiAutomation.Steps
{
    [Binding]
    public class _46900ValidateUrnInPostAttendancesSteps
    {
        private ScenarioContext _scenarioContext;
        private Settings _settings;
        public _46900ValidateUrnInPostAttendancesSteps(ScenarioContext scenarioContext, Settings settings)
        {
            _scenarioContext = scenarioContext;
            _settings = settings;
        }

        [Given(@"the payload is JSON")]
        public void GivenThePayloadIsJSON()
        {
            var bodyString = Utilities.Libraries.NoURNValueJson("ValidSubmission");
            _settings.Request.AddParameter("application/json", bodyString, ParameterType.RequestBody);
        }

        [Given(@"the ""(.*)"" is set to ""(.*)""")]
        public void GivenTheIsSetTo(string p0, int urnvalue)
        {
            var body1 = new Temp1
            {
                urn = urnvalue,
                submissionData = new SubmissionData[]
            {
                    new SubmissionData {
                        ncYearGroup = "E2",
                        date = DateTime.Now,
                        schoolSession = "AM",
                        attendances = new Attendances1[]
                        {
                            new Attendances1
                            {

                               attendanceCode = "B",
                                value = 10
                            },new Attendances1
                            {

                               attendanceCode = "C",
                                value = 20
                            }
                        }
                    }
            }
            };

            //  body.submissionData1[0].attendances1[0].attendanceCode = "D";

            _settings.Request.AddJsonBody(body1);
        }

        [Given(@"the payload is JSON is set with no URN")]
        public void GivenThePayloadIsJSONIsSetWithNoURN()
        {
            var bodyString = Utilities.Libraries.NoURNValueJson("NoUrnJson");
            _settings.Request.AddParameter("application/json", bodyString, ParameterType.RequestBody);
        }
    }
}
