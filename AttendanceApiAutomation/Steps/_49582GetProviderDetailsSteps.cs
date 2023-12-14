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
using Newtonsoft.Json;

namespace AttendanceApiAutomation.Steps
{
    [Binding]
    public class _49582GetProviderDetailsSteps
    {
        private ScenarioContext _scenarioContext;
        private Settings _settings;
        public _49582GetProviderDetailsSteps(ScenarioContext scenarioContext, Settings settings)
        {
            _scenarioContext = scenarioContext;
            _settings = settings;
        }

        [Given(@"the MIS supplier has made a valid GET/provider/\{URN} request using a dummy URN to the attendances API\.")]
        public void GivenTheMISSupplierHasMadeAValidGETProviderURNRequestUsingADummyURNToTheAttendancesAPI_()
        {
            _settings.BaseUrl = Shared.GetUrl("/provider/{urn}", "function");
            Console.WriteLine("URL being used for this GET request is : " + _settings.BaseUrl);
            _settings.RestClient.BaseUrl = _settings.BaseUrl;
            _settings.RestClient.Timeout = 10000; // 10 second timeout
            _settings.Request = new RestRequest(Method.GET);
        }

        [Given(@"I am making a GET Provider request with a valid JWT token to EAPIM")]
        public void GivenIAmMakingAGETProviderRequestWithAValidJWTTokenToEAPIM()
        {
            var token = Shared.getJwtToken("a4f1cad3-f515-4316-8c78-d4af9d07f118", "A5g8~et7_JbPBaZsADx6kn14~mEe.Iu1Nz");
            _settings.BaseUrl = Shared.GetUrl("/provider/{urn}", "eapim");
            Console.WriteLine("URL being used for this GET request is : " + _settings.BaseUrl);
            _settings.RestClient.BaseUrl = _settings.BaseUrl;
            _settings.RestClient.Timeout = 10000; // 10 second timeout
            _settings.Request = new RestRequest(Method.GET);
            _settings.Request.AddHeader("Authorization", token);
        }

        

        [Given(@"the urn is set to ""(.*)""")]
        public void GivenTheUrnIsSetTo(string postId)
        {
            Thread.Sleep(2000);
            _settings.Request.AddUrlSegment("urn", postId.ToString());
        }

        [When(@"the request is processed")]
        public void WhenTheRequestIsProcessed()
        {
            _settings.Response = _settings.RestClient.ExecuteAsyncRequest<Provider>(_settings.Request).GetAwaiter().GetResult();
        }
    }
}
