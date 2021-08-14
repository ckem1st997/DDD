using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
namespace UserAPI.Infrastructure.Middlewares
{
    public class FailingMiddleware
    {
        private readonly RequestDelegate _next;
        private bool _mustFail;
        private readonly FailingOptions _options;
        private readonly ILogger _logger;

        public FailingMiddleware(RequestDelegate next, ILogger<FailingMiddleware> logger, FailingOptions options)
        {
            _next = next;
            _options = options;
            _mustFail = false;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            var path = context.Request.Path;
            if (path.Equals(_options.ConfigPath, StringComparison.OrdinalIgnoreCase))
            {
                await ProcessConfigRequest(context);
                return;
            }

            if (MustFail(context))
            {
                _logger.LogInformation("Response for path {Path} will fail.", path);
                context.Response.StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;
                context.Response.ContentType = "text/plain";
                MessageFailingMiddleware messageFailing = new MessageFailingMiddleware();
                messageFailing.StatusCode = context.Response.StatusCode;
                messageFailing.Message = "Bạn hông thể truy cập vào trang này do bộ chặn FailingMiddleware đã được bật !";
                await context.Response.WriteAsJsonAsync(messageFailing);
            }
            else
            {
                // cho phép cho truy cập điểm cuối
                await _next.Invoke(context);
            }
        }

        private async Task ProcessConfigRequest(HttpContext context)
        {
            var enable = context.Request.Query.Keys.Any(k => k == "enable");
            var disable = context.Request.Query.Keys.Any(k => k == "disable");

            if (enable && disable)
            {
                throw new ArgumentException("Must use enable or disable querystring values, but not both");
            }

            if (disable)
            {
                _mustFail = false;
                await SendOkResponse(context, "FailingMiddleware đang tắt, các yêu cầu sẽ được xử lý !");
                return;
            }
            if (enable)
            {
                _mustFail = true;
                await SendOkResponse(context, "FailingMiddleware đang bật, các yêu cầu sẽ trả về lỗi HTTP 500");
                return;
            }

            // If reach here, that means that no valid parameter has been passed. Just output status
            await SendOkResponse(context, string.Format("FailingMiddleware đang {0}", _mustFail ? "bật" : "tắt"));
            return;
        }

        private async Task SendOkResponse(HttpContext context, string message)
        {
            context.Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
            context.Response.ContentType = "text/plain";
            MessageFailingMiddleware messageFailing = new MessageFailingMiddleware();
            messageFailing.StatusCode = context.Response.StatusCode;
            messageFailing.Message = message;
            await context.Response.WriteAsJsonAsync(message);
        }

        private bool MustFail(HttpContext context)
        {
            var rpath = context.Request.Path.Value;

            if (_options.NotFilteredPaths.Any(p => p.Equals(rpath, StringComparison.InvariantCultureIgnoreCase)))
            {
                return false;
            }

            return _mustFail &&
                (_options.EndpointPaths.Any(x => x == rpath)
                || _options.EndpointPaths.Count == 0);
        }
    }
}
