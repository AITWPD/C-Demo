using AttendanceApiAutomation.Base;
using AttendanceApiAutomation.Model;
using AttendanceApiAutomation.Utilities;
using RestSharp;
using TechTalk.SpecFlow;
using NUnit.Framework;
using System.Collections.Generic;
using System;

namespace AttendanceApiAutomation.Steps
{
    [Binding]
    public class _52572EapimAuthenticationSteps
    {
        private readonly Settings _settings;

        public _52572EapimAuthenticationSteps(Settings settings) => _settings = settings;


        

        [Given(@"I am making a GET attendance code request with a valid JWT token to EAPIM")]
        public void GivenIAmMakingAGETAttendanceCodeRequestWithAValidJWTTokenToEAPIM()
        {
            var token = Shared.getJwtToken("a4f1cad3-f515-4316-8c78-d4af9d07f118", "A5g8~et7_JbPBaZsADx6kn14~mEe.Iu1Nz");
            _settings.BaseUrl = _settings.BaseUrl = Shared.GetUrl("/codes/attendance", "eapim");
            Console.WriteLine("URL being used for this GET request is : " + _settings.BaseUrl);
            _settings.RestClient.BaseUrl = _settings.BaseUrl;
            _settings.RestClient.Timeout = 10000; // 10 second timeout
            _settings.Request = new RestRequest(Method.GET);
            _settings.Request.AddHeader("Authorization", token);
            
        }
        
        [Given(@"I have a valid subscription key")]
        public void GivenIHaveAValidSubscriptionKey()
        {
            _settings.Request.AddHeader("Ocp-Apim-Subscription-Key", "ace1c1ad2e7c4409a0313e4723f7b273");
        }

        [Given(@"I am making a GET attendance code request with an invalid JWT token to EAPIM")]
        public void GivenIAmMakingAGETAttendanceCodeRequestWithAnInvalidJWTTokenToEAPIM()
        {
            var token = "yJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsIng1dCI6ImtnMkxZczJUMENUaklmajRydDZKSXluZW4zOCIsImtpZCI6ImtnMkxZczJUMENUaklmajRydDZKSXluZW4zOCJ9.eyJhdWQiOiJodHRwczovL2RmZWIyY2Rldi5vbm1pY3Jvc29mdC5jb20vMWQ4MzM1ODEtMWE4YS00NTY1LWIwYTUtYmNmYTY0NzI3NTRmIiwiaXNzIjoiaHR0cHM6Ly9zdHMud2luZG93cy5uZXQvOTk1MTc1NTYtMWY5OC00OTU1LWE0YzMtYjk3Y2U3ZTcwM2YxLyIsImlhdCI6MTYwMjk2MDE3OCwibmJmIjoxNjAyOTYwMTc4LCJleHAiOjE2MDI5NjQwNzgsImFpbyI6IkUyUmdZSGhuelJod1o4WjU4YjhkUWU5V3lRV2ZBUUE9IiwiYXBwaWQiOiJiMDRjMmRjMy01ODliLTRhNWItYWZiNi02MDFlNjhhOGVkZWQiLCJhcHBpZGFjciI6IjEiLCJpZHAiOiJodHRwczovL3N0cy53aW5kb3dzLm5ldC85OTUxNzU1Ni0xZjk4LTQ5NTUtYTRjMy1iOTdjZTdlNzAzZjEvIiwib2lkIjoiZDg4NzAwZjItNWUzZi00NjRjLTgxNmEtOTVmZGYxYjdlMGVjIiwicmgiOiIwLkFBQUFWblZSbVpnZlZVbWt3N2w4NS1jRDhjTXRUTENiV0Z0S3I3WmdIbWlvN2Uwa0FBQS4iLCJyb2xlcyI6WyJhcGkuYXR0ZW5kYW5jZS5jbGllbnQud3JpdGUiLCJhcGkuYXR0ZW5kYW5jZS5jbGllbnQucmVhZCJdLCJzdWIiOiJkODg3MDBmMi01ZTNmLTQ2NGMtODE2YS05NWZkZjFiN2UwZWMiLCJ0aWQiOiI5OTUxNzU1Ni0xZjk4LTQ5NTUtYTRjMy1iOTdjZTdlNzAzZjEiLCJ1dGkiOiJFRlFlYmNud2FVYVJaWDZtLWVWOEFBIiwidmVyIjoiMS4wIn0.wripLgiIDApu6MpWvDUy9ULmBcru6-b_NNUZS_nFWempQht3AUkrd4-cBQ27PkzQwuW6MAXaZ6wE5fIizDRVad90GQwi5GFJ03K2LPsEQQFHEnykURFf-47JLlji3Q04y7qTpPJ8Da2Ujwr0ztaIQ16j4U1sj4loF19Md2WQDaK-Jtlb3SsqV2kuXl-qTCVHcJVgxN2aRljP_kWuJHeZUgKUu2WSl9UGvomg9fzFp6gOFWMbL-T9h6gnKOVkSvl6lp7gpjyum33dZZchz3R7EU6xf_CQfVpEDT1VybHsngxqkEmUa-ZPxVf7hkxRXS0AwRLjBDn7XsOUrzj2eli4_w2";
            _settings.Request = new RestRequest("https://dev-api-customerengagement.platform.education.gov.uk/data/codes/attendance", Method.GET);
            _settings.Request.AddHeader("Authorization", token);
        }


        [When(@"the ""(.*)"" request is submitted")]
        public void WhenTheRequestIsSubmitted(string responseModel)
        {
            if (responseModel == "Attendance")
            {
                _settings.Response = _settings.RestClient.Execute<List<AttendanceResp>>(_settings.Request);
            }
            else if (responseModel == "Attendance")
            {
                _settings.Response = _settings.RestClient.Execute<List<NcYearGroups>>(_settings.Request);
            }
        }

        [Then(@"a HTTP ""(.*)"" response is returned")]
        public void ThenAHTTPResponseIsReturned(int expCode)
        {
            int numericStatusCode = (int)_settings.Response.StatusCode;
            Assert.That(numericStatusCode, Is.EqualTo(expCode), $"The response code returned {numericStatusCode} wasn't {expCode} as expected");

        }
        [Given(@"I am making a POST attendance code request with a valid JWT token to EAPIM")]
        public void GivenIAmMakingAPOSTAttendanceCodeRequestWithAValidJWTTokenToEAPIM()
        {
            var token = Shared.getJwtToken("a4f1cad3-f515-4316-8c78-d4af9d07f118", "A5g8~et7_JbPBaZsADx6kn14~mEe.Iu1Nz");
            _settings.BaseUrl = Shared.GetUrl("/attendances", "eapim");
            Console.WriteLine("URL being used for this POST request is : " + _settings.BaseUrl);
            _settings.RestClient.BaseUrl = _settings.BaseUrl;
            _settings.RestClient.Timeout = 10000; // 10 second timeout

            _settings.Request = new RestRequest(Method.POST);
            _settings.Request.AddHeader("Authorization", token);
        }

        

    }
}
