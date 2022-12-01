using System.Diagnostics;
using System.Net;
using Newtonsoft.Json;
using WebApi.Middlewares;

namespace WebApi.Middlewares
{
	public class CustomExceptionMiddleware
	{
		private readonly RequestDelegate _next;

		public CustomExceptionMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task Invoke(HttpContext context)
		{
			var watch = Stopwatch.StartNew();
			try
			{
				string message = $"[Request] HTTP {context.Request.Method} - {context.Request.Path}";
				Console.WriteLine(message);
				await _next(context);
				watch.Stop();
				message = $"[Response] HTTP {context.Request.Method} - {context.Request.Path} responded. Status: {context.Response.StatusCode} in {watch.Elapsed.TotalMilliseconds}ms";
				Console.WriteLine(message);
			}
			catch (Exception ex)
			{
				watch.Stop();
				await HandleException(context, ex, watch);
			}
		}

		private Task HandleException(HttpContext context, Exception ex, Stopwatch watch)
		{
			context.Response.ContentType = "application/json";
			context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
			string message = $"[Error] HTTP {context.Request.Method} - {context.Request.Path} responded. Status: {context.Response.StatusCode}, Error Msg: {ex.Message} in {watch.Elapsed.TotalMilliseconds}ms";
			Console.WriteLine(message);
			var result = JsonConvert.SerializeObject(new { error = ex.Message }, Formatting.None);
			return context.Response.WriteAsync(result);
		}
	}
}

public static class CustomExceptionMiddlewareExtension
{
	public static IApplicationBuilder UseCustomExceptionMiddleware(this IApplicationBuilder builder)
	{
		return builder.UseMiddleware<CustomExceptionMiddleware>();
	}
}
