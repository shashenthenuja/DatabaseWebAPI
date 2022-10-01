using DataAccessLayer.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace BusinessLayer.Controllers
{
    public class GetBankDataController : ApiController
    {
        RestClient restClient = new RestClient("http://localhost:53746/");
        [ResponseType(typeof(BankData))]
        public IHttpActionResult GetBankData(int id)
        {
            RestRequest request = new RestRequest("api/getbankdata/{id}", Method.Get);
            request.AddParameter("id", id);
            RestResponse response = restClient.Execute(request);
            return Ok(response);
        }
    }
}
