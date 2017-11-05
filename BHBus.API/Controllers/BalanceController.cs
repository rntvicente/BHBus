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
    [RoutePrefix("api/v1/public/balance")]
    [EnableCors(origins: "http://localhost:55779", headers: "*", methods: "get,post")]
    public class BalanceController : ApiController
    {
        private Balance _balance;

        private readonly IBalanceAppService _balanceAppService;

        public BalanceController(IBalanceAppService balanceAppService)
        {
            _balanceAppService = balanceAppService;
        }

        [Route("credits/card/{card}/value/{value:double}")]
        [HttpPost]
        public HttpResponseMessage Credits(Guid card, double value)
        {
            try
            {
                _balance = _balanceAppService.Credits(card, value);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, _balance);
        }

        [Route("debits/card/{card}/busline/{busline}")]
        [HttpPost]
        public HttpResponseMessage Debits(Guid card, string busline)
        {
            try
            {
                _balance = _balanceAppService.Debits(card, busline);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, _balance);
        }

        [Route("debits/")]
        public HttpResponseMessage GetDebits(Guid card)
        {
            List<Balance> balanceList;

            try
            {
                balanceList = _balanceAppService.GetBalanceForNumberCardAsNoTracking(card).ToList();
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, balanceList
                .OrderByDescending(o => o.DateRegister)
                .Select(b => new { b.PassengerID, b.DateRegister, b.Value, b.TransactionType })
                .ToList());
        }

        [Route("debits/{start}/{finish}")]
        public HttpResponseMessage GetDebits(string start, string finish)
        {
            List<Balance> balanceList;

            try
            {
                balanceList = _balanceAppService.GetBalanceForDateAsNoTracking(Convert.ToDateTime(start), Convert.ToDateTime(finish)).ToList();
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, balanceList
                .Select(b => new { b.PassengerID, b.NumberCard, b.DateRegister, b.Value, b.TransactionType })
                .OrderByDescending(o => o.DateRegister)
                .ToList());
        }

        protected override void Dispose(bool disposing)
        {
            _balanceAppService.Dispose();
            base.Dispose(disposing);
        }
    }
}