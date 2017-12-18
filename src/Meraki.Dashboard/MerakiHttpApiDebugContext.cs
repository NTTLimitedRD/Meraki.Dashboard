using System;
using System.Threading;
using MerakiDashboard;

namespace Meraki.Dashboard
{
    /// <summary>
    /// Instantiate one of these around calls to <see cref="MerakiDashboardClient"/> 
    /// or <see cref="MerakiHttpApiClient"/> to see dumps of any Meraki API calls.
    /// </summary>
    public class MerakiHttpApiDebugContext: IDisposable
    {
        /// <summary>
        /// Count of active contexts. Used to ensure nesting does not cause problems.
        /// </summary>
        private static readonly AsyncLocal<int> Context = new AsyncLocal<int>();

        /// <summary>
        /// Create a new <see cref="MerakiHttpApiDebugContext"/>.
        /// </summary>
        public MerakiHttpApiDebugContext()
        {
            Context.Value = Context.Value + 1;
        }

        /// <summary>
        /// Finalizer.
        /// </summary>
        ~MerakiHttpApiDebugContext()
        {
            Dispose(false);
        }

        /// <summary>
        /// Actually dispose resources.
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                Context.Value = Context.Value - 1;
                if (Context.Value < 0 )
                {
                    throw new InvalidOperationException("Too many MerakiHttpApiDebugContext disposes.");
                }
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Is this context set?
        /// </summary>
        /// <returns></returns>
        public static bool IsSet()
        {
            return Context.Value > 0;
        }

    }
}
