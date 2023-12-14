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
    public class _49578_POSTAttendanceDataSteps
    {
        private ScenarioContext _scenarioContext;
        private Settings _settings;
        public _49578_POSTAttendanceDataSteps(ScenarioContext scenarioContext, Settings settings)
        {
            _scenarioContext = scenarioContext;
            _settings = settings;
        }

        [Given(@"the payload is not JSON")]
        public void GivenThePayloadIsNotJSON()
        {
            var bodyString = Utilities.Libraries.NoURNValueJson("XMLBody");
            _settings.Request.AddParameter(bodyString, ParameterType.RequestBody);
            _settings.Request.AddHeader("Content-Type", "application/json");
            _settings.Request.RequestFormat = DataFormat.Xml;
        }
        
        [Given(@"the ""(.*)"" of the heaader is ""(.*)""")]
        public void GivenTheOfTheHeaaderIs(string headKey, string headValue)
        {
            _settings.Request.AddHeader(headKey, headValue);
        }

        [When(@"the content header type of the request is  not application / JSON")]
        public void WhenTheContentHeaderTypeOfTheRequestIsNotApplicationJSON()
        {
            _settings.Request.AddHeader("Content-Type", "application/xml");
        }
        
        [Then(@"an error description is returned in the body")]
        public void ThenAnErrorDescriptionIsReturnedInTheBody()
        {
            var jSonResponse = JsonConvert.DeserializeObject(_settings.Response.Content);
            Assert.That(jSonResponse, Is.EqualTo("Unexpected character encountered while parsing value: %. Path '', line 0, position 0."), $"The description returned is not correct");
        }

        [Then(@"an error description is returned in the body was ""(.*)""")]
        public void ThenAnErrorDescriptionIsReturnedInTheBodyWas(string expectedError)
        {
            var jSonResponse = JsonConvert.DeserializeObject(_settings.Response.Content);
            Assert.That(jSonResponse, Is.EqualTo(expectedError), $"The description returned is not correct");
        }

        [Then(@"an error description is returned in the body ""(.*)""")]
        public void ThenAnErrorDescriptionIsReturnedInTheBody(string error)
        {
            var jSonResponse = JsonConvert.DeserializeObject(_settings.Response.Content);
            Assert.That(jSonResponse, Is.EqualTo(error), $"The description returned is not correct");
        }
    }
}
