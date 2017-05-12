/*
 * *********************************************
 * molInfo的js公共类
 * Timestamp : 2016/9/6
 *********************************************
 */
var MolInfo = MolInfo || {};

//web页面方法接口
MolInfo.utils={
	//iframe弹出层
	modalDialog: function (options) {
        var defaults = {
            id: null, //tagert id
            title: '系统窗口',
            content: '内容',
            closeBtn:1,
            skin: "animated bounceInRight",
            shade: 0.5,
            area: ["650px", "470px"],
            shadeClose: true,
            btn: ['确认', '关闭'],
            btnclass: ['btn btn-primary', 'btn btn-danger'],
            callBack: null
        };
        var opts = $.extend(true, defaults, options);
        layer.open({
            id: opts.id,
            type: 2,
            shade: opts.shade,
            title: opts.title,
            skin: opts.skin,
            fix: false,
            area: opts.area,
            content: opts.content,
            btn: opts.btn,
            btnclass: opts.btnclass,
            yes: function () {
                opts.callBack(opts.id)
            }, cancel: function () {
                return true;
            }
        });
    },
	
    //提示信息
    popMsg: function(msg){
        parent.layer.msg(msg,{
        	time: 1500,
            shade: 0.5,
            title: '提示',
            skin: 'layui-layer-msg',
            fix: false,
			move: false,
			scrollbar: false,
            area: ['370px','160px'],
            btn:['确定'],
            yes: function(index){
            	parent.layer.close(index);
            }
        })
    },
    
    //询问确认框
    popAlert: function(options){
    	var defaults = {
    		id: null,
            title: '警告',
            content: '内容',
            skin: "layui-layer-msg",
            shade: 0.5,
            area: ['370px','160px'],
            shadeClose: true,
            btn: ['确认', '取消'],
            callBack: null
        };
        var opts = $.extend(true, defaults, options);
        parent.layer.open({
            id: null,
            shade: opts.shade,
            title: opts.title,
            skin: opts.skin,
            fix: false,
            area: opts.area,
            content: opts.content,
            btn: opts.btn,
            yes: function (index) {
                opts.callBack(opts.id,index);
            }, cancel: function () {
                return true;
            }
        });
    }
}