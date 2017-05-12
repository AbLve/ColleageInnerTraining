var pIndex = 1;
$(':submit').click(function () {
    var k = $('#search').val(), t = $('#TypeId').val(), ta = $('#Taget').val();
    var strurl = $.StringFormat("GetDataList?keyword={0}&taget={1}&type={2}", k, ta, t);
    GetDataList(strurl);
})
function change() {
    $(':submit').trigger('click');
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
