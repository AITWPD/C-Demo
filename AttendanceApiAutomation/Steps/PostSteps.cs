using System;
using TechTalk.SpecFlow;
using AttendanceApiAutomation.Base;
using AttendanceApiAutomation.Model;
using AttendanceApiAutomation.Utilities;
using RestSharp;
using Newtonsoft.Json;
using RestSharp.Serialization.Json;
using NUnit.Framework;
using System.Collections.Generic;
using TechTalk.SpecFlow.Time;

namespace AttendanceApiAutomation.Steps
{
    [Binding]
    public class PostSteps
    {
        string fileString = "";
        string clientID = "b04c2dc3-589b-4a5b-afb6-601e68a8eded";
        DataStorage blobRead = new DataStorage();
        DataStorage calcFile = new DataStorage();
        SubmissionDataPayLoad inputFile = new SubmissionDataPayLoad();

        private ScenarioContext _scenarioContext;
        private Settings _settings;
        public PostSteps(ScenarioContext scenarioContext, Settings settings)
        {
            _scenarioContext = scenarioContext;
            _settings = settings;
        }

        [Given(@"a supplier has POST to the attendances API\.")]
        public void GivenASupplierHasPOSTToTheAttendancesAPI_()
        {
            _settings.BaseUrl = Shared.GetUrl("/attendances", "function");
            Console.WriteLine("URL being used for this POST request is : " + _settings.BaseUrl);
            _settings.RestClient.BaseUrl = _settings.BaseUrl;
            _settings.RestClient.Timeout = 10000; // 10 second timeout

            _settings.Request = new RestRequest(Method.POST);
        }
        
        [Given(@"the application type is Json")]
        public void GivenTheApplicationTypeIsJson()
        {
            _settings.Request.RequestFormat = DataFormat.Json;
        }
        
        [Given(@"the payload is JSON file ""(.*)""")]
        public void GivenThePayloadIsJSONFile(string filename)
        {
            var bodyString = Utilities.Libraries.NoURNValueJson(filename);
            inputFile = JsonConvert.DeserializeObject<SubmissionDataPayLoad>(bodyString);
            calcFile = JsonConvert.DeserializeObject<DataStorage>(bodyString);

            

            for (int i = 0; i < inputFile.submissionData.Count; i++)
            {
                inputFile.submissionData[i].date = DateTime.UtcNow;
                calcFile.submissionData[i].date = inputFile.submissionData[i].date;

            }
            calcFile.clientId = clientID;
            _settings.Request.AddJsonBody(inputFile);
        }

        [Given(@"the EAPIM payload is JSON file ""(.*)""")]
        public void GivenTheEAPIMPayloadIsJSONFile(string filename)
        {
            var bodyString = Utilities.Libraries.NoURNValueJson(filename);
            inputFile = JsonConvert.DeserializeObject<SubmissionDataPayLoad>(bodyString);
            calcFile = JsonConvert.DeserializeObject<DataStorage>(bodyString);

            for (int i = 0; i < inputFile.submissionData.Count; i++)
            {
                inputFile.submissionData[i].date = DateTime.UtcNow;
                calcFile.submissionData[i].date = inputFile.submissionData[i].date;
            }
            calcFile.clientId = clientID;
        }

        [When(@"the Post request is processed")]
        public void WhenThePostRequestIsProcessed()
        {
             
            Guid g = Guid.NewGuid();
            _settings.Request.AddHeader("api-client-app-id", clientID);
            _settings.Request.AddHeader("api-client-session-id", g.ToString());
            try
            {
                _settings.Response = _settings.RestClient.ExecuteTaskAsync(_settings.Request).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occurredd while doing POST request.  Initial Response:      " + _settings.Response);
                _settings.Response.ResponseStatus = ResponseStatus.Error;
                _settings.Response.ErrorMessage = ex.Message;
                _settings.Response.ErrorException = ex;
                Console.WriteLine("Error occurred while doing POST request.  Exception:      " + ex);
                var twilioException = new ApplicationException("Error occurred while doing POST request.", ex);
                throw twilioException;
            }
        }

        [When(@"the Post request is processed with clientID ""(.*)""")]
        public void WhenThePostRequestIsProcessedWithClientID(string inputClientID)
        {
            Guid g = Guid.NewGuid();
            _settings.Request.AddHeader("api-client-app-id", inputClientID);
            _settings.Request.AddHeader("api-client-session-id", g.ToString());
            calcFile.clientId = inputClientID;
            try
            {
                _settings.Response = _settings.RestClient.ExecuteTaskAsync(_settings.Request).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occurredd while doing POST request.  Initial Response:      " + _settings.Response);
                _settings.Response.ResponseStatus = ResponseStatus.Error;
                _settings.Response.ErrorMessage = ex.Message;
                _settings.Response.ErrorException = ex;
                Console.WriteLine("Error occurred while doing POST request.  Exception:      " + ex);
                var twilioException = new ApplicationException("Error occurred while doing POST request.", ex);
                throw twilioException;
            }
        }


        [Then(@"I write process the response json")]
        public void ThenIWriteProcessTheResponseJson()
        {
            JsonDeserializer deserial = new JsonDeserializer();
            var messageList = deserial.Deserialize<List<SubmissionResponse>>(_settings.Response);
            if (messageList[0].submissionRef != null) { calcFile.submissionRef = messageList[0].submissionRef; }

            for (int i = 0; i < messageList[0].errors.Count; i++)
            {
                string temp = messageList[0].errors[i].path;
                int firstIndex = temp.IndexOf("[");
                int secondIndex = temp.IndexOf("]");

                var m = temp.Substring((firstIndex + 1), (secondIndex - firstIndex - 1));
                int k = Int32.Parse(m);

                var id = messageList[0].errors[i].id;
                var code = messageList[0].errors[i].code;
                var status = messageList[0].errors[i].status;
                var title = messageList[0].errors[i].title;
                var detail = messageList[0].errors[i].detail;
                var path = messageList[0].errors[i].path;


                calcFile.submissionData[k].dqCheckResults.Add(new DqCheckResults() { id = id, code = code, status = status, title = title,  detail = detail, path = path });

            }
        }

        [Then(@"I read record from blob storage")]
        public void ThenIReadRecordFromBlobStorage()
        {
            var folderName = "submissions";
            var fileName = (DateTime.UtcNow.ToString("yyyy-MM-dd") + "/" + calcFile.clientId + "-" + calcFile.urn + "-" + calcFile.submissionRef + "-" + DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm"));
            fileName =  Shared.GetBlobFileName(folderName, fileName);
            fileString = Shared.GetBlob(folderName, fileName);
        }

        [Then(@"I write string to json")]
        public void ThenIWriteStringToJson()
        {
            blobRead = JsonConvert.DeserializeObject<DataStorage>(fileString);
        }

        [Then(@"I confirm that the blob storage is correct")]
        public void ThenIConfirmThatTheBlobStorageIsCorrect()
        {
            var calcFil = JsonConvert.SerializeObject(calcFile, Newtonsoft.Json.Formatting.None,
                             new JsonSerializerSettings
                             {
                                 NullValueHandling = NullValueHandling.Ignore
                             });
            DataStorage blobRead1 = new DataStorage();
            blobRead1 = JsonConvert.DeserializeObject<DataStorage>(calcFil);

            fileString = fileString.Replace("\r\n", string.Empty);
            calcFil = calcFil.Trim();
            fileString = fileString.Replace(" ", string.Empty);
            calcFil = calcFil.Replace(" ", string.Empty);

            Assert.That(calcFil, Is.EqualTo(fileString), $"The calculated json {calcFil} is not matching the reaad json {fileString}");

        }

        [Given(@"I want to reaad a test blob storage")]
        public void GivenIWantToReaadATestBlobStorage()
        {
            var folderName = "submissions";
            var fileName = (calcFile.clientId + "-" + calcFile.urn + "-" + calcFile.submissionRef + "-" + DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm"));
            fileName = Shared.GetBlobFileName(folderName, fileName);
            fileString = Shared.GetBlob(folderName, fileName);
        }

        [Then(@"I wait ""(.*)"" seconds")]
        public void ThenIWaitSeconds(int time)
        {
            System.Threading.Thread.Sleep(time*1000);
        }

        [Given(@"I set date to ""(.*)""")]
        public void GivenISetDateTo(DateTime inputDate)
        {
            calcFile.submissionData[0].date = inputDate;
            inputFile.submissionData[0].date = inputDate;
        }

        [Given(@"I set urn to ""(.*)""")]
        public void GivenISetUrnTo(int urn)
        {
            calcFile.urn = urn;
            inputFile.urn = urn;
        }


        [Given(@"I set date \[(.*)] to ""(.*)""")]
        public void GivenISetDateTo(int i, DateTime inputDate)
        {
            inputDate = DateTime.SpecifyKind(inputDate, DateTimeKind.Utc);
            calcFile.submissionData[i].date = inputDate;
            inputFile.submissionData[i].date = inputDate;
        }

        [Given(@"I set NcYearGroup \[(.*)] to ""(.*)""")]
        public void GivenISetNcYearGroupTo(int i, string yearGroup)
        {
            calcFile.submissionData[i].ncYearGroup = yearGroup;
            inputFile.submissionData[i].ncYearGroup = yearGroup;
        }

        [Given(@"I set attendance value \[(.*)] for SubmissionData \[(.*)] to value \[(.*)]")]
        public void GivenISetAttendanceValueForSubmissionDataToValue(int i, int a, int value)
        {
            calcFile.submissionData[i].attendances[a].value = value;
            inputFile.submissionData[i].attendances[a].value = value;
        }



        [When(@"I submit POST with fileupdate")]
        public void WhenISubmitPOSTWithFileupdate()
        {
            Guid g = Guid.NewGuid();
            _settings.Request.AddHeader("api-client-app-id", clientID);
            _settings.Request.AddHeader("api-client-session-id", g.ToString());
            _settings.Request.AddJsonBody(inputFile);
            _settings.Response = _settings.RestClient.ExecuteTaskAsync(_settings.Request).GetAwaiter().GetResult();
           
        }


    }
}
