
namespace DataUpload.DealService.UnitTests
{
    using DataUpload.Services;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    
    using Moq;
    using Reader.Interface.Models;
    using System.Collections.Generic;
    [TestClass]
    public class DealServiceTest
    {
        [TestMethod]
        public void VerifyEmptyDeals()
        {
            // arrange
            var dbServiceMock = new Mock<IDBService>();
            dbServiceMock.Setup(_ => _.FetchAllDeals()).Returns(new System.Collections.Generic.List<Deal>());
          
            var dealService = new DealService(dbServiceMock.Object);

            // act
            var deals = dealService.GetAll();

            // assert
            Assert.AreEqual(0, deals.Count, "No deals are present in the Database");
        }
        [TestMethod]
        public void VerifyDealsWithValidData()
        {
            // arrange
            var dbServiceMock = new Mock<IDBService>();
            List<Deal> dealsList = new List<Deal>(){
                new Deal() { CustomerName ="testcustomer", Date=System.DateTime.Now, DealershipName= "Curtis Ryan Honda" , DealNumber = 1, Price=35000, Vehicle="Honda CRV 2012"},
                 new Deal() { CustomerName ="testcustomer2", Date=System.DateTime.Now, DealershipName= "Test DBA" , DealNumber = 2, Price=55000, Vehicle="Honda CRV 2013"},
                  new Deal() { CustomerName ="testcustomer3", Date=System.DateTime.Now, DealershipName= "ABC Generie" , DealNumber = 3, Price=38000, Vehicle="Honda CRV 2015"},
                   new Deal() { CustomerName ="testcustomer4", Date=System.DateTime.Now, DealershipName= "test5" , DealNumber = 4, Price=37000, Vehicle="Honda CRV 2018"}
            };
            dbServiceMock.Setup(_ => _.FetchAllDeals()).Returns(dealsList);
            
            var dealService = new DealService( dbServiceMock.Object);

            // act
            var deals = dealService.GetAll();

            // assert
            Assert.IsTrue(deals.Count == 4, string.Format("Expected deals count should be 4 where as Actual is: {0}", deals.Count));
        }

    }
}
