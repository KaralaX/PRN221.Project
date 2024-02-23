using PRN221.Project.Application.Common.Services;

namespace PRN221.Project.Infrastructure.Services;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow() => DateTime.UtcNow;
}