using Reader.Interface.Models;
using DataUpload.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;

namespace DataUpload.WebAPI.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
   
    public class DealController : ControllerBase
    {
        protected IDealService _dealService;
        private readonly ILogger<DealController> _logger;
        public DealController(ILogger<DealController> logger, IDealService dealService)
        {
            _logger = logger;
            _dealService = dealService;
        }

        // GET: api/<DealController>
        [HttpGet]
        public IEnumerable<Deal> Get()
        {
            _logger.LogInformation("Fetch all Deals from InMemoryDB service");
            Response.Headers.Add("Access-Control-Allow-Origin", "*");
            return _dealService.GetAll();
        }

        [HttpGet("GetAllVehicles")]
        public IEnumerable<Deal> GetAllVehicles()
        {
            _logger.LogInformation("Fetch all Deals from InMemoryDB service");
            try
            {
                Response.Headers.Add("Access-Control-Allow-Origin", "*");
                return _dealService.GetAll();
            }
            catch(Exception ex)
            {
                _logger.LogError("Error while retrieving deals list. {0}", ex.StackTrace);
            }
            return null;
        }

        [HttpGet("GetMostPopularVehicle")]
        public string[] GetMostPopularVehicle()
        {
            _logger.LogInformation("Fetch all Deals from InMemoryDB service");
            string[] vehicles = null;
            try
            {
                vehicles = _dealService.GetMostPopularVehicle();
            }
            catch(Exception ex)
            {
                _logger.LogError("Error while retrieving most popular vehicles list. {0}", ex.StackTrace);
            }
            return vehicles;
        }

        [HttpPost]
        public async Task<FileUploadModel> PostAsync([FromForm] List<IFormFile> files)
        {
            _logger.LogInformation("Uploading deals");

            FileUploadModel result = new FileUploadModel();
            List<CustomError> errorsList = new List<CustomError>();
            try
            {
                foreach (var formFile in files)
                {
                    if (formFile.Length > 0)
                    {
                        if (Path.GetExtension(formFile.FileName).ToLower() != ".csv")
                        {
                            CustomError error = new CustomError();

                            error.message = string.Format("Filename: {0} Error: {1}", formFile.FileName, "Invalid file type. supported file types are: csv");
                            errorsList.Add(error);
                        }
                        else
                        {
                            var errors = await _dealService.UploadDealsAsync(formFile);
                            if (errors != null && errors.Count > 0)
                            {
                                errors.First().message = string.Format("Filename: {0} Error(s): {1}", formFile.FileName, errors.First().message);
                                errorsList.AddRange(errors);
                            }
                        }
                    }
                }
                result.deals = GetAllVehicles().ToList();
                result.errors = errorsList;
            }
            catch(Exception ex)
            {
                _logger.LogError(string.Format("Error while uploading deals to the db. Error is {0}", ex.StackTrace));
            }
            return result;
        }


    }
}
