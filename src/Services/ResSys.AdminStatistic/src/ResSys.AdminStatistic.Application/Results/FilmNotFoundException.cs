using ResSys.AdminStatistic.Application;

namespace ResSys.AdminStatistic.Application
{
    public sealed class FilmNotFoundException : ApplicationException
    {
        public FilmNotFoundException(string message)
            : base(message)
        { }
    }
}
