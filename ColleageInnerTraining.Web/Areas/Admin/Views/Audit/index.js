var pIndex = 1;
$(':submit').click(function () {
    var k = $('#search').val(), t = $('#TypeId').val();
    var strurl = $.StringFormat("GetDataList?keyword={0}&type={1}", k, t);
    GetDataList(strurl);
})
function change() {
    $(':submit').trigger('click');
}
function audited(id, s) {
    $.get('Audited', { id: id, stauts: s }, function (result) {
        $.modalMsg(result.msg, result.type);
        $(':submit').trigger('click');
    })
}
function del(id) {
    $.deleteForm({
        url: 'Delete?id=' + id,
        type: 'get',
        success: function () {
            var k = $('#search').val();
            var strurl = $.StringFormat("GetDataList?keyword={0}&pIndex={1}", k, pIndex > 1 ? pIndex - 1 : 1);
            GetDataList(strurl);
        }
    })
}
$("#TypeId").bindSelect({
    url: "GetCTreeSelectJson"
});
