using Queues.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Mvc;

namespace Queues.Controllers
{
    public class RequestApiController : ApiController
    {
        public static IEnumerable<Request> rlist;
        // GET: RequestApi
        //public ActionResult Index()
        //{
        //    return View();
        //}
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("myroute/sreetama/")]
        public IHttpActionResult Post()
        {
            int op = globalMethod.addRequestFromWeb();
            if (op == 0)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            return Ok();
        }
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/addrequest/")]
        public IHttpActionResult Post(Request r)
        {
            int op = globalMethod.addrequesttocollection(r);
            if (op == 1)
                return Ok();
            else
                return NotFound();
        }
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/getallrequest")]
         public IHttpActionResult GetAllRequest()
        {
            rlist = globalMethod.blockingcollectionreturn().ToList();
            return Ok(rlist);
        }
    }
}