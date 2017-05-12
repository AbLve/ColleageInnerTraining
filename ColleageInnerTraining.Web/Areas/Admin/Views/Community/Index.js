$(':submit').click(function () {
    var txtname = $('#txtName').val();
    var txtQuest = $('#txtquestion').val();
    var txtbTime = $('#txtBTime').val();
    var txteTime = $('#txtETime').val();

    var strurl = $.StringFormat("GetCommunityDataList?name={0}&quest={1}&bTime={2}&eTime={3}",
        txtname, txtQuest, txtbTime, txteTime);
    $.loading(true, "正在加载数据....");
    window.setTimeout(function () {
        $('div#dataListDiv').load(strurl, function () {
            $.loading(false);
        });
    }, 500);
})