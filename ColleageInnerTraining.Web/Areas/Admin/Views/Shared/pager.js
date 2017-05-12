//替换分页href
$("div.page-foot a").click(function () {
    if (!$(this).attr('datahref')) {
        return;
    }
    var k = $('#search').val();
    $.loading(true, "正在加载数据....");
    var $this = $(this);
    window.setTimeout(function () {
        var strurl = $.StringFormat($this.attr("datahref") + "&keyword={0}", k);
        pIndex = strurl.getParam('pIndex');
        $('div#dataListDiv').load(strurl, function () {
            $.loading(false);
        });
    }, 500);
});
function GoPage() {
    if ($('#pIndextxt').val() == 0 || parseInt($('#pIndextxt').val()) > parseInt($('#pageCount').html())) {
        $('#pIndextxt').attr('title', '页数无效');
        return false;
    }
    location.href = location.href + '?pIndex=' + $('#pIndextxt').val() + '&pSize=' + $('#txt_ShowPageNum').val() + '&keyword=' + $('[type=search]').val();
}
function pagesizeChange() {
    location.href = location.href + '?pSize=' + $('#txt_ShowPageNum').val() + '&keyword=' + $('[type=search]').val();
}
function GetDataList(strurl){
    $.loading(true, "正在加载数据....");
    window.setTimeout(function () {
        $('div#dataListDiv').load(strurl, function () {
            $('.table_info').InitTable();
        });
    }, 500);
}