using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProDat.Web2.Models.ETL
{
    public class FileUpload
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IFormFile File { get; set; }
        public DateTime UploadDate { get; set; }
    }
}
