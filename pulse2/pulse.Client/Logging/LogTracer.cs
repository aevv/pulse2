using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pulse.Client.Logging
{
    class LogTracer
    {
        private static object _sync = new object();
        private static LogTracer _instance;

        public static LogTracer Instance
        {
            get
            {
                lock (_sync)
                    return _instance ?? (_instance = new LogTracer());
            }
        }

        private LogTracer()
        {
            _coreTrace = new TraceSource("Core");
        }

        private TraceSource _coreTrace;

        public void TraceError(Exception ex)
        {
            lock (_sync)
            {
                _coreTrace.TraceEvent(TraceEventType.Error, 0, ex.Message);
            }
        }

        public void TraceInfo(object item)
        {
            lock (_sync)
            {
                _coreTrace.TraceInformation(item.ToString());
            }
        }

        public void TraceInfo(string message)
        {
            lock (_sync)
            {
                _coreTrace.TraceInformation(message);
            }
        }

        public void TraceInfo(string format, params object[] args)
        {
            lock (_sync)
            {
                _coreTrace.TraceInformation(format, args);
            }
        }
    }
}