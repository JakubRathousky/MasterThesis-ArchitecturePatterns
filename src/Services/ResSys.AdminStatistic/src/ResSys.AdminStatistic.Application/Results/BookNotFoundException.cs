using ResSys.AdminStatistic.Application;

namespace ResSys.AdminStatistic.Application
{
    public sealed class BookNotFoundException : ApplicationException
    {
        public BookNotFoundException(string message)
            : base(message)
        { }
    }
}
