using AttendanceApiAutomation.Model;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AttendanceApiAutomation.Utilities
{
    class Shared
    {
        public static string GetSettings(string value )
        {
            string directory = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).ToString();
            string directory1 = Directory.GetParent(directory).ToString();
            string directory2 = Directory.GetParent(directory1).ToString();
            string directory3 = Directory.GetParent(directory2).ToString();
            var builder = new ConfigurationBuilder()
               .SetBasePath(directory2)
               .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
               //.AddUserSecrets<Program>()
               .AddEnvironmentVariables();

            IConfigurationRoot configuration = builder.Build();
            var mySettingsConfig = new MySettingsConfig();
            configuration.GetSection("MySettings").Bind(mySettingsConfig);

            if (value == "blobAccountName") { return mySettingsConfig.blobAccountName; }
            else if (value == "blobAccountKey") { return mySettingsConfig.blobAccountKey; }
            else if (value == "basicUrl") { return mySettingsConfig.basicUrl; }
            else if (value == "EapimUrl") { return mySettingsConfig.EapimUrl; }
            else if (value == "getNcYearGroup") { return mySettingsConfig.getNcYearGroup; }
            else if (value == "getAttCodeFuncKey") { return mySettingsConfig.getAttCodeFuncKey; }
            else if (value == "getProviderFuncKey") { return mySettingsConfig.getProviderFuncKey; }
            else if (value == "postAttFuncKey") { return mySettingsConfig.postAttFuncKey; }
            else { return ""; }

        }
        public static string GetBlob(string containerName, string fileName)
        {
            var blobAccountName = GetSettings("blobAccountName");
            var blobAccountKey = GetSettings("blobAccountKey");
            if (blobAccountName == null) { blobAccountName = "s151d01saapid01"; }
            if (blobAccountKey == null) { blobAccountKey = "/MUPLFnZNBTdZQmEzcNyw1FTqCQVa3XXcQx9EsfE6nCT1u0O74euq/1/3UmXtGbqvWTTUZKPVATu9gOZ7qMQMg=="; }

            string connectionString = $"DefaultEndpointsProtocol=https;AccountName=" + blobAccountName + ";AccountKey=" + blobAccountKey + ";EndpointSuffix=core.windows.net";

            // Azure connection String from Env Variable
            //string connectionString = Environment.GetEnvironmentVariable("AZURE_STORAGE_CONNECTION_STRING");

            // Setup the connection to the storage account
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);

            // Connect to the blob storage
            CloudBlobClient serviceClient = storageAccount.CreateCloudBlobClient();
            // Connect to the blob container
            CloudBlobContainer container = serviceClient.GetContainerReference($"{containerName}");
            // Connect to the blob file
            CloudBlockBlob blob = container.GetBlockBlobReference($"{fileName}");
            // Get the blob file as text
            string contents = blob.DownloadTextAsync().Result;

            return contents;
        }

        public static string GetBlobFileName(string containerName, string fileName)
        {
            string returnName = "";
            var blobAccountName = GetSettings("blobAccountName");
            var blobAccountKey = GetSettings("blobAccountKey");
            if (blobAccountName == null) { blobAccountName = "s151d01saapid01"; }
            if (blobAccountKey == null) { blobAccountKey = "/MUPLFnZNBTdZQmEzcNyw1FTqCQVa3XXcQx9EsfE6nCT1u0O74euq/1/3UmXtGbqvWTTUZKPVATu9gOZ7qMQMg=="; }
            string connectionString = $"DefaultEndpointsProtocol=https;AccountName=" + blobAccountName + ";AccountKey=" + blobAccountKey + ";EndpointSuffix=core.windows.net";

            BlobContainerClient blobContainerClient = new BlobContainerClient(connectionString, "submissions");
            var blobs = blobContainerClient.GetBlobs();
            foreach (BlobItem blobItem in blobs)
            {
                if (blobItem.Name.StartsWith(fileName))
                {
                    returnName = blobItem.Name;
                }
               
            }
            
            return returnName;

        }

        public static Uri GetUrl(string method, string endPoint)
        {

            //Read environment variables

            //var baseURL = System.Environment.GetEnvironmentVariable("S151_FUNCTIONAPP_BASE_URL", EnvironmentVariableTarget.Machine);

            var baseURL = GetSettings("basicUrl");
            var EAPIMURL = GetSettings("EapimUrl");
            var getNcYGFuncKey = GetSettings("getNcYearGroup");
            var getAttCodeFuncKey = GetSettings("getAttCodeFuncKey");
            var getProviderFuncKey = GetSettings("getProviderFuncKey");
            var PostAttFuncKey = GetSettings("postAttFuncKey");
            

            // default to d01 if env variables are null
            if (baseURL == null) { baseURL = "https://s151d01-fa-exp-attapi.azurewebsites.net"; }
            if (getNcYGFuncKey == null) { getNcYGFuncKey = "lrb8zM5azaSdWBncevU3/EAZVzpbOkKQqCJ1isZVLhacgql9jaAcag=="; }
            if (getAttCodeFuncKey == null) { getAttCodeFuncKey = "FKJ7NckxndtmLJrC9BaM0MisaPBRSr46VL2reJcqVMXZPLj879JH6A=="; }
            if (getProviderFuncKey == null) { getProviderFuncKey = "cyGfq34jfGSFXxiurfZiBL3dDGHRPzMkGBZ9aaAU4vkIpOaGr41auQ=="; }
            if (PostAttFuncKey == null) { PostAttFuncKey = "4XbJZDbQsTqi7VJAjC4h9YXFKaB4JTK4O/rhyxWk8eQxBQjW79JgWQ=="; }
            if (EAPIMURL == null) { EAPIMURL = "https://dev-api-customerengagement.platform.education.gov.uk/data"; }

            if (endPoint == "function")
            {
                //return baseURL for different functions
                if (method == "/attendances") { return new Uri(baseURL + method + "?code=" + PostAttFuncKey); }
                if (method == "/codes/attendance") { return new Uri(baseURL + method + "?code=" + getAttCodeFuncKey); }
                if (method == "/codes/ncyeargroup") { return new Uri(baseURL + method + "?code=" + getNcYGFuncKey); }
                if (method == "/provider/{urn}") { return new Uri(baseURL + method + "?code=" + getProviderFuncKey); }
                else { return new Uri(baseURL + method); };
            }
            else if (endPoint == "eapim")
            {
                if (method == "/attendances") { return new Uri(EAPIMURL + method); }
                if (method == "/codes/attendance") { return new Uri(EAPIMURL + method); }
                if (method == "/codes/ncyeargroup") { return new Uri(EAPIMURL + method); }
                if (method == "/provider/{urn}") { return new Uri(EAPIMURL + method); }
                else { return new Uri(baseURL + method); };
            }
            else { return new Uri(baseURL + method); };
        }

        public static string getJwtToken(string client_id, string secret)
        {
            var client = new RestSharp.RestClient("https://login.microsoftonline.com/99517556-1f98-4955-a4c3-b97ce7e703f1/oauth2/v2.0/token");
            var request = new RestSharp.RestRequest(Method.POST);

            request.AddParameter("client_id", client_id, ParameterType.GetOrPost);
            request.AddParameter("grant_type", "client_credentials", ParameterType.GetOrPost);
            request.AddParameter("client_secret", secret, ParameterType.GetOrPost);
            request.AddParameter("scope", "https://dfeb2cdev.onmicrosoft.com/1d833581-1a8a-4565-b0a5-bcfa6472754f/.default", ParameterType.GetOrPost);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            var res = client.Execute(request);
            var token = Libraries.GetResponseObject(res, "access_token");
            return token;
        }
    }
}
