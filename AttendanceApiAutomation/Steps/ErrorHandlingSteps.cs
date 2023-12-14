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

namespace AttendanceApiAutomation.Steps
{

    [Binding]
    public class ErrorHandlingSteps
    {

        private ScenarioContext _scenarioContext;
        private Settings _settings;
        public ErrorHandlingSteps(ScenarioContext scenarioContext, Settings settings)
        {
            _scenarioContext = scenarioContext;
            _settings = settings;
        }


        [Then(@"body returned contains submissionref and no errors")]
        public void ThenBodyReturnedContainsSubmissionrefAndNoErrors()
        {
            JsonDeserializer deserial = new JsonDeserializer();
            var messageList = deserial.Deserialize<List<SubmissionResponse>>(_settings.Response);


            Assert.That(messageList[0].submissionRef, Is.Not.Null, "SubmissionRef is not empty");
            Assert.That(messageList[0].errors.Count, Is.EqualTo(0), "Error Array is not empty");
        }

        [Then(@"the request-response is logged")]
        public void ThenTheRequest_ResponseIsLogged()
        {
            //_scenarioContext.Pending();
        }

        [Then(@"Error ""(.*)"" ""(.*)"" returned equals ""(.*)""")]
        public void ThenErrorReturnedEquals(int errorArray, string key, string value)
        {
            JsonDeserializer deserial = new JsonDeserializer();
            var messageList = deserial.Deserialize<List<SubmissionResponse>>(_settings.Response);

            var errorResp = messageList[0].errors[errorArray];
            var temp = "";
            if (key == "id") { temp = errorResp.id; }
            else if (key == "code") { temp = errorResp.code; }
            else if (key == "status") { temp = errorResp.status; }
            else if (key == "title") { temp = errorResp.title; }
            else if (key == "detail") { temp = errorResp.detail; }
            else if (key == "path") { temp = errorResp.path; }

            // var temp = errorRespkey.ToString();
            Assert.That(temp, Is.EqualTo(value), $"The {key} is not matching");
        }


        //400 resonse errors
        [Then(@"""(.*)"" returned equals ""(.*)""")]
        public void ThenReturnedEquals(string key, string value)
        {
            var temp = _settings.Response.GetResponseObject(key);
            Assert.That(temp, Is.EqualTo(value), $"The {key} is not matching");
        }

        //validate submission Response
        [Then(@"Submission Reference is returned")]
        public void ThenSubmissionReferenceIsReturned()
        {
            JsonDeserializer deserial = new JsonDeserializer();
            var messageList = deserial.Deserialize<List<SubmissionResponse>>(_settings.Response);
            Assert.That(messageList[0].submissionRef, Is.Not.Null, "SubmissionRef is not empty");
        }
    }
}
