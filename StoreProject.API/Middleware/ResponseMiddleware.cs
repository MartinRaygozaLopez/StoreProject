using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace StoreProject.API.Middleware
{
    public class ResponseMiddleware
    {
        public class ResponseClass
        {
            public string status { get; set; }

            public object data { get; set; }

            public object message { get; set; }

            public bool success { get; set; }

            public bool fail { get; set; }
        }

        public class ErrorDetails
        {
            public ErrorDetails(int statusCode, string message)
            {
                this.statusCode = statusCode;
                this.message = message;
            }
            public int statusCode { get; set; }

            public string message { get; set; }
        }

        private readonly RequestDelegate _next;

        public ResponseMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (IsSwagger(context))
                await this._next(context);
            else
            {
                var existingBody = context.Response.Body;

                using (var newBody = new MemoryStream())
                {
                    context.Response.Body = newBody;

                    try
                    {
                        await _next(context);

                        var newResponse = await FormatResponse(context.Response);

                        context.Response.Body = new MemoryStream();
                        newBody.Seek(0, SeekOrigin.Begin);
                        context.Response.Body = existingBody;
    
                        await context.Response.WriteAsync(newResponse);

                    }
                    catch (Exception ex)
                    {
                        var response = new ErrorDetails(context.Response.StatusCode, ex.Message);
                        var newResponse = JsonConvert.SerializeObject(response);

                        context.Response.Body = new MemoryStream();
                        newBody.Seek(0, SeekOrigin.Begin);
                        context.Response.Body = existingBody;
    
                        await context.Response.WriteAsync(newResponse);
                    }
                }
            }
        }

        private bool IsSwagger(HttpContext context)
        {
            return context.Request.Path.StartsWithSegments("/swagger");
        }

        private async Task<string> FormatResponse(HttpResponse response)
        {
            //We need to read the response stream from the beginning...and copy it into a string...I'D LIKE TO SEE A BETTER WAY TO DO THIS
            //
            response.Body.Seek(0, SeekOrigin.Begin);
            var content = await new StreamReader(response.Body).ReadToEndAsync();
            var Response = new ResponseClass();
            Response.status = response.StatusCode == 200 ? "Success" : "Error";

            if (!IsResponseValid(response))
            {
                Response.message = JsonConvert.DeserializeObject(JsonConvert.SerializeObject(new ErrorDetails(response.StatusCode, content)));
                Response.fail = true;
            }
            else
            {
                try
                {
                    Response.data = JsonConvert.DeserializeObject(content);
                }
                catch
                {
                    Response.data = content;
                }
                Response.success = true;
            }

            var json = JsonConvert.SerializeObject(Response);

            //We need to reset the reader for the response so that the client an read it
            response.Body.Seek(0, SeekOrigin.Begin);

            return $"{json}";
        }

        private bool IsResponseValid(HttpResponse response)
        {
            if ((response != null)
                && (response.StatusCode == 200
                || response.StatusCode == 201
                || response.StatusCode == 202))
            {
                return true;
            }
            return false;
        }
    }
}