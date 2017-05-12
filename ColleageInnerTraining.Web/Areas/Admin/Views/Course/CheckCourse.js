$(':submit').click(function () {
    var txtcname = $('#courseName').val() == "" ? "" : $('#courseName').val();
    var txttname = $('#txtTeacherName').val() == "" ? "" : $('#txtTeacherName').val();
    var txttype = $('#txtType').val();
    var txtcate = $('#txtCate').val();
    var strurl = $.StringFormat("GetCheckCourseDataList?txtcname={0}&txttname={1}&txttype={2}&txtcate={3}", txtcname, txttname, txttype, txtcate);
    $.loading(true, "正在加载数据....");
    window.setTimeout(function () {
        $('div#dataListDiv').load(strurl, function () {
            $.loading(false);
        });
    }, 500);
});

//审核通过，审核不通过
function CheckCourse(id, status, cStatus) {
    if (cStatus == "2" || cStatus == "3") {
        $.modalMsg('该课程已经审核过了', 'error');
        return false;
    }
    $.modalConfirm('您确定要审核吗？',
        function (a) {
            if (a) {
                $.get("/Admin/Course/Check",
                    { id: id, status: status },
                    function (result) {
                        if (result == "0") {
                            location.href = "/Admin/Course/CheckCourse"
                        } else {
                            $.modalMsg('审核失败', 'error');
                        }
                    })
            }


        });
}