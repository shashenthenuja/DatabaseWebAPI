using BusinessLayer.Models;
using DataAccessLayer.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BusinessLayer.Controllers
{
    public class SearchDataController : ApiController
    {
        List<BankData> list = new List<BankData>();
        Access access = new Access();
        public IHttpActionResult SearchData(string name)
        {
            
            RestRequest request = new RestRequest("api/bankdata/", Method.Get);
            RestResponse response = access.restClient.Execute(request);
            DbSet<BankData> set = JsonConvert.DeserializeObject<DbSet<BankData>>(response.Content);
            foreach (BankData item in set)
            {
                if (item.FirstName.Equals(name))
                {
                    list.Add(item);
                }
            }
            return Json(list);
        }
    }
}
