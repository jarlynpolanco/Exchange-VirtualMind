using Exchange.Data;
using Exchange.Data.Models;
using System.Collections.Generic;

namespace Exchange.Services
{
    public class PurchaseLimitService
    {
        private readonly UnitOfWork<AppDbContext> _unitOfWork;

        public PurchaseLimitService(UnitOfWork<AppDbContext> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public bool IsValidCurrency(string currency) => _unitOfWork.GetRepository<PurchaseLimit>().Count(x => x.Currency == currency) > 0;


        public IEnumerable<PurchaseLimit> GetPurchaseLimits => _unitOfWork.GetRepository<PurchaseLimit>().GetAll();

    }
}
