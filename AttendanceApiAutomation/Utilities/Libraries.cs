using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Deserializers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Reflection;
using RestSharp.Serialization.Json;

namespace AttendanceApiAutomation.Utilities
{
    public static class Libraries
    {

        public static Dictionary<string, string> DeserializeResponse(this IRestResponse restResponse)
        {
            var JSONObj = new JsonDeserializer().Deserialize<Dictionary<string, string>>(restResponse);

            return JSONObj;
        }

        public static string GetResponseObject(this IRestResponse response, string responseObject)
        {
            JObject obs = JObject.Parse(response.Content);
            return obs[responseObject].ToString();
        }


        public static int GetResponseObjectCount(this IRestResponse response, int responseObjectCount)
        {
            JObject obs = JObject.Parse(response.Content);
            return obs.Count;
        }


        public static async Task<IRestResponse<T>> ExecuteAsyncRequest<T>(this RestClient client, IRestRequest request) where T : class, new()
        {
            var taskCompletionSource = new TaskCompletionSource<IRestResponse<T>>();

            client.ExecuteAsync<T>(request, restResponse =>
            {
                if (restResponse.ErrorException != null)
                {
                    const string message = "Error retrieving response.";
                    throw new ApplicationException(message, restResponse.ErrorException);
                }

                taskCompletionSource.SetResult(restResponse);
            });

            return await taskCompletionSource.Task;
        }

        public static string NoURNValueJson(string fileName)
        {
            string json;
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "TestData");
            string filePath = Path.Combine(path, $"{fileName}.json");
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException(filePath);
            }
            //  StreamReader reader = new StreamReader("C:\\Code\\Absence-API\\attendance-api-automation\\RestSharpDemo\\TestData\\" + fileName + ".json");
            //StreamReader reader = new StreamReader("D:\\a\\1\\s\\attendance-api-automation\\RestSharpDemo\\TestData\\" + fileName + ".json");
            StreamReader reader = new StreamReader(filePath);
            try
            {
                do
                {
                    json = reader.ReadToEnd();
                }
                while (reader.Peek() != -1);
            }
            catch
            {
                return ("File is empty");
            }
            finally
            {
                reader.Close();
            }
            return (json);
        }
    }
}
