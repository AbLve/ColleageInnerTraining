var pIndex = 1;

$(':submit').click(function () {
    var k = $('#search').val(), d = $('#Department').val(), b = $('#isexitsel').val(), c = $('#cid').val(), j = $('#JobPost').val();
    var strurl = $.StringFormat("GetMemberDataList?keyword={0}&dId={1}&isExit={2}&cId={3}&jId={4}", k, d, b, c, j);
    GetDataList(strurl);
})

function setmanyuser() {
    var ids = commonJs.getIds();
    if (!ids) {
        $.modalMsg('请选择一项', 'warning');
    }
    else {
        if (true) {
            $.get('SetUser', { cid: $('#cid').val(), uids: ids }, function (result) {
                $.modalMsg(result.msg, result.type);
                $(':submit').trigger('click');
            })
        }
    }
}

function change() {
    var e = $('#isexitsel').val();
    if (e === '1') {
        $('#setuserbtn').attr('disabled', 'disabled');
    }
    else {
        $('#setuserbtn').removeAttr('disabled');
    }
    $(':submit').trigger('click');
}

function setsingleuser(uids) {
    $.get('SetUser', { cid: $('#cid').val(), uids: uids + ',' }, function (result) {
        $.modalMsg(result.msg, result.type);
        $(':submit').trigger('click');
    })
}

function remove(uid) {
    $.deleteForm({
        prompt: "确认移除吗？",
        url: 'RemoveUser',
        param: { cid: $('#cid').val(), uid: uid },
        type: 'get',
        success: function () {
            var k = $('#search').val(), d = $('#Department').val(), b = $('#isexitsel').val(), c = $('#cid').val(), j = $('#JobPost').val();
            var strurl = $.StringFormat("GetMemberDataList?keyword={0}&dId={1}&isExit={2}&cId={3}&jId={4}&pIndex={1}", k, d, b, c, j, k, pIndex = pIndex > 1 ? pIndex - 1 : 1);
            GetDataList(strurl);
        }
    })
}

$("#Department").bindSelect({
    url: "GetDTreeSelectJson"
});