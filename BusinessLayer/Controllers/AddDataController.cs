using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RestSharp;
using Newtonsoft.Json;

namespace BusinessLayer.Controllers
{
    public class AddDataController : ApiController
    {
        RestClient restClient = new RestClient("http://localhost:53746/");
        public IHttpActionResult AddData(int id, BankData bankData)
        {
            RestRequest request = new RestRequest("api/putbankdata/{id}", Method.Put);
            request.AddParameter("id", id);
            request.AddJsonBody(JsonConvert.SerializeObject(bankData));
            RestResponse response = restClient.Execute(request);
            return Ok(response);
        }

    }
}
