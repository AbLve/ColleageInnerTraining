$(':submit').click(function () {
    var name = $('#name').val() == "" || $('#name').val() == undefined ? "" : $('#name').val();
    var department = $('#tetDepartment').val() == "" ? 0 : $('#tetDepartment').val();
    var jobPost = $('#txtJobPost').val() == "" ? 0 : $('#txtJobPost').val();
    var strurl = $.StringFormat("GetAccountUserDataList?name={0}&departmentId={1}&jobPostId={2}",
        name, department, jobPost);
    $.loading(true, "正在加载数据....");
    window.setTimeout(function () {
        $('div#dataListDiv').load(strurl, function () {
            $.loading(false);
        });
    }, 500);
})

$("#Synchronization").click(function () {
    $('#Synchronization').attr('disabled', "true");
    $.get('UserYynchronization', function (result) {
        $("#Synchronization").removeAttr("disabled");
        if (result == "0") {
            $.modalMsg("用户同步成功");
        } else {
            $.modalMsg("用户同步失败");
        }
    })
});
$("#tetDepartment").bindSelect({
    url: "GetTreeDepartSelectJson"
});