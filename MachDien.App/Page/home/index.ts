const _win = <any>window;
const _jq = _win.$;
const _Vue = _win.Vue;
const _moment = _win.moment;

interface AppData {
    hub: {
        homeHub: any,
        server: IHomeHubCaller,
        state: {
            oldState: eStateHub,
            newState: eStateHub,
        },
    },
    noidung: {
        thongsos: ThongSoTB[],
        receverDatas: string[],
        models: {
            ts: {
                CBDS18B20: number,
                CBDHT11: number,
                CBDHT12: number,
                CBUthap: number,
                CBUcao: number,
                CBIthap: number,
                CBIcao: number,
                CBcospi: number,
                Time: Date,
            },
            gettingSetting: number,
            setting: number,
        }
    }
}

interface IHomeHubCallback {
    callback_join(d: DataTranfer): void;
    callback_capNhatThongSo(d: DataTranfer): void;
    callback_caiDatThongSo(d: DataTranfer): void;
    callback_receiveData(d: DataTranfer): void;

    callback_getSetting(d: DataTranfer): void;
    callback_rep_getSetting(d: DataTranfer): void;
    callback_rep_caiDatThongSo(d: DataTranfer): void;
}

interface IHomeHubCaller {
    join(d: DataTranfer): void;
    capNhatThongSo(d: DataTranfer): void;
    caiDatThongSo(d: DataTranfer): void;
    receiveData(d: DataTranfer): void;

    getSetting(d: DataTranfer): void;
    rep_getSetting(d: DataTranfer): void;
    rep_caiDatThongSo(d: DataTranfer): void;
}


interface DataTranfer {
    success: boolean;
    message: string;
    data: any;

}

interface ThongSoTB {
    DS18B20: string,
    DHT11: string,
    U: string,
    I: string,
    P: string,
    cospi: string,
    canh_bao: string,
    Time: string,
    relay: number
}

interface RelaySetting {
    relay: number,
}

enum eStateHub {
    connecting = 0,
    connected = 1,
    reconnecting = 2,
    disconnected = 4,
}

class App implements IHomeHubCallback {
    callback_rep_caiDatThongSo = (d: DataTranfer) => {
        clearTimeout(this.data.noidung.models.setting);
        this.data.noidung.models.setting = undefined;

        _win.UIkit.modal('#modal-center').hide();
        alert("Cài đặt thông số thành công! " + d.data);
    }

    callback_getSetting(d: DataTranfer): void {
        throw new Error("Method not implemented.");
    }

    callback_rep_getSetting = (d: DataTranfer) => {
        clearTimeout(this.data.noidung.models.gettingSetting);
        this.data.noidung.models.gettingSetting = undefined;
        if (d.success) {
            this.data.noidung.models.ts = {
                CBDS18B20: Number(d.data.CBDS18B20),
                CBDHT11: Number(d.data.CBDHT11),
                CBDHT12: Number(d.data.CBDHT12),
                CBUthap: Number(d.data.CBUthap),
                CBUcao: Number(d.data.CBUcao),
                CBIthap: Number(d.data.CBIthap),
                CBIcao: Number(d.data.CBIcao),
                CBcospi: Number(d.data.CBcospi) / 10,
                Time: d.data.Time,
            };
            _win.UIkit.modal('#modal-center').show();
        } else {
            alert("Lỗi lấy thông số thiết bị. " + d.message);
        }
    }

    callback_receiveData = (d: DataTranfer) => {
        if (d.success) {
            if (this.data.noidung.receverDatas.length > 18) {
                this.data.noidung.receverDatas = [];
            }
            this.data.noidung.receverDatas.push(d.data);
        }
    }
    callback_join = (d: DataTranfer) => {
        console.log(d);
    }
    callback_capNhatThongSo = (d: DataTranfer) => {
        this.data.noidung.thongsos = d.data;
    }
    callback_caiDatThongSo = (d: DataTranfer) => {
        console.log(d);
    }

    el = '#app';
    data = <AppData>{
        hub: {
            homeHub: undefined,
            server: undefined,
            state: undefined
        },
        noidung: {
            thongsos: [{
                DS18B20: "---",
                DHT11: "---",
                U: "---",
                I: "---",
                P: "---",
                cospi: "---",
                canh_bao: undefined,
                Time: undefined,
                relay: 0
            }],
            receverDatas: [],
            models: {
                ts: {
                    CBDS18B20: 50,
                    CBDHT11: 50,
                    CBDHT12: 50,
                    CBUthap: 50,
                    CBUcao: 50,
                    CBIthap: 50,
                    CBIcao: 50,
                    CBcospi: 50,
                    Time: undefined,
                },
                gettingSetting: 0,
                setting: 0,
            }
        }
    };
    created = () => {

        //connect hub
        const me = this;
        _jq.connection.hub.url = `${_win.HUB_URL}/signalr`;
        this.data.hub.homeHub = _jq.connection.homeHub;
        this.data.hub.server = this.data.hub.homeHub.server;

        this.data.hub.homeHub.client.callback_rep_caiDatThongSo = this.callback_rep_caiDatThongSo;
        this.data.hub.homeHub.client.callback_rep_getSetting = this.callback_rep_getSetting;
        this.data.hub.homeHub.client.callback_join = this.callback_join;
        this.data.hub.homeHub.client.callback_capNhatThongSo = this.callback_capNhatThongSo;
        this.data.hub.homeHub.client.callback_receiveData = this.callback_receiveData;

        _jq.connection.hub.logging = true;
        _jq.connection.hub.stateChanged(function (state: any) {
            me.data.hub.state = state;
            console.log(`${eStateHub[me.data.hub.state.oldState]} -> ${eStateHub[me.data.hub.state.newState]}`);
        });
        _jq.connection.hub.start().done((d: any) => {
            me.data.hub.server.join(<DataTranfer>{
                data: {
                    name: "web"
                },
            });
        });
    };

    methods = {
        caithongso: () => {
            if (confirm(`Bạn muốn thay đổi cài đặt?`)) {
                this.data.hub.server.caiDatThongSo(<DataTranfer>{
                    data: this.computed.settingToSend()
                });
                this.data.noidung.models.setting = setTimeout(() => {
                    if (this.data.noidung.models.setting) {
                        alert("Thiết lập thông số thiết bị thất bại!");
                    }
                }, 15000);
            }
        },
        getsetting: () => {
            this.data.hub.server.getSetting(<DataTranfer>{
                data: "SETTING"
            });
            this.data.noidung.models.gettingSetting = setTimeout(() => {
                if (this.data.noidung.models.gettingSetting) {
                    alert("Lấy thông số cài đặt từ thiết bị thất bại!");
                }
            }, 15000);
        },
    };

    filters = {
        format_datetime: (d, format = "HH:mm:ss DD/MM/YYYY") => {
            if (d) {
                return _moment(d).format(format);
            }
            return '---';
        },
        on_off: (d) => {
            return d == 0 ? "OFF" : d == 1 ? "ON" : '---';
        }
    }

    padstring = (d: string) => {
        if (d == 'CBcospi') {
            return (this.data.noidung.models.ts[d] * 10).toString().padStart(3, '0');
        }
        return this.data.noidung.models.ts[d].toString().padStart(3, '0');
    };

    computed = {
        settingToSend: () => {
            if (this.data.noidung.models.ts) {
                //'DA|DS18B20|DHT11|DHT12|Uthap|Ucao|Ithap|Icao|cospi';
                return `DA|${this.padstring('CBDS18B20')}|${this.padstring('CBDHT11')}|${this.padstring('CBDHT12')}|${this.padstring('CBUthap')}|${this.padstring('CBUcao')}|${this.padstring('CBIthap')}|${this.padstring('CBIcao')}|${this.padstring('CBcospi')}`;
            }
            return '';
        }
    }
}

const _vm = new _Vue(new App());
