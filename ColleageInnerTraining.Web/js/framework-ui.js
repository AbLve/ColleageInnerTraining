
$.fn.formSerialize = function (formdate) {
    var element = $(this);
    if (!!formdate) {
        for (var key in formdate) {
            var $id = element.find('#' + key);
            var value = $.trim(formdate[key]).replace(/&nbsp;/g, '');
            var type = $id.attr('type');
            if ($id.hasClass("select2-hidden-accessible")) {
                type = "select";
            }
            switch (type) {
                case "checkbox":
                    if (value == "1") {
                        $id.attr("checked", 'checked');
                    } else {
                        $id.removeAttr("checked");
                    }
                    break;
                case "select":
                    $id.val(value).trigger("change");
                    break;
                default:
                    $id.val(value);
                    break;
            }
        };
        return false;
    }
    var postdata = {};
    element.find('input,select,textarea').each(function (r) {
        var $this = $(this);
        var id = $this.attr('id');
        var type = $this.attr('type');
        switch (type) {
            case "checkbox":
                postdata[id] = $(this).prop("checked") == true ? 1 : 0;
                break;
            default:
                //  var value = $this.val() == "" ? "&nbsp;" : $this.val();
                // if (!$.request("keyValue")) {
                //    value = value.replace(/&nbsp;/g, '');
                // }
                var value = $this.val();
                postdata[id] = value;
                break;
        }
        if (id.indexOf("Content") >= 0) {
            postdata[id] = encodeURI($this.val());
        }
    });
    if ($('[name=__RequestVerificationToken]').length > 0) {
        postdata["__RequestVerificationToken"] = $('[name=__RequestVerificationToken]').val();
    }
    return postdata;
}

$.fn.bindSelect = function (options) {
    var defaults = {
        id: "id",
        text: "text",
        search: false,
        url: "",
        param: [],
        change: null
    };
    var options = $.extend(defaults, options);
    var $element = $(this);
    if (options.url != "") {
        $.ajax({
            url: options.url,
            data: options.param,
            dataType: "json",
            async: false,
            success: function (data) {
                $.each(data, function (i) {
                    $element.append($("<option></option>").val(data[i][options.id]).html(data[i][options.text]));
                });
                //$element.select2({
                //    minimumResultsForSearch: options.search == true ? 0 : -1
                //});
                //$element.on("change", function (e) {
                //    if (options.change != null) {
                //        options.change(data[$(this).find("option:selected").index()]);
                //    }
                //    $("#select2-" + $element.attr('id') + "-container").html($(this).find("option:selected").text().replace(/　　/g, ''));
                //});
            }
        });
    }
    //else {
    //    $element.select2({
    //        minimumResultsForSearch: -1
    //    });
    //}
}

$.fn.Initdate = function (sId, eId) {
    $(this).daterangepicker({
        applyClass: 'btn-sm btn-success',
        cancelClass: 'btn-sm btn-default',
        locale: {
            applyLabel: '确认',
            cancelLabel: '取消',
            fromLabel: '起始时间',
            toLabel: '结束时间',
            customRangeLabel: '自定义',
            firstDay: 1
        },
        ranges: {
            //'最近1小时': [moment().subtract('hours',1), moment()],
            //'今日': [moment().startOf('day'), moment()],
            //'昨日': [moment().subtract('days', 1).startOf('day'), moment().subtract('days', 1).endOf('day')],
            '最近7日': [moment().subtract('days', 6), moment()],
            '最近30日': [moment().subtract('days', 29), moment()],
            '本月': [moment().startOf("month"), moment().endOf("month")],
            '上个月': [moment().subtract(1, "month").startOf("month"), moment().subtract(1, "month").endOf("month")]
        },
        opens: 'right',    // 日期选择框的弹出位置
        separator: ' 至 ',
        showWeekNumbers: true,     // 是否显示第几周


        //timePicker: true,
        //timePickerIncrement : 10, // 时间的增量，单位为分钟
        //timePicker12Hour : false, // 是否使用12小时制来显示时间


        //maxDate : moment(),           // 最大时间
        format: 'YYYY-MM-DD'

    }, function (start, end, label) { // 格式化日期显示框
        $('#' + sId).val(start.format('YYYY-MM-DD'));
        $('#' + eId).val(end.format('YYYY-MM-DD'));
    })
    .next().on('click', function () {
        $(this).prev().focus();
    });
}

$.fn.FormatStr = function (fmt) {
    var e = this.val();
    if (!e) {
        return;
    }
    var date = new Date(e.replace(RegExp("-", "g"), "/"));
    var o = {
        "M+": date.getMonth() + 1, //月份 
        "d+": date.getDate(), //日 
        "h+": date.getHours(), //小时 
        "m+": date.getMinutes(), //分 
        "s+": date.getSeconds(), //秒 
        "q+": Math.floor((date.getMonth() + 3) / 3), //季度 
        "S": date.getMilliseconds() //毫秒 
    };
    if (/(y+)/.test(fmt)) fmt = fmt.replace(RegExp.$1, (date.getFullYear() + "").substr(4 - RegExp.$1.length));
    for (var k in o)
        if (new RegExp("(" + k + ")").test(fmt)) fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
    this.val(fmt);
}

$.fn.InitTable = function () {
    if ($(this).find('tbody').children("tr").length === 1) {
        var thlength = $(this).find('tbody').children("tr").find('th').length;
        $(this).append("<tr><td colspan='" + thlength + "' style='color:red'>检索不到数据！</td></tr>");
    }
    $.loading(false);
}

$.request = function (name) {
    var search = location.search.slice(1);
    var arr = search.split("&");
    for (var i = 0; i < arr.length; i++) {
        var ar = arr[i].split("=");
        if (ar[0] == name) {
            if (unescape(ar[1]) == 'undefined') {
                return "";
            } else {
                return unescape(ar[1]);
            }
        }
    }
    return "";
}

$.reload = function () {
    location.reload();
    return false;
}

$.getUrlParam = function (name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) return unescape(r[2]); return null;
}

$.submitForm = function (options) {
    var defaults = {
        url: "",
        param: [],
        loading: "正在提交数据...",
        success: null,
        close: true
    };
    var options = $.extend(defaults, options);
    //$.loading(true, options.loading);
    window.setTimeout(function () {
        if ($('[name=__RequestVerificationToken]').length > 0) {
            options.param["__RequestVerificationToken"] = $('[name=__RequestVerificationToken]').val();
        }
        $.ajax({
            url: options.url,
            data: options.param,
            type: "post",
            dataType: "json",
            success: function (data) {
                if (data.code == 200) {
                    options.success(data);
                    $.modalMsg(data.msg, 'success');
                } else {
                    $.modalAlert(data.msg, 'error');
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                $.loading(false);
                $.modalMsg(errorThrown, "error");
            },
            //beforeSend: function () {
            //    $.loading(true, options.loading);
            //},
            complete: function () {
                $.loading(false);
            }
        });
    }, 500);
}

$.loading = function (bool, text) {
    var $loadingpage = $("#loadingPage");
    var $loadingtext = $loadingpage.find('.loading-content');
    if (bool) {
        $loadingpage.show();
    } else {
        if ($loadingtext.attr('istableloading') == undefined) {
            $loadingpage.hide();
        }
    }
    if (!!text) {
        $loadingtext.html(text);
    } else {
        $loadingtext.html("数据加载中，请稍后…");
    }
    $loadingtext.css("left", ($('body').width() - $loadingtext.width()) / 2 - 50);
    $loadingtext.css("top", ($('body').height() - $loadingtext.height()) / 2);
}

$.modalAlert = function (content, type) {
    var icon = "";
    if (type == 'success') {
        icon = "fa-check-circle";
    }
    if (type == 'error') {
        icon = "fa-times-circle";
    }
    if (type == 'warning') {
        icon = "fa-exclamation-circle";
    }
    layer.alert(content, {
        icon: icon,
        title: "系统提示",
        btn: ['确认'],
        btnclass: ['btn btn-primary']
    });
}

$.modalConfirm = function (content, callBack) {
    layer.confirm(content, {
        icon: "fa-exclamation-circle",
        title: "系统提示",
        btn: ['确认', '取消'],
        btnclass: ['btn btn-primary', 'btn btn-danger'],
    }, function () {
        callBack(true);
    }, function () {
        callBack(false)
    });
}

$.deleteForm = function (options) {
    var defaults = {
        prompt: "注：您确定要删除该项数据吗？",
        url: "",
        param: [],
        type: "",
        loading: "正在删除数据...",
        success: null,
        close: true
    };
    var options = $.extend(defaults, options);
    if ($('[name=__RequestVerificationToken]').length > 0) {
        options.param["__RequestVerificationToken"] = $('[name=__RequestVerificationToken]').val();
    }
    $.modalConfirm(options.prompt, function (r) {
        if (r) {
            $.loading(true, options.loading);
            window.setTimeout(function () {
                $.ajax({
                    url: options.url,
                    data: options.param,
                    type: options.type,
                    dataType: "json",
                    success: function (data) {
                        if (data.code == 200) {
                            options.success(data);
                            $.modalMsg(data.msg, data.type);
                        } else {
                            $.modalMsg("操作失败");
                            $.modalAlert(data.msg, data.type);
                        }
                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        $.loading(false);
                        $.modalMsg(errorThrown, "error");
                    },
                    complete: function () {
                        $.loading(false);
                    }
                });
            }, 500);
        }
    });

}

$.modalMsg = function (content, type) {
    if (type != undefined) {
        var icon = "";
        if (type == 'success') {
            icon = "fa-check-circle";
        }
        if (type == 'error') {
            icon = "fa-times-circle";
        }
        if (type == 'warning') {
            icon = "fa-exclamation-circle";
        }
        layer.msg(content, { icon: icon, time: 2000, shift: 5 });
        $(".layui-layer-msg").find('i.' + icon).parents('.layui-layer-msg').addClass('layui-layer-msg-' + type);
    } else {
        layer.msg(content);
    }
}

$.modalClose = function () {
    var index = layer.getFrameIndex(window.name); //先得到当前iframe层的索引
    var $IsdialogClose = $("#layui-layer" + index).find('.layui-layer-btn').find("#IsdialogClose");
    var IsClose = $IsdialogClose.is(":checked");
    if ($IsdialogClose.length == 0) {
        IsClose = true;
    }
    if (IsClose) {
        layer.close(index);
    } else {
        location.reload();
    }
}

$.modalOpen = function (options) {
    var defaults = {
        id: null,
        title: '系统窗口',
        width: "100px",
        height: "100px",
        url: '',
        shade: 0.3,
        btn: ['确认', '关闭'],
        btnclass: ['btn btn-primary', 'btn btn-danger'],
        callBack: null
    };
    var options = $.extend(true, defaults, options);
    var _width = $(window).width() > parseInt(options.width.replace('px', '')) ? options.width : $(window).width() + 'px';
    var _height = $(window).height() > parseInt(options.height.replace('px', '')) ? options.height : $(window).height() + 'px';
    //alert(_width+"  , "+_height);
    // alert(options.id);
    layer.open({
        id: options.id,
        type: 2,
        shade: options.shade,
        title: options.title,
        fix: false,
        area: [_width, _height],
        content: options.url,
        btn: options.btn,
        btnclass: options.btnclass,
        yes: function () {
            options.callBack(options.id)
        },
        cancel: function () {
            return true;
        }

    });
}

$.StringFormat = function () {
    if (arguments.length == 0)
        return null;
    var str = arguments[0];
    for (var i = 1; i < arguments.length; i++) {
        var re = new RegExp('\\{' + (i - 1) + '\\}', 'gm');
        str = str.replace(re, arguments[i]);
    }
    return str;
}

$.UpLoad = function (e) {
    var filename = $(e).val().replace(/.*(\/|\\)/, "");
    var fileExt = (/[.]/.exec(filename)) ? /[^.]+$/.exec(filename.toLowerCase()) : '';
    var regex = /word|pdf|excl|zip|doc|docx|xls|xlsc|rar|ppt/;
    layer.confirm(
                '确定上传文件?', {
                    btn: ['确定', '取消'],
                    btnclass: ['btn btn-primary', 'btn btn-danger'],
                },
                function (index) {
                    if (regex.test(fileExt)) {
                        $(e).next().click();
                    }
                    else {
                        $.modalMsg("请检查文件格式是否正确");
                    }
                }, function () { layer.close() });
}

$.IsOver = function (list) {
    var nary = list.sort();
    for (var i = 0; i < list.length; i++) {
        if (nary[i] == nary[i + 1]) {
            alert("重复名称：" + nary[i]);
            return;
        }
    }
}

// 对Date的扩展，将 Date 转化为指定格式的String
// 月(M)、日(d)、小时(h)、分(m)、秒(s)、季度(q) 可以用 1-2 个占位符， 
// 年(y)可以用 1-4 个占位符，毫秒(S)只能用 1 个占位符(是 1-3 位的数字) 
// 例子： 
// (new Date()).Format("yyyy-MM-dd hh:mm:ss.S") ==> 2006-07-02 08:09:04.423 
// (new Date()).Format("yyyy-M-d h:m:s.S")      ==> 2006-7-2 8:9:4.18 
Date.prototype.Format = function (fmt) {
    var o = {
        "M+": this.getMonth() + 1, //月份 
        "d+": this.getDate(), //日 
        "h+": this.getHours(), //小时 
        "m+": this.getMinutes(), //分 
        "s+": this.getSeconds(), //秒 
        "q+": Math.floor((this.getMonth() + 3) / 3), //季度 
        "S": this.getMilliseconds() //毫秒 
    };
    if (/(y+)/.test(fmt)) fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    for (var k in o)
        if (new RegExp("(" + k + ")").test(fmt)) fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
    return fmt;
}
/*时间格式化*/
String.prototype.toDate = function () {
    return new Date(this.replace(RegExp("-", "g"), "/"));
}

String.prototype.getParam = function (name) {
    var element = this;
    var arr = element.split("&");
    for (var i = 0; i < arr.length; i++) {
        var ar = arr[i].split("=");
        if (ar[0] == name) {
            if (unescape(ar[1]) == 'undefined') {
                return "";
            } else {
                return unescape(ar[1]);
            }
        }
    }
    return "";
}


var commonJs = function () {
    return {
        cball: function (e) {
            $('[name=cbsingle]:checkbox').prop('checked', $(e).prop('checked'));
        },
        go: function (url) {
            location.href = url;
        },
        getIds: function () {
            var ids = "";
            $('[name=cbsingle]:checked').each(function () {
                ids += $(this).attr("realid") + ",";
            });
            return ids;
        },
        //预览图片
        preview: function (e, imgid) {
            if (e.files && e.files[0]) {
                var reader = new FileReader();
                reader.onload = function (evt) {
                    $("#" + imgid).attr("src", evt.target.result);
                }
                reader.readAsDataURL(e.files[0]);
            }
        }
    }
}()

$.extend({
    //转义HTML加密
    htmlencode: function (value) {
        return $('<div/>').text(value).html();
    },
    //转义HTML解密
    htmldecode: function (value) {
        return $('<div/>').html(value).text();
    }
})

