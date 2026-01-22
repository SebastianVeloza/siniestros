using Application.Services;
using log4net;

namespace WebAPI
{
    public class Log4NetAppLogger<T> : IAppLogger<T>
    {
        private readonly ILog _logger;

        public Log4NetAppLogger()
        {
            _logger = LogManager.GetLogger(typeof(T));
        }



        public void Info(string message)
        {
            _logger.Info(message);
        }

        public void Warn(string message)
        {
            _logger.Warn(message);
        }

        public void Error(string message, Exception ex = null)
        {
            if (ex != null)
                _logger.Error(message, ex);
            else
                _logger.Error(message);
        }
    }
}
