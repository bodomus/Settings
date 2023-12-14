using Microsoft.Extensions.Logging;
using System;

namespace LottoSheli.SendPrinter.Bootstraper
{
    /// <summary>
    /// Provides global objects factory
    /// </summary>
    public interface IAbstractObjectsFactory : IDisposable
    {
        /// <summary>
        /// Gets service of requested type
        /// </summary>
        /// <returns></returns>
        TService GetService<TService>();

        /// <summary>
        /// Gets configured logger factory
        /// </summary>
        /// <returns></returns>
        ILoggerFactory GetLoggerFactory();

    }
}
