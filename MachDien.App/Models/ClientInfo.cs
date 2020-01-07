using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MachDien.App.Models
{
    public class ClientInfo
    {
        public TcpClient Client { get; set; }
        public StreamReader Reader { get; set; }
        public StreamWriter Writer { get; set; }
    }
}
