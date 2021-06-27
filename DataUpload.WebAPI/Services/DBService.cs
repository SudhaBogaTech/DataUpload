using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Reader.Interface.Models;

namespace DataUpload.Services
{
    public class InMemoryDBService : IDBService
    {
        private static List<Deal> deals = new List<Deal>();

        public void AddDeal(Deal newDeal)
        {
           if(!deals.Any(m => m.DealNumber == newDeal.DealNumber))
            deals.Add(newDeal);
        }

        static InMemoryDBService()
        {
            deals = new List<Deal>();
        }
        public void AddMultipleDeals(List<Deal> newdeals)
        {
            foreach (Deal deal in newdeals)
            {
                if(!deals.Any(m => m.DealNumber == deal.DealNumber))
                deals.Add(deal);
            }
        }

        public List<Deal> FetchAllDeals()
        {
           
            return deals;
        }

        public Deal GetDealByDealNumber(int dealNumber)
        {
            return deals.Where(m => m.DealNumber == dealNumber).FirstOrDefault();
        }

        public string[] FetchMostPopularVehicles()
        {
            if(deals.Count > 0)
            {
                var VehcileList = deals.GroupBy(m => m.Vehicle).Select(grp =>
                   new
                   {
                       vehicleName = grp.Key,
                       repeatCount = grp.Count()
                   }).OrderByDescending(a => a.repeatCount).Distinct(); ;
                if (VehcileList != null && VehcileList.FirstOrDefault() != null)
                {
                   return  VehcileList.Where(m => m.repeatCount == VehcileList.First().repeatCount).Select(m => m.vehicleName).ToArray();
                }
               
            }
            return null;
        }
    }
}
