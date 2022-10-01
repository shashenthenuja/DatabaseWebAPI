using BankLib;
using BusinessLayer.Models;
using DataAccessLayer.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BusinessLayer.Controllers
{
    public class GenerateDataController : ApiController
    {
        Access access = new Access();
        DatabaseClass data = new DatabaseClass();
        public IHttpActionResult GenerateData()
        {
            List<DataStruct> list = data.getList();
            int count = 0;
            foreach (DataStruct item in list)
            {
                BankData bd = new BankData();
                bd.Id = count;
                bd.FirstName = item.firstName;
                bd.LastName = item.lastName;
                bd.AccNum = (int)item.acctNo;
                bd.Pin = (int)item.pin;
                bd.Balance = (int)item.balance;
                RestRequest request = new RestRequest("api/bankdata/", Method.Put);
                request.AddJsonBody(JsonConvert.SerializeObject(bd));
                RestResponse response = access.restClient.Execute(request);
                count++;
            }
            return Ok();
        }
    }
}
