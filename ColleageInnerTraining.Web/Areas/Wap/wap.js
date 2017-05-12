    var commSetting = {
    appkey: null,
    sTimestamp: null,
    sign: null,
    userId: null,
    userName: null,
    departmentId: null,
    apiUrl: null,
    loadimg:""
    }
    //载入参数
    function loadSetting(callback) {
        $.ajax({
            type: 'GET',
            url: abp.appPath + 'Wap/commSetting?v=1',
            data: {},
            success: function (result) {
                var data = JSON.stringify(result)
                commSetting.apiUrl = result.apiUrl;
                commSetting.sTimestamp = result.timestamp;
                commSetting.appkey = result.appkey;
                commSetting.sign = result.sign;
                if (result.userId != null) {
                    commSetting.userId = result.userId;
                    commSetting.userName = result.userName;
                    commSetting.departmentId = result.departId;
                    callback();
                }
                else
                    location.href = abp.appPath + 'Account/LogOut'
            }
        });
    }
    var MaxPageSize = 65535;
