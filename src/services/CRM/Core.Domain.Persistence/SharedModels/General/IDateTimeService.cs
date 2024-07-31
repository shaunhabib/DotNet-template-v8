using System;

namespace Core.Domain.Persistence.SharedModels.General
{
    public interface IDateTimeService
    {
        DateTime NowUtc { get; }
        DateTime Now { get; }
    }
}
