using CefSharp;
using CefSharp.WinForms;
using ClassLibraryHelper;
using MachDien.App.Hubs;
using MachDien.App.Models;
using Microsoft.AspNet.SignalR.Client;
using Newtonsoft.Json;
using NHotkey.WindowsForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace MachDien.App
{
    public partial class Form1 : Form
    {
        private int tcpServerPort;
        private TcpListener _tcpListener;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
            Visible = false;
            Opacity = Visible ? 100 : 0;

            string key = ConfigHelper.GetConfig("Shortkey", "B");
            Keys keys = Keys.B;
            if (Enum.TryParse(key, out Keys keys1))
            {
                keys = keys1;
            }
            HotkeyManager.Current.AddOrReplace("Increment", Keys.Control | Keys.Alt | keys, (s, ee) =>
            {
                Visible = !Visible;
                Opacity = Visible ? 100 : 0;
                WindowState = Visible ? FormWindowState.Maximized : FormWindowState.Minimized;
            });

            label_hub.Text = Api.Startup.HUB;
            label_webpage.Text = Api.Startup.WEBPAGE;

            pictureBox_webpage.Image = Api.Startup.WEBPAGE_ON ? Properties.Resources.dot_green_50 : Properties.Resources.dot_red_50;
            pictureBox_hub.Image = Api.Startup.HUB_ON ? Properties.Resources.dot_green_50 : Properties.Resources.dot_red_50;


            tcpServerPort = ConfigHelper.GetConfig("tcpServerPort", 55);
            _tryListenServerTCP();

            Cef.Initialize(new CefSettings());
            var browser = new ChromiumWebBrowser(label_webpage.Text);
            panel_web.Controls.Add(browser);
            browser.Dock = DockStyle.Fill;
        }


        static bool TCPSErver_ON = false;
        private HubConnection _hubConnection_homeHub;
        private IHubProxy _proxy_homeHub;
        private StateChange _ConnectionState_homeHub;
        static Dictionary<EndPoint, ClientInfo> Clients = new Dictionary<EndPoint, ClientInfo>();

        private async void _tryListenServerTCP()
        {
            try
            {
                TCPSErver_ON = false;
                _tcpListener?.Stop();
                await Task.Delay(2000);
                _tcpListener = new TcpListener(IPAddress.Any, tcpServerPort);
                _tcpListener.Start();
                _log($"SERVER STARTED IN {_tcpListener.LocalEndpoint}");
                TCPSErver_ON = true;
                var t = Task.Run(() =>
                {
                    while (TCPSErver_ON)
                    {
                        try
                        {
                            var client = _tcpListener.AcceptTcpClient();
                            var stream = client.GetStream();
                            var ci = new ClientInfo
                            {
                                Client = client,
                                Reader = new StreamReader(stream),
                                Writer = new StreamWriter(stream) { AutoFlush = true },
                            };
                            if (Clients.ContainsKey(client.Client.RemoteEndPoint))
                            {
                                Clients[client.Client.RemoteEndPoint] = ci;
                            }
                            else
                            {
                                Clients.Add(client.Client.RemoteEndPoint, ci);
                            }
                            _log($"{client.Client.RemoteEndPoint} CONNECTED. Client{Clients.Count}");
                            Task.Run(() =>
                            {
                                while (client.Connected)
                                {
                                    try
                                    {
                                        var line = ci.Reader.ReadLine();
                                        _log(line);
                                        try
                                        {
                                            var now = DateTime.Now;
                                            receiveData(DataTranfer.From($"{now}>>{client.Client.RemoteEndPoint}>>\"{line}\""));
                                            if (line.StartsWith("{\"CBDS18B20\""))
                                            {
                                                var data = JsonConvert.DeserializeObject<ThongSoSetting>(line);
                                                data.Time = now;
                                                rep_getSetting(DataTranfer.From(data));
                                            }
                                            else if (line == "OK")
                                            {
                                                rep_caiDatThongSo(DataTranfer.From(line));
                                            }
                                            else
                                            {
                                                var data = JsonConvert.DeserializeObject<ThongSoTB>(line);
                                                data.Time = now;
                                                capNhatThongSo(DataTranfer.From(new[] { data }));
                                            }
                                            ci.Writer.Write("ok");
                                        }
                                        catch (Exception e2)
                                        {
                                            ci.Writer.Write("json ko hop le. " + e2.Message);
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        _log(ex.Message);
                                        break;
                                    }
                                }
                                _log($"{client.Client.RemoteEndPoint} disconnected");
                                if (Clients.ContainsKey(client.Client.RemoteEndPoint))
                                {
                                    var ep = client.Client.RemoteEndPoint;
                                    Clients[ep]?.Client?.Dispose();
                                    Clients.Remove(ep);
                                }
                            });
                        }
                        catch (Exception ex)
                        {
                            _log(ex);
                        }

                    }
                });
                label_tcpServer.Text = $"{_tcpListener?.LocalEndpoint}";
                pictureBox_tcp.Image = Properties.Resources.dot_green_50;
            }
            catch (Exception ex)
            {
                ex.LogToDebugAndFile();
                TCPSErver_ON = false;
                label_tcpServer.Text = $"*:{tcpServerPort}";
                pictureBox_tcp.Image = Properties.Resources.dot_red_50;
                await Task.Delay(5000);
                _tryListenServerTCP();
            }
        }

        private void _log(object msg)
        {
            richTextBox1?.InvokeHelper(c =>
            {
                if (c.TextLength > 50000)
                {
                    c.Clear();
                }
                c.AppendText($"\n{DateTime.Now.ToString("HH:mm:ss")}>>{msg}");
            });
        }

        readonly Microsoft.AspNet.SignalR.Client.ConnectionState?[] stateNeedReconnect = new Microsoft.AspNet.SignalR.Client.ConnectionState?[] { null, Microsoft.AspNet.SignalR.Client.ConnectionState.Disconnected };

        public bool isNeedReconnect()
        {
            return stateNeedReconnect.Contains(_ConnectionState_homeHub?.NewState);
        }

        public bool tryEnsureConnected()
        {
            try
            {
                if (_hubConnection_homeHub == null || _proxy_homeHub == null)
                {
                    var url = Api.Startup.HUB;
                    _log($"tryEnsureConnected _hubConnection_homeHub {url} ...");
                    if (_hubConnection_homeHub?.ConnectionId != null)
                    {
                        _hubConnection_homeHub?.Stop();
                    }
                    _hubConnection_homeHub = new HubConnection(url);
                    _proxy_homeHub = _hubConnection_homeHub.CreateHubProxy("homeHub");
                    _hubConnection_homeHub.Closed += async () =>
                     {
                         _log("_hubConnection_homeHub Closed");
                         if (isNeedReconnect())
                         {
                             while (!tryEnsureConnected() && COMMON.APPRUNING)
                             {
                                 await Task.Delay(10000);
                                 tryEnsureConnected();
                             }
                         }
                     };
                    _hubConnection_homeHub.Error += (ex) =>
                    {
                        _log(ex);
                    };
                    _hubConnection_homeHub.StateChanged += async (tc) =>
                     {
                         _log($"_hubConnection_homeHub: {_ConnectionState_homeHub?.OldState} -> {_ConnectionState_homeHub?.NewState}");
                         _ConnectionState_homeHub = tc;
                         if (isNeedReconnect())
                         {
                             while (!tryEnsureConnected() && COMMON.APPRUNING)
                             {
                                 await Task.Delay(10000);
                                 tryEnsureConnected();
                             }
                         }
                     };
                    _registerEventHub();
                    _hubConnection_homeHub.Start();
                }
                return _ConnectionState_homeHub?.NewState == Microsoft.AspNet.SignalR.Client.ConnectionState.Connected;
            }
            catch (Exception ex)
            {
                _log(ex);
                ex.LogToDebugAndFile();
                _hubConnection_homeHub?.Stop();
                _hubConnection_homeHub = null;
                _proxy_homeHub = null;
                return false;
            }
        }

        private void _registerEventHub()
        {
            _proxy_homeHub.On<DataTranfer>(nameof(callback_caiDatThongSo), callback_caiDatThongSo);
            _proxy_homeHub.On<DataTranfer>(nameof(callback_join), callback_join);
            _proxy_homeHub.On<DataTranfer>(nameof(callback_getSetting), callback_getSetting);
        }

        public void TryCallServer(string method, DataTranfer data)
        {
            try
            {
                if (tryEnsureConnected())
                {
                    _proxy_homeHub.Invoke<DataTranfer>(method, data);
                }
            }
            catch (Exception ex)
            {
                _log(ex);
                ex.LogToDebugAndFile();
            }
        }

        private void label_webpage_Click(object sender, EventArgs e)
        {
            Process.Start(label_webpage.Text);
        }
    }
}
