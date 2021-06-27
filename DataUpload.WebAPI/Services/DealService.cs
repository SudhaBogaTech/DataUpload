using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Reader.Interface.Models;
using Reader.Interface;
using System.Composition.Hosting;
using System.Reflection;
using System.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition;
using System.Runtime.Loader;

namespace DataUpload.Services
{
    public class DealService : IDealService
    {
        private readonly IDBService _inMemoryDBService;


        [System.ComponentModel.Composition.ImportMany(typeof(IFileReader))]
        private IEnumerable<Lazy<IFileReader, Dictionary<string, object>>> fileReaders { get; set; }

        public List<Deal> GetAll()
        {
            return _inMemoryDBService.FetchAllDeals();
        }
        public DealService(IDBService dbService)
        {
            
            _inMemoryDBService = dbService;
        }

       
        public async Task<List<CustomError>> UploadDealsAsync(IFormFile file)
        {

            using (var memoryStream = new MemoryStream())
            {
                //dealService.clear();
                await file.CopyToAsync(memoryStream);

                //var catalog = new AggregateCatalog();
                //catalog.Catalogs.Add(new DirectoryCatalog(@"C:\DealDataUpload\DataUpload\DataUpload.WebAPI\bin\Plugins", "*.dll"));
                //var container = new CompositionContainer(catalog);
                //container.ComposeParts(this);
                var assemblies = Directory
                    .GetFiles(@"C:\DealDataUpload\DataUpload\DataUpload.WebAPI\bin\Plugins", "*.dll", SearchOption.AllDirectories)
                    .Select(AssemblyLoadContext.Default.LoadFromAssemblyPath)
                    .ToList();
                var configuration = new ContainerConfiguration()
                    .WithAssemblies(assemblies);

                using (var container = configuration.CreateContainer())
                {
                    string fileType = Path.GetExtension(file.FileName).ToLower().Remove(0, 1);
                    foreach (var readerType in container.GetExports<IFileReader>(fileType))
                    {
                        
                            FileUploadModel uploadresult = readerType.Read(memoryStream);


                            if (uploadresult.errors.Count == 0)
                            {
                                List<Deal> totDeals = uploadresult.deals;
                                _inMemoryDBService.AddMultipleDeals(totDeals);
                            }
                            else
                            {
                                return uploadresult.errors;
                            }
                        
                    }
                }
            }
                // var exports = container.GetExports<IFileReader,IDealReaderAttributeMetadata>();
                //var _fileReader = exports.Where(m => m.Metadata.FormatType == ".csv").FirstOrDefault().Value;

                
                
            
            return null;
        }

        public string[] GetMostPopularVehicle()
        {
            return _inMemoryDBService.FetchMostPopularVehicles();
        }
    }
}
