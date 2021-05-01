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
            var foreignAmount = purchase.Amount / purchase.Rate;
            if (ValidatePurchases(purchase.UserId, foreignAmount, purchase.Currency))
            {
                purchase.AmountResult = foreignAmount;
                purchase.Date = DateTime.Now;
                _unitOfWork.GetRepository<Purchase>().Add(purchase);
                _unitOfWork.Save();
               
                return purchase;
            }
            return null;
        }

    public bool ValidatePurchases(int userId, decimal foreignAmount, string currency) 
        {
            var date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            var purchasesSum = _unitOfWork.GetRepository<Purchase>()
                .GetAll(x => x.UserId == userId && x.Date.Date >= date.Date && x.Date.Date <= DateTime.Now.Date)
                .Sum(s => s.AmountResult);

            var purchaseLimitAmount = _purchaseLimitService.GetPurchaseLimits.FirstOrDefault(x => x.Currency == currency);

            if ((purchasesSum + foreignAmount) > purchaseLimitAmount.AmountLimit)
                throw new HttpStatusException($"The user does not have monthly availability in the specified currency",
                   HttpStatusCode.Forbidden);

            return true;
        }
    }
}
