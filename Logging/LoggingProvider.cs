using Microsoft.Extensions.Logging;
using System.Net.Http;

namespace ChargesWFM.UI.Logging
{
    public class LoggingProvider : ILoggerProvider
    {
        public HttpClient _http;
        public LoggingProvider(HttpClient http)
        {
            _http = http;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new UILogger(this, _http, categoryName);
        }

        public void Dispose()
        {
        }
    }

}
