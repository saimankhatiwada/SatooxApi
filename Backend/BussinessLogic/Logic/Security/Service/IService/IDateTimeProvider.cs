namespace BussinessLogic.Logic.Security.Service.IService;

public interface IDateTimeProvider
{
    DateTimeOffset Now { get; }
}
