using ClassLibraryHelper;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Hosting;
using Owin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.SelfHost;

[assembly: OwinStartupAttribute(typeof(MachDien.App.Api.Startup))]
namespace MachDien.App.Api
{
    public class Startup
    {
        public static string WEBPAGE { get; private set; }
        public static string HUB { get; private set; }

        public static bool WEBPAGE_ON { get; protected set; }
        public static bool HUB_ON { get; protected set; }

        public static void Run()
        {
            try
            {
                //start web client
                WEBPAGE = ConfigHelper.GetConfig("webpage", "http://localhost:8080");
                var config = new HttpSelfHostConfiguration(WEBPAGE);

                config.Routes.MapHttpRoute("Page Default", "{controller}/{id}", new { controller = "Home", id = RouteParameter.Optional });
                config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
                config.MessageHandlers.Add(new FileHandler());
                HttpSelfHostServer server = new HttpSelfHostServer(config);
                server.OpenAsync().Wait();
                WEBPAGE_ON = true;
            }
            catch (Exception ex)
            {
                ex.LogToDebugAndFile();
                WEBPAGE_ON = false;
            }

            try
            {
                //start hub
                HUB = ConfigHelper.GetConfig("hub", "http://*:8888");
                WebApp.Start($"http://*:{new Uri(HUB).Port}");
                HUB_ON = true;
            }
            catch (Exception ex)
            {
                ex.LogToDebugAndFile();
                HUB_ON = false;
            }
        }

        public void Configuration(IAppBuilder app)
        {
            app.UseCors(CorsOptions.AllowAll);
            app.MapSignalR();
        }
    }

    public class FileHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var ext = Path.GetExtension(request.RequestUri.AbsolutePath);
            if (string.IsNullOrWhiteSpace(ext))
            {
                return base.SendAsync(request, cancellationToken);
            }
            return Task.Run<HttpResponseMessage>(() =>
            {
                try
                {
                    var response = request.CreateResponse();
                    var baseFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                    var suffix = request.RequestUri.AbsolutePath.Substring(1);
                    var fullPath = Path.Combine(baseFolder, suffix);
                    response.Content = new StreamContent(new FileStream(fullPath, FileMode.Open));
                    return response;
                }
                catch (Exception ex)
                {
                    ex.LogToDebugAndFile();
                    return new HttpResponseMessage(System.Net.HttpStatusCode.NotFound);
                }
            });
        }
    }
}
