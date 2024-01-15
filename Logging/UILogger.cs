using System;
using Microsoft.Extensions.Logging;
using ChargesWFM.UI.Logging;
using ChargesWFM.UI;
using System.Net.Http;
using System.Threading.Tasks;
using ChargesWFM.UI.Models;
using ChargesWFM.UI.Logging.Models;

namespace ChargesWFM.UI.Logging
{
    public class UILogger : ILogger
    {
        protected readonly LoggingProvider _loggingprovider;

        private readonly HttpClient _http;
        public string _categoryName;

        public UILogger(LoggingProvider loggingprovider, HttpClient http, string categoryName)
        {
            _loggingprovider = loggingprovider;
            _http = http;
            _categoryName = categoryName;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel != LogLevel.None;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }
            _ = WriteLog(logLevel, eventId, state, exception, formatter)
                .ContinueWith(task => Console.WriteLine(task.Exception?.ToString() ?? "Failed to write log"), TaskContinuationOptions.OnlyOnFaulted);
        }

        public async Task UILogs(LogLevel logLevel, EventId eventId, string exception)
        {
            try
            {
                LogModel log = new LogModel
                {
                    logLevel = logLevel.ToString(),
                    eventId = eventId != null ? eventId.Name : "eventid",
                    exception = exception,
                    categoryName = _categoryName
                };
                var result = await ApiHelper.PostUsingMsgPackAsync<LogModelreturn>("/Logger/UIGrayLog", log, _http);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private async Task WriteLog<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            var logEntry = new LogEntry
            {
                Level = logLevel,
                EventId = eventId.Id,
                EventName = eventId.ToString(),
                Category = _categoryName,
                Message = state.ToString(),
                FormattedMessage = formatter(state, exception),
                Facility = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name,
                Version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? string.Empty,
            };
            await ApiHelper.PostUsingMsgPackAsync("/Logger/UI", logEntry, _http);
        }
    }
}
