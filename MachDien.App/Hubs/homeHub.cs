using ClassLibraryHelper;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachDien.App.Hubs
{
    public class homeHub : Hub<IHomeHubCallback>, IHomeHubCaller
    {
        public void caiDatThongSo(DataTranfer d)
        {
            Clients.All.callback_caiDatThongSo(d);
        }

        public void capNhatThongSo(DataTranfer d)
        {
            Clients.All.callback_capNhatThongSo(d);
        }

        public void getSetting(DataTranfer d)
        {
            Clients.All.callback_getSetting(d);
        }

        public void join(DataTranfer d)
        {
            Clients.Caller.callback_join(d);
        }

        public void receiveData(DataTranfer d)
        {
            Clients.All.callback_receiveData(d);
        }

        public void rep_caiDatThongSo(DataTranfer d)
        {
            Clients.All.callback_rep_caiDatThongSo(d);
        }

        public void rep_getSetting(DataTranfer d)
        {
            Clients.All.callback_rep_getSetting(d);
        }
    }

    public interface IHomeHubCallback
    {
        void callback_join(DataTranfer d);

        void callback_capNhatThongSo(DataTranfer d);
        void callback_caiDatThongSo(DataTranfer d);

        void callback_receiveData(DataTranfer d);

        void callback_getSetting(DataTranfer d);
        void callback_rep_getSetting(DataTranfer d);
     
        void callback_rep_caiDatThongSo(DataTranfer d);
    }

    public interface IHomeHubCaller
    {
        void join(DataTranfer d);

        void capNhatThongSo(DataTranfer d);
        void caiDatThongSo(DataTranfer d);

        void receiveData(DataTranfer d);

        void getSetting(DataTranfer d);
        void rep_getSetting(DataTranfer d);

        void rep_caiDatThongSo(DataTranfer d);
    }

    public class DataTranfer
    {
        public bool success { get; set; }
        public string message { get; set; }
        public object data { get; set; }

        public static implicit operator DataTranfer(Func<object> data)
        {
            try
            {
                return DataTranfer.From(data.Invoke());
            }
            catch (Exception ex)
            {
                ex.LogToFile();
                return ex;
            }
        }

        public static implicit operator DataTranfer(Exception ex)
        {
            return DataTranfer.From(ex, ex.Message, false);
        }

        public static DataTranfer From(object data, string message = null, bool success = true)
        {
            return new DataTranfer
            {
                data = data,
                message = message,
                success = success
            };
        }
    }
}
