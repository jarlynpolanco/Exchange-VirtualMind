using AutoMapper;
using Exchange.Contracts;
using Exchange.Core.Exceptions;
using Exchange.Data.Models;
using Exchange.Models.DTOs;
using Exchange.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using Exchange.Models;

namespace Exchange.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ExchangeRateController : Controller
    {
        private readonly IExchangeRateProvider _exchangeRateProvider;
        private readonly PurchaseService _purchaseService;
        private readonly UserService _userService;
        private readonly IMapper _mapper;

        public ExchangeRateController(IExchangeRateProvider exchangeRateProvider, PurchaseService purchaseService,
            UserService userService, IMapper mapper) 
        {
            _exchangeRateProvider = exchangeRateProvider;
            _purchaseService = purchaseService;
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet("{currency}")]
        [Produces(typeof(RateDTO))]
        public async Task<ActionResult<GenericResponse<RateDTO>>> Rate(string currency)
        {
            currency = currency.ToUpper();
            var response = await _exchangeRateProvider.GetRateAsync(currency);

            if (response == null)           
                throw new HttpStatusException($"Something went wrong. Please contact the System Administrator.",
                  HttpStatusCode.InternalServerError);

            return Ok(new GenericResponse<RateDTO>()
            {
                Data = _mapper.Map<RateDTO>(response),
                ErrorCode = "200"
            });
        }


        [HttpPost]
        [Produces(typeof(PurchaseResponseDTO))]
        public async Task<ActionResult<GenericResponse<PurchaseResponseDTO>>> Purchase([FromBody] PurchaseDTO purchaseDTO)
        {
            var response = await _exchangeRateProvider.GetRateAsync(purchaseDTO.Currency);

            if (response == null)
                throw new HttpStatusException($"Something went wrong. Please contact the System Administrator.",
                  HttpStatusCode.InternalServerError);

            var rate = _mapper.Map<RateDTO>(response);

            var purchase = _mapper.Map<Purchase>(purchaseDTO);

            purchase.Rate = rate.Buy;

            var purchaseResponse = _purchaseService.AddPurchase(purchase);

            var user = _userService.FindUserById(purchaseResponse.UserId);

            var responseDTO = _mapper.Map<PurchaseResponseDTO>(purchaseResponse);

            responseDTO.FullName = $"{user.FirstName} {user.LastName}";
            responseDTO.UserName = user.UserName;

            return Ok(new GenericResponse<PurchaseResponseDTO>()
            {
                Data = responseDTO,
                ErrorCode = "200"
            });
        }
    }
}
