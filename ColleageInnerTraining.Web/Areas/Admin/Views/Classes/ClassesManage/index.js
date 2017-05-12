var pIndex = 1;
$(':submit').click(function () {
    var k = $('#search').val();
    var strurl = $.StringFormat("GetCManageDataList?keyword={0}", k);
    GetDataList(strurl);
})
function del(id) {
    $.deleteForm({
        url: 'ClassesManageDelete?id=' + id,
        type: 'get',
        success: function () {
            var d = $('#sel').val(), k = $('#search').val();
            var strurl = $.StringFormat("GetCManageDataList?keyword={0}&pIndex={1}", k, pIndex = pIndex > 1 ? pIndex - 1 : 1);
            GetDataList(strurl);
        }
    })
}