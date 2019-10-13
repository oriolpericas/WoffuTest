using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobTitleAPI.Models {
    public class JobTitle {
        public int JobTitleId { get; set; }
        public string JobTitleKey { get; set; }
        public string Name { get; set; }
        public int CompanyId { get; set; }

    }
}
