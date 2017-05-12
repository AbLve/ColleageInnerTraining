var pIndex = 1;
$(':submit').click(function () {
    var k = $('#search').val(), t = $('#Type').val();
    var strurl = $.StringFormat("/Admin/Banner/GetDataList?keyword={0}&type={1}", k, t);
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
