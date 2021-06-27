using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reader.Interface.Models
{
    public class FileUploadModel
    {
        public List<Deal> deals { get; set; }
        public List<CustomError> errors { get; set; }
    }

    public class CustomError
    {
        public string message { get; set; }
    }
}
