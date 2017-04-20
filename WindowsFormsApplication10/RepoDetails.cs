using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication10
{
    class RepoDetails
    {
        public string Name { get; set; }
        public string Description  { get; set; }
        public string CreateDate { get; set; }

        public RepoDetails()
        {

        }

        public RepoDetails (string name, string description, string createdate)
        {
            Name = name;
            Description = description;
            CreateDate = createdate; 
        }
    }
}
