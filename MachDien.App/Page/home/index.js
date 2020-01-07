const _win = window;
const _jq = _win.$;
const _Vue = _win.Vue;
const _moment = _win.moment;
var eStateHub;
(function (eStateHub) {
    eStateHub[eStateHub["connecting"] = 0] = "connecting";
    eStateHub[eStateHub["connected"] = 1] = "connected";
    eStateHub[eStateHub["reconnecting"] = 2] = "reconnecting";
    eStateHub[eStateHub["disconnected"] = 4] = "disconnected";
})(eStateHub || (eStateHub = {}));
class App {
    constructor() {
        this.callback_rep_caiDatThongSo = (d) => {
            clearTimeout(this.data.noidung.models.setting);
            this.data.noidung.models.setting = undefined;
            _win.UIkit.modal('#modal-center').hide();
            alert("Cài đặt thông số thành công! " + d.data);
        };
        this.callback_rep_getSetting = (d) => {
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
            }
            else {
                alert("Lỗi lấy thông số thiết bị. " + d.message);
            }
        };
        this.callback_receiveData = (d) => {
            if (d.success) {
                if (this.data.noidung.receverDatas.length > 18) {
                    this.data.noidung.receverDatas = [];
                }
                this.data.noidung.receverDatas.push(d.data);
            }
        };
        this.callback_join = (d) => {
            console.log(d);
        };
        this.callback_capNhatThongSo = (d) => {
            this.data.noidung.thongsos = d.data;
        };
        this.callback_caiDatThongSo = (d) => {
            console.log(d);
        };
        this.el = '#app';
        this.data = {
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
        this.created = () => {
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
            _jq.connection.hub.stateChanged(function (state) {
                me.data.hub.state = state;
                console.log(`${eStateHub[me.data.hub.state.oldState]} -> ${eStateHub[me.data.hub.state.newState]}`);
            });
            _jq.connection.hub.start().done((d) => {
                me.data.hub.server.join({
                    data: {
                        name: "web"
                    },
                });
            });
        };
        this.methods = {
            caithongso: () => {
                if (confirm(`Bạn muốn thay đổi cài đặt?`)) {
                    this.data.hub.server.caiDatThongSo({
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
                this.data.hub.server.getSetting({
                    data: "SETTING"
                });
                this.data.noidung.models.gettingSetting = setTimeout(() => {
                    if (this.data.noidung.models.gettingSetting) {
                        alert("Lấy thông số cài đặt từ thiết bị thất bại!");
                    }
                }, 15000);
            },
        };
        this.filters = {
            format_datetime: (d, format = "HH:mm:ss DD/MM/YYYY") => {
                if (d) {
                    return _moment(d).format(format);
                }
                return '---';
            },
            on_off: (d) => {
                return d == 0 ? "OFF" : d == 1 ? "ON" : '---';
            }
        };
        this.padstring = (d) => {
            if (d == 'CBcospi') {
                return (this.data.noidung.models.ts[d] * 10).toString().padStart(3, '0');
            }
            return this.data.noidung.models.ts[d].toString().padStart(3, '0');
        };
        this.computed = {
            settingToSend: () => {
                if (this.data.noidung.models.ts) {
                    return `DA|${this.padstring('CBDS18B20')}|${this.padstring('CBDHT11')}|${this.padstring('CBDHT12')}|${this.padstring('CBUthap')}|${this.padstring('CBUcao')}|${this.padstring('CBIthap')}|${this.padstring('CBIcao')}|${this.padstring('CBcospi')}`;
                }
                return '';
            }
        };
    }
    callback_getSetting(d) {
        throw new Error("Method not implemented.");
    }
}
const _vm = new _Vue(new App());
