using BHBus.Application.Interfaces;
using BHBus.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace BHBus.API.Controllers
{
    [EnableCors(origins: "http://localhost:55779", headers: "*", methods: "get,post")]
    [RoutePrefix("api/v1/public/busline")]
    public class BusLineController : ApiController
    {
        private BusLine _busLine;

        private readonly IBusLineAppService _busLineAppService;

        public BusLineController(IBusLineAppService busLineAppService)
        {
            _busLineAppService = busLineAppService;
        }

        [HttpPost]
        [Route("create/line/{line}")]
        public HttpResponseMessage RegisterBusLine(string line)
        {
            try
            {
                _busLine = new BusLine(line);

                _busLineAppService.Add(_busLine);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, _busLine);
        }

        [Route("get")]
        public HttpResponseMessage GetBusLine()
        {
            List<BusLine> busList;

            try
            {
                busList = _busLineAppService.GetAllAsNoTracking().ToList();
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, busList);
        }

        protected override void Dispose(bool disposing)
        {
            _busLineAppService.Dispose();
            base.Dispose(disposing);
        }
    }
}