using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Reader.Interface.Models;

namespace DataUpload.Services
{
   public interface IDealService
    {
        List<Deal> GetAll();
        bool IsFileTypeSupported(string type);
        string[] GetMostPopularVehicle();
        Task<List<CustomError>> UploadDealsAsync(IFormFile file);

       
    }
}
