﻿using System;

namespace Cryoprison
{
    /// <summary>
    /// A global reporting system, for exceptions and jailbreaks.  This is not
    /// intended to be the actual return values of the system, but rather it
    /// provides debugging / logging support hooks.
    /// </summary>
    public static class Reporter
    {
        /// <summary>
        /// Hook function to be called when a jailbreak is reported, with the
        /// ID string of the jailbreak.  Set this value if you would like
        /// to receive global jailbreak notifications.
        /// </summary>
        public static Action<string> OnJailbreakReported { get; set; }

        /// <summary>
        /// Hook function to be called when an exception is reported, with a
        /// string identifying the context that caused the exception to be
        /// raised and the actual exception.  Set this value if you would like
        /// to receive global exception notifications.
        /// </summary>
        public static Action<string, Exception> OnExceptionReported { get; set; }

        /// <summary>
        /// Used internally to report exceptions.
        /// </summary>
        /// <param name="message">The context that the exception was raised in.</param>
        /// <param name="exception">The actual exception.</param>
        public static void ReportException(string message, Exception exception)
        {
            try
            {
                var r = OnExceptionReported;
                if (r != null)
                {
                    r(message, exception);
                }
            }
            catch (Exception)
            {
                // We don't forward exceptions from the reporting mechanism.
            }
        }

        /// <summary>
        /// Used internally to report jailbreaks.
        /// </summary>
        /// <param name="id">The jailbreak ID string.</param>
        public static void ReportJailbreak(string id)
        {
            try
            {
                var r = OnJailbreakReported;
                if (r != null)
                {
                    r(id);
                }
            }
            catch (Exception)
            {
                // We don't forward exceptions from the reporting mechanism.
            }
        }
    }
}
