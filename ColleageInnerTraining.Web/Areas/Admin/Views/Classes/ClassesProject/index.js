var pIndex = 1;
$(':submit').click(function () {
    var t = $('#ClassProType').val(), k = $('#search').val(), c = $('#cid').val();
    var strurl = $.StringFormat("GetProjectDataList?keyword={0}&type={1}&cId={2}", k, t, c);
    GetDataList(strurl);
})
function change() {
    $(':submit').trigger('click');
}
function remove(Id, type) {

    $.deleteForm({
        prompt: "确认移除吗？",
        url: 'RemoveProject?Id=' + Id + '&cId=' + $('#cid').val() + '&type=' + type,
        type: 'get',
        success: function () {
            var k = $('#search').val();
            var strurl = $.StringFormat("GetProjectDataList?keyword={0}&pIndex={1}&cId={2}", k, pIndex = pIndex > 1 ? pIndex - 1 : 1, $('#cid').val());
            GetDataList(strurl);
        }
    })

}