
namespace DataUpload.DealService.UnitTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    
    using Moq;
    using System.Collections.Generic;
    using System;
    using DataUpload.Services;
    using Reader.Interface.Models;

    [TestClass]
    public class DBServiceTest
    {
        [TestMethod]
        public void VerifyAddDealToTheDBService()
        {
            // arrange
            InMemoryDBService dbservice = new InMemoryDBService();
            Deal mockDeal = new Deal() { CustomerName = "testcustomer", Date = System.DateTime.Now, DealershipName = "Curtis Ryan Honda", DealNumber = 1, Price = 35000, Vehicle = "Honda CRV 2012" };

           

            // act
             dbservice.AddDeal(mockDeal);

            // assert
            Assert.IsTrue(dbservice.FetchAllDeals().Count > 0, string.Format("Expected deals count is greater than where as the actual deal count is: {0} ", dbservice.FetchAllDeals().Count));
        }
        [TestMethod]
        public void VerifyMostOftenUsedVehicles()
        {
            // arrange
            InMemoryDBService dbservice = new InMemoryDBService();
            List<Deal> dealsList = new List<Deal>(){
                new Deal() { CustomerName ="testcustomer", Date=System.DateTime.Now, DealershipName= "Curtis Ryan Honda" , DealNumber = 1, Price=35000, Vehicle="Honda CRV 2012"},
                 new Deal() { CustomerName ="testcustomer2", Date=System.DateTime.Now, DealershipName= "Test DBA" , DealNumber = 2, Price=55000, Vehicle="Honda CRV 2013"},
                  new Deal() { CustomerName ="testcustomer3", Date=System.DateTime.Now, DealershipName= "ABC Generie" , DealNumber = 3, Price=38000, Vehicle="Honda CRV 2018"},
                   new Deal() { CustomerName ="testcustomer4", Date=System.DateTime.Now, DealershipName= "test5" , DealNumber = 4, Price=37000, Vehicle="Honda CRV 2018"}
            };
            dbservice.AddMultipleDeals(dealsList);
           
            // act
            string[] vehicles = dbservice.FetchMostPopularVehicles();

            // assert
            Assert.IsTrue(Array.IndexOf(vehicles, "Honda CRV 2018") != -1, "Expected a match did not found in the collection");
        }

    }
}
