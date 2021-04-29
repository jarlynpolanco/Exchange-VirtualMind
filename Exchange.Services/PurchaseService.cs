using Exchange.Core.Exceptions;
using Exchange.Data;
using Exchange.Data.Models;
using System;
using System.Linq;
using System.Net;

namespace Exchange.Services
{
    public class PurchaseService
    {
        private readonly UnitOfWork<AppDbContext> _unitOfWork;
        private readonly PurchaseLimitService _purchaseLimitService;

        public PurchaseService(UnitOfWork<AppDbContext> unitOfWork, PurchaseLimitService purchaseLimitService)
        {
            _unitOfWork = unitOfWork;
            _purchaseLimitService = purchaseLimitService;

        }


        public Purchase AddPurchase(Purchase purchase) 
        {
            if (ValidatePurchases(purchase.UserId, purchase.Amount, purchase.Currency))
            {
                purchase.AmountResult = purchase.Amount * purchase.Rate;
                purchase.Date = DateTime.Now;
                _unitOfWork.GetRepository<Purchase>().Add(purchase);
                _unitOfWork.Save();
               
                return purchase;
            }
            return null;
        }

    public bool ValidatePurchases(int userId, decimal amount, string currency) 
        {
            var date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            var purchasesSum = _unitOfWork.GetRepository<Purchase>()
                .GetAll(x => x.UserId == userId && x.Date.Date >= date.Date && x.Date.Date <= DateTime.Now.Date)
                .Sum(s => s.Amount);

            var purchaseLimitAmount = _purchaseLimitService.GetPurchaseLimits.FirstOrDefault(x => x.Currency == currency);

            if ((purchasesSum + amount) > purchaseLimitAmount.AmountLimit)
                throw new HttpStatusException($"The user does not have monthly availability in the specified currency",
                   HttpStatusCode.Forbidden);

            return true;
        }


    }
}
