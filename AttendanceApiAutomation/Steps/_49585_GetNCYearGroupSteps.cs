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
    public class _49585_GetNCYearGroupSteps
    {
        private ScenarioContext _scenarioContext;
        private Settings _settings;
        public _49585_GetNCYearGroupSteps(ScenarioContext scenarioContext, Settings settings)
        {
            _scenarioContext = scenarioContext;
            _settings = settings;
        }


        [Given(@"the supplier has submitted a GET/codes/ncyeargroup request to the attendances API\.")]
        public void GivenTheSupplierHasSubmittedAGETCodesNcyeargroupRequestToTheAttendancesAPI_()
        {
            _settings.BaseUrl = Shared.GetUrl("/codes/ncyeargroup", "function");
            Console.WriteLine("URL being used for this GET request is : " + _settings.BaseUrl);
            _settings.RestClient.BaseUrl = _settings.BaseUrl;
            _settings.RestClient.Timeout = 10000; // 10 second timeout
            _settings.Request = new RestRequest(Method.GET);
        }

        [Given(@"I am making a GET ncYear Group Code request with a valid JWT token to EAPIM")]
        public void GivenIAmMakingAGETNcYearGroupCodeRequestWithAValidJWTTokenToEAPIM()
        {
            var token = Shared.getJwtToken("a4f1cad3-f515-4316-8c78-d4af9d07f118", "A5g8~et7_JbPBaZsADx6kn14~mEe.Iu1Nz");
            _settings.BaseUrl = Shared.GetUrl("/codes/ncyeargroup", "eapim");
            Console.WriteLine("URL being used for this GET request is : " + _settings.BaseUrl);
            _settings.RestClient.BaseUrl = _settings.BaseUrl;
            _settings.RestClient.Timeout = 10000; // 10 second timeout
            _settings.Request = new RestRequest(Method.GET);
            _settings.Request.AddHeader("Authorization", token);
        }


        [Then(@"I should see the following year group values")]
        public void ThenIShouldSeeTheFollowingYearGroupValues(Table table)
        {
            JsonDeserializer deserial = new JsonDeserializer();
            var messageList = deserial.Deserialize<List<NcYearGroups>>(_settings.Response);

            table.CompareToSet<NcYearGroups>(messageList);
        }
    }
}
