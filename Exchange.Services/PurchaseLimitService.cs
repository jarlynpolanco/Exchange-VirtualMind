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

        public IEnumerable<PurchaseLimit> GetPurchaseLimits => _unitOfWork.GetRepository<PurchaseLimit>().GetAll();

    }
}
