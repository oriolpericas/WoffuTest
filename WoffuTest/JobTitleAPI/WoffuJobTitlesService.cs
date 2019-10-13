using JobTitleAPI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace JobTitleAPI {
    public class WoffuJobTitlesService {

        /// <summary>
        /// URL to the Woffu JobTitles Service
        /// </summary>
        public string URL { get; set; }

        /// <summary>
        /// Authorization username
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Get all job titles
        /// </summary>
        /// <returns>List of job titles</returns>
        public JobTitle[] GetJobTitles() {
            string result = SendRequest("GET", "/jobtitles");
            return JsonConvert.DeserializeObject<JobTitle[]>(result);
        }

        /// <summary>
        /// Get one job title
        /// </summary>
        /// <param name="id">Job title ID</param>
        /// <returns></returns>
        public JobTitle GetJobTitle(int id) {
            string result = SendRequest("GET", $"/jobtitles/{id}");
            return JsonConvert.DeserializeObject<JobTitle>(result);
        }

        /// <summary>
        /// Create a new job title
        /// </summary>
        /// <param name="jobTitle">New job title</param>
        public JobTitle CreateJobTitle(JobTitle jobTitle) {
            string result = SendRequest("POST", $"/jobtitles", JsonConvert.SerializeObject(jobTitle));
            return JsonConvert.DeserializeObject<JobTitle>(result);
        }

        /// <summary>
        /// Edit a job title
        /// </summary>
        /// <param name="jobTitle">Edited job title</param>
        public void EditJobTitle(JobTitle jobTitle) {
            SendRequest("PUT", $"/jobtitles/{jobTitle.JobTitleId}", JsonConvert.SerializeObject(jobTitle));
        }

        /// <summary>
        /// Delete a job title
        /// </summary>
        /// <param name="id">Job title ID</param>
        public void DeleteJobTitle(int id) {
            SendRequest("DELETE", $"/jobtitles/{id}");
        }

        private string SendRequest(string method, string endpoint, string body = "") {
            // Create the web request
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(URL + endpoint);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = method;
            // Add basic authentication
            httpWebRequest.Credentials = new NetworkCredential(Username, "");

            // Write request body if not empty
            if (!string.IsNullOrEmpty(body)) {
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream())) {
                    streamWriter.Write(body);
                }
            }

            // Read and return response
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream())) {
                return streamReader.ReadToEnd();
            }
        }


    }
}
