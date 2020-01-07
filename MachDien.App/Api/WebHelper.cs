using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MachDien.App.Api
{
    public static class WebHelper
    {
        public static HttpResponseMessage CreateReponsePage(string content)
        {
            var response = new HttpResponseMessage();
            response.Content = new StringContent(content);
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");
            return response;
        }
    }
}
