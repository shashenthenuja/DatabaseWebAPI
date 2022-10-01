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
    public class GetAllDataController : ApiController
    {

        RestClient restClient = new RestClient("http://localhost:53746/");
        [ResponseType(typeof(BankData))]
        public IHttpActionResult GetAllData()
        {
            RestRequest request = new RestRequest("api/getbankdatas/", Method.Get);
            RestResponse response = restClient.Execute(request);
            return Ok(response);
        }
    }
}
