//编辑
$('.lanse').bind('click', del);
$('.yanhse').click(function () {
    var abtn = $(this);
    var nextbtn = $(this).next();
    var li = abtn.parents("li");
    var txt = li.find(".tagetnametxt");
    var p = li.find('p');
    if (txt.hasClass('none')) {
        p.hide();
        txt.removeClass('none').focus();
    }
    else {
        p.show();
        txt.addClass('none');
    }

    if (abtn.html() === '确认') {
        abtn.html('修改');
        nextbtn.html('删除').unbind('click').bind("click", del);
    }
    else {
        abtn.html('确认').unbind('click').bind("click", sava);
        nextbtn.html('取消').unbind('click').bind("click", cancel);
    }
})
//取消
function cancel() {
    reloaddata();
}
//删除
function del() {
    var $this = $(this).parents('li').find('input.tagetnametxt');
    $.deleteForm({
        url: 'Delete?id=' + $this.attr('id'),
        type: 'get',
        success: function () {
            $this.parents('li').remove();
        }
    })
}
//新增
function add() {
    var lihtml = $('<li><div class="tagetnamediv"><input type="text" id="" class="tagetnametxt" placeholder="请输入标签名称" /><p>标签</p></div>\
                           <div class="adiv"><a class="yanhse" onclick="sava(this)">确认</a> |<a class="lanse" onclick="cancel(this)">取消</a></div></li>');
    $('#dataListDiv ul').append(lihtml);
}
//保存
function sava(e) {
    var $this = $(e).parents('li').find('input.tagetnametxt');
    var id = $this.attr('id'), name = $this.val();
    if (!name) {
        $this.focus();
        return;
    }
    var dto = { Id: id, Name: name };
    $.submitForm({
        url: 'Save',
        param: dto,
        success: function (result) {
            reloaddata();
        }
    })
}

function reloaddata() {
    var strurl = "GetDataList";
    $.loading(true, "正在加载数据....");
    window.setTimeout(function () {
        $('div#dataListDiv').load(strurl, function () {
            $.loading(false);
            $('.lanse').click(del);
        });
    }, 500);
}
