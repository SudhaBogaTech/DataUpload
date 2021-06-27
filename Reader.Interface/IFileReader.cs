using Reader.Interface.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Reader.Interface
{
    public interface IFileReader
    {

        FileUploadModel Read(MemoryStream memoryStream);

    }
}
