using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MachDien.App
{
    public static class ControlFormHelper
    {
        public static void InvokeHelper<T>(this T control, Action<T> action) where T : Control
        {
            if (COMMON.APPRUNING && control != null && control.Disposing == false && control.IsHandleCreated && control.IsDisposed == false)
            {
                if (control.InvokeRequired)
                {
                    control.Invoke(new MethodInvoker(() =>
                    {
                        action.Invoke(control);
                    }));
                }
                else
                {
                    action?.Invoke(control);
                }
            }
        }
    }
}
