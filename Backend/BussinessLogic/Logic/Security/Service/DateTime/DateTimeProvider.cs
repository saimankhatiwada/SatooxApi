using BussinessLogic.Logic.Security.Service.IService;

namespace BussinessLogic.Logic.Security.Service.DateTime;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTimeOffset Now => DateTimeOffset.Now;
}
