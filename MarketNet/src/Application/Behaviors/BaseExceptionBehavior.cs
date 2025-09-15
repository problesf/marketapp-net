using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using MarketNet.Domain.Exceptions.Base;

namespace MarketNet.Application.Behaviors
{
    /// <summary>
    /// Captura únicamente las excepciones que heredan de BaseException
    /// y las vuelve a lanzar tras registrarlas en el log.
    /// </summary>
    public class BaseExceptionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<BaseExceptionBehavior<TRequest, TResponse>> _logger;

        public BaseExceptionBehavior(ILogger<BaseExceptionBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            try
            {
                return await next();
            }
            catch (BaseException bex)
            {
                _logger.LogWarning(bex,
                    "Excepción de dominio capturada en {RequestType}: {ErrorType} - {Message}",
                    typeof(TRequest).Name, bex.ErrorType, bex.Message);

                throw; 
            }
        }
    }
}