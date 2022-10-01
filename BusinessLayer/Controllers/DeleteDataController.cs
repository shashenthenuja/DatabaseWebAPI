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
    public class DeleteDataController : ApiController
    {
        Access access = new Access();
        public IHttpActionResult AddData(int id)
        {
            RestRequest request = new RestRequest("api/bankdata/{id}", Method.Delete);
            request.AddParameter("id", id);
            RestResponse response = access.restClient.Execute(request);
            return Ok(response);
        }
    }
}
