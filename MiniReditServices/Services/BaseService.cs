using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Service.Services.Base
{
    /// <summary>
    /// The service responsible for writing logfiles
    /// </summary>
    public abstract class BaseService
    {
        #region Static prop's.

        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private readonly string _logPrefix;

        #endregion

        #region Ctor 

        public BaseService()
        {
            _logPrefix = this.GetType().UnderlyingSystemType.Name + " - ";
        }

        #endregion

        /// <summary>
        /// Logs an information message with log prefix
        /// </summary>
        /// <param name="message">Message</param>
        protected void LogInformation(string message)
        {
            _log.Info(_logPrefix + message);

        }

        /// <summary>
        /// Logs a warning message with log prefix
        /// </summary>
        /// <param name="message">Message</param>
        protected void LogWarning(string message)
        {
            _log.Warn(_logPrefix + message);

        }

        /// <summary>
        /// Logs a error message with log prefix and exception
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="exception">Exception</param>
        protected void LogError(string message, Exception exception)
        {
            _log.Error(_logPrefix + message, exception);
        }

        /// <summary>
        /// Start stopwatch.
        /// </summary>
        /// <param name="stopwatch">Stopwatch</param>
        /// <returns>Stopwatch</returns>
        protected Stopwatch StartStopwatch(Stopwatch stopwatch)
        {
            stopwatch.Start();
            return stopwatch;
        }

        /// <summary>
        /// Stop stopwatch and log.
        /// </summary>
        /// <param name="stopwatch">Stopwatch</param>
        /// <param name="logMessage">Log message (Method name)</param>
        /// <returns>Stopwatch</returns>
        protected Stopwatch StopStopwatch(Stopwatch stopwatch, string logMessage)
        {
            stopwatch.Stop();
            _log.Info($"STOPWATCH {logMessage} executed in: {stopwatch.Elapsed.TotalSeconds} s");
            stopwatch.Reset();

            return stopwatch;
        }

        /// <summary>
        /// Gets method name on async methods instead of movenext
        /// </summary>
        /// <param name="name">Method name</param>
        /// <returns>Method name</returns>
        protected static string GetActualAsyncMethodName([CallerMemberName]string name = null) => name;
    }
}
