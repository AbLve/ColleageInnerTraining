$(function () {
    //三级联动
    $.each(province, function (k, p) {
        var option = "<option value='" + p.ProID + "'>" + p.ProName + "</option>";
        $("#selProvince").append(option);
    });

    $("#selProvince").change(function () {
        var selValue = $(this).val();
        $("#selCity option").remove();
        $.each(city, function (k, p) {
            if (p.ProID == selValue) {
                var option = "<option value='" + p.CityID + "'>" + p.CityName + "</option>";
                $("#selCity").append(option);
            }
        });
        $("#selCity").change();
    });

    $("#selCity").change(function () {
        var selValue = $(this).val();
        $("#selDistrict option").remove();
        $.each(District, function (k, p) {
            if (p.CityID == selValue) {
                var option = "<option value='" + p.Id + "'>" + p.DisName + "</option>";
                $("#selDistrict").append(option);
            }
        });
    });

    $("#selProvince").val(ProvinceID);
    $("#selProvince").change();
    $("#selCity").val(CityID);
    $("#selCity").change();
    $("#selDistrict").val(AreaID);
    $("#Department").val(DepartmentID);
    $("#post").val(PostID);

    //提交信息
    $(":submit").click(function () {
        $("#ProvinceID").val($("#selProvince").val());
        $("#province").val($("#selProvince").find("option:selected").text());
        $("#CityID").val($("#selCity").val());
        $("#City").val($("#selCity").find("option:selected").text());
        $("#AreaID").val($("#selDistrict").val());
        $("#Area").val($("#selDistrict").find("option:selected").text());
        $("#DepartmentID").val($("#Department").val());
        $("#DepartmentName").val($("#Department").find("option:selected").text());
        $("#PostID").val($("#post").val());
        $("#PostName").val($("#post").find("option:selected").text());
        $("#fmAccountUser").validate().form();
    });
});
