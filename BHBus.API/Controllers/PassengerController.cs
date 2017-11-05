using BHBus.Application.Interfaces;
using BHBus.Domain.Entities;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace BHBus.API.Controllers
{
    [RoutePrefix("api/v1/public/passenger")]
    [EnableCors(origins: "http://localhost:55779", headers: "*", methods: "get,post")]
    public class PassengerController : ApiController
    {
        private Passenger _passenger;

        private readonly IPassengerAppService _passengerAppService;

        public PassengerController(IPassengerAppService passengerAppService)
        {
            _passengerAppService = passengerAppService;
        }

        [HttpPost]
        [Route("create/name/{name}/email/{email}/password/{password}")]
        public HttpResponseMessage RegisterPassenger(string name, string email, string password)
        {
            try
            {
                _passenger = new Passenger(name, email, password);

                 _passengerAppService.RegisterPassenger(_passenger);

            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, "Gravado com sucesso.");
        }

        protected override void Dispose(bool disposing)
        {
            _passengerAppService.Dispose();
            base.Dispose(disposing);
        }
    }
}
