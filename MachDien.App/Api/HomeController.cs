using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace MachDien.App.Api
{
    public class HomeController : ApiController
    {
        public HttpResponseMessage Get()
        {
            var page = "Page/home/index.html";
            return WebHelper.CreateReponsePage(File.ReadAllText(page));
        }
    }
}
