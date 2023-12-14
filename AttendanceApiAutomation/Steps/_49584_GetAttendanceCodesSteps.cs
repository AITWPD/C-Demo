using System;
using TechTalk.SpecFlow;
using NUnit.Framework;
using RestSharp;
using RestSharp.Deserializers;
using AttendanceApiAutomation.Base;
using AttendanceApiAutomation.Model;
using AttendanceApiAutomation.Utilities;
using System.Threading;
using System.Collections.Generic;
using TechTalk.SpecFlow.Assist;
using RestSharp.Serialization.Json;
using static AttendanceApiAutomation.Model.AttendanceResp;

namespace AttendanceApiAutomation.Steps
{
    
    [Binding]
    public class _49584_GetAttendanceCodesSteps
    {
        private ScenarioContext _scenarioContext;
        private Settings _settings;
        public _49584_GetAttendanceCodesSteps(ScenarioContext scenarioContext, Settings settings)
        {
            _scenarioContext = scenarioContext;
            _settings = settings;
        }

        [Given(@"that the API request is valid")]
        public void GivenThatTheAPIRequestIsValid()
        {
            //add the verification in later for this
            //ScenarioContext.Current.Pending();
        }

        [Given(@"a supplier has submitted a GET/codes/attendance request to the attendances API\.")]
        public void GivenASupplierHasSubmittedAGETCodesAttendanceRequestToTheAttendancesAPI_()
        {
            _settings.BaseUrl = _settings.BaseUrl = Shared.GetUrl("/codes/attendance", "function");
            Console.WriteLine("URL being used for this GET request is : " + _settings.BaseUrl);
            _settings.RestClient.BaseUrl = _settings.BaseUrl;
            _settings.RestClient.Timeout = 10000; // 10 second timeout
            _settings.Request = new RestRequest(Method.GET);
        }
        
        [Then(@"I should see following absence codes")]
        public void ThenIShouldSeeFollowingAbsenceCodes(Table table)
        {
            JsonDeserializer deserial = new JsonDeserializer();
            var messageList = deserial.Deserialize<List<AttendanceResponse>>(_settings.Response);

            table.CompareToSet<AttendanceResponse>(messageList);
        }
    }
}
