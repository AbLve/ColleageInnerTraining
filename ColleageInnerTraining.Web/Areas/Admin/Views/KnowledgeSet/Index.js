var pIndex = 1;

//隐藏大与2的项
$('#Type option:gt(2)').hide();

//类型下拉框事件
function change() {
    var type = $("#Type").val();
    if (type == 0) {
        $("#Department").show();
        $("#job").addClass("hidden");
    }
    else {
        $("#Department").hide();
        $("#job").removeClass("hidden");
    }
}

//设置
function set() {
    var kId = $("#kId").val();
    var d = $("#Department :selected").val();//业务id
    var j = $("#job :selected").val();//业务id
    var type = $("#Type").val();

    $.getJSON("Set", { kId: kId, type: type, bId: type == 0 ? d : j }, function (result) {
        $.modalMsg(result.msg, result.type);
        GetDataList(strurl);
    });
}

//移除
function remove(Id) {
    $.deleteForm({
        prompt: "确认移除吗？",
        url: 'Remove?id=' + Id,
        type: 'get',
        success: function () {
            var strurl = $.StringFormat("GetDataList?kId={0}&pIndex={1}", $("#kId").val(), pIndex > 1 ? pIndex - 1 : 1);
            GetDataList(strurl);
        }
    })
}