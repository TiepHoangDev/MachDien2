using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachDien.App.Models
{
    public class ThongSoTB
    {
        public string DS18B20 { get; set; }
        public string DHT11 { get; set; }
        public string DHT12 { get; set; }
        public string U { get; set; }
        public string I { get; set; }
        public string P { get; set; }
        public string cospi { get; set; }
        public string canh_bao { get; set; }
        public DateTime Time { get; set; } = DateTime.Now;
        public int relay { get; set; }
    }

    public class ThongSoSetting
    {
        public string CBDS18B20 { get; set; }
        public string CBDHT11 { get; set; }
        public string CBDHT12 { get; set; }
        public string CBUthap { get; set; }
        public string CBUcao { get; set; }
        public string CBIthap { get; set; }
        public string CBIcao { get; set; }
        public string CBcospi { get; set; }
        public DateTime Time { get; set; } = DateTime.Now;
    }

    public class RelaySetting
    {
        public int relay { get; set; }
    }
}
