using Exchange.Contracts;
using Exchange.Data;
using Exchange.Data.Models;

namespace Exchange.Services
{
    public class LogService : ILog
    {
        private readonly UnitOfWork<AppDbContext> _unitOfWork;

        public LogService(UnitOfWork<AppDbContext> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void WriteLog(string message, string typeLog)
        {
            _unitOfWork.GetRepository<Log>().Add(new Log() { Message = message, LogType = typeLog });
            _unitOfWork.Save();
        }
    }
}
