
using Core.Domain.Persistence.SharedModels.General;

namespace Web.Framework.Services
{
    public class DateTimeService : IDateTimeService
    {
        public DateTime NowUtc => DateTime.UtcNow;
        public DateTime Now => DateTime.Now;
    }
}
