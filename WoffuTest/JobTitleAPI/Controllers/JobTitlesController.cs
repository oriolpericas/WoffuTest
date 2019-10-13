using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JobTitleAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobTitleAPI.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class JobTitlesController : ControllerBase {

        // Woffu Job titles service API
        WoffuJobTitlesService api = new WoffuJobTitlesService() {
            URL = "https://app-dev.woffu.com/api/v1",
            Username = "hhkgBYNxYd0HIaLwhpzucEkmLr%2fHbDOZ6HkmDvFvjGFG1nnMgmnAcw%3d%3d"
        };

        [HttpGet]
        public IEnumerable<JobTitle> Get() {
            return api.GetJobTitles();
        }

        [HttpGet("{id}", Name = "Get")]
        public JobTitle Get(int id) {
            return api.GetJobTitle(id);
        }

        [HttpPost]
        public void Post([FromBody] JobTitle value) {
            api.CreateJobTitle(value);
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] JobTitle value) {
            api.EditJobTitle(value);
        }

        [HttpDelete("{id}")]
        public void Delete(int id) {
            api.DeleteJobTitle(id);
        }
    }
}
