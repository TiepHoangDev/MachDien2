using ClassLibraryHelper;
using MachDien.App.Hubs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachDien.App
{
    public partial class Form1 : IHomeHubCallback, IHomeHubCaller
    {
        public void caiDatThongSo(DataTranfer d)
        {
            throw new NotImplementedException();
        }

        public void callback_caiDatThongSo(DataTranfer d)
        {
            _log("callback_caiDatThongSo: " + d.ToJson());
            if (Clients?.Any() == true)
            {
                foreach (var item in Clients)
                {
                    item.Value.Writer.WriteLine(d.data + "");
                    item.Value.Writer.Flush();
                }
            }
        }

        public void callback_capNhatThongSo(DataTranfer d)
        {
            throw new NotImplementedException();
        }

        public void callback_getSetting(DataTranfer d)
        {
            _log("callback_getSetting: " + d.ToJson());
            if (Clients?.Any() == true)
            {
                foreach (var item in Clients)
                {
                    item.Value.Writer.WriteLine(d.data + "");
                    item.Value.Writer.Flush();
                }
            }
        }

        public void callback_join(DataTranfer d)
        {
            _log("success callback_join");
        }

        public void callback_receiveData(DataTranfer d)
        {
            throw new NotImplementedException();
        }

        public void callback_rep_caiDatThongSo(DataTranfer d)
        {
            throw new NotImplementedException();
        }

        public void callback_rep_getSetting(DataTranfer d)
        {
            throw new NotImplementedException();
        }

        public void capNhatThongSo(DataTranfer d)
        {
            TryCallServer("capNhatThongSo", d);
        }

        public void getSetting(DataTranfer d)
        {
            throw new NotImplementedException();
        }

        public void join(DataTranfer d)
        {
            TryCallServer("join", d);
        }

        public void receiveData(DataTranfer d)
        {
            TryCallServer("receiveData", d);
        }

        public void rep_caiDatThongSo(DataTranfer d)
        {
            TryCallServer("rep_caiDatThongSo", d);
        }

        public void rep_getSetting(DataTranfer d)
        {
            TryCallServer("rep_getSetting", d);
        }
    }
}
