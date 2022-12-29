using ProDat.Web2.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProDat.Web2.TagLibrary
{
    public class ProjectDetails
    {
        private static string projectName;
        private readonly TagContext _context;

        private static readonly ProjectDetails _ProjectDetails = new ProjectDetails();

        private ProjectDetails()
        {
            
        }

        public static ProjectDetails GetInstance() => _ProjectDetails;

        public string GetProjectName() => projectName;

    }
}
