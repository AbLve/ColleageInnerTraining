$(':submit').click(function () {
    var txtcname = $('#courseName').val() == "" ? "" : $('#courseName').val();
    var txttname = $('#txtTeacherName').val() == "" ? "" : $('#txtTeacherName').val();
    var txttype = $('#txtType').val();
    var txtcate = $('#txtCate').val();

    var txtDepartment = $('#tetDepartment').val();
    var txtjobPost = $('#txtJobPost').val();
    var strurl = $.StringFormat("GetCurriManageDataList?txtcname={0}&txttname={1}&txttype={2}&txtcate={3}&DepartmentId={4}&jobPostId={5}"
        , txtcname, txttname, txttype, txtcate, txtDepartment, txtjobPost);
    $.loading(true, "正在加载数据....");
    window.setTimeout(function () {
        $('div#dataListDiv').load(strurl, function () {
            $.loading(false);
        });
    }, 500);
})
//编辑
function editorCourse(status, id)
{
    if (status == 2 || status == 4) {
        $.modalMsg('该课程已审核或已完成，不允许编辑', 'error');
        return false;
    }
    location.href = "/Admin/Course/CreateAndEdite?id=" + id;
}

$("#txtCate").bindSelect({
    url: "GetCateTreeSelectJson"
});
$("#tetDepartment").bindSelect({
    url: "GetTreeDepartSelectJson"
});