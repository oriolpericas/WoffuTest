using JobTitleAPI;
using JobTitleAPI.Models;
using Newtonsoft.Json;
using System;

namespace UnitTest {
    class Program {
        static void Main(string[] args) {
            WoffuJobTitlesService api = new WoffuJobTitlesService() {
                URL = "https://app-dev.woffu.com/api/v1",
                Username = "hhkgBYNxYd0HIaLwhpzucEkmLr%2fHbDOZ6HkmDvFvjGFG1nnMgmnAcw%3d%3d"
            };

            try {
                // Get all job titles
                Console.WriteLine("Getting ALL job titles");
                var titles = api.GetJobTitles();
                Console.WriteLine("Success! Job titles found: " + titles.Length);
                Console.WriteLine("\n\n");


                // Create a new job title
                Console.WriteLine("Creating a new job title");
                var createdTitle = api.CreateJobTitle(new JobTitle() {
                    CompanyId = 14138,
                    Name = "Test ANalst job",
                    JobTitleKey = "TestAnalystJobTitle"
                });
                Console.WriteLine("Success! Job title created:");
                Console.WriteLine(JsonConvert.SerializeObject(createdTitle, Formatting.Indented));
                Console.WriteLine("\n\n");


                // Get only one job title
                Console.WriteLine("Getting one job title");
                var singleTitle = api.GetJobTitle(createdTitle.JobTitleId);
                Console.WriteLine("Success! Job title obtained:");
                Console.WriteLine(JsonConvert.SerializeObject(singleTitle, Formatting.Indented));
                Console.WriteLine("\n\n");


                // Edit a job title
                Console.WriteLine("Correcting name of one job title");
                singleTitle.Name = "Test Analyst job title";
                api.EditJobTitle(singleTitle);
                Console.WriteLine("Success!");
                // Get the edited job title
                Console.WriteLine("Getting the edited job title");
                var editedTitle = api.GetJobTitle(singleTitle.JobTitleId);
                Console.WriteLine("Job title obtained:");
                Console.WriteLine(JsonConvert.SerializeObject(editedTitle, Formatting.Indented));
                // Check the name has changed
                if (editedTitle.Name != singleTitle.Name) {
                    Console.WriteLine("[ERROR] The edited job title name does not match!");
                } else {
                    Console.WriteLine("Success! The name has been corrected");
                }
                Console.WriteLine("\n\n");


                // Delete the job title
                Console.WriteLine("Deleting one job title");
                api.DeleteJobTitle(singleTitle.JobTitleId);
                Console.WriteLine("Success!");

            } catch (Exception ex) {
                Console.WriteLine("[ERROR]: " + ex.ToString());
            }
        }

    }
}
