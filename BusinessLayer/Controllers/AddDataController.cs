using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RestSharp;
using Newtonsoft.Json;
using BusinessLayer.Models;

namespace BusinessLayer.Controllers
{
    public class AddDataController : ApiController
    {
        Access access = new Access();
        public IHttpActionResult AddData(int id, BankData bankData)
        {
            RestRequest request = new RestRequest("api/bankdata/", Method.Post);
            request.AddJsonBody(JsonConvert.SerializeObject(bankData));
            RestResponse response = access.restClient.Execute(request);
            if (response.Content != null)
            {
                return Ok(response.Content);
            }
            return StatusCode(HttpStatusCode.NoContent);
        }

    }
}
