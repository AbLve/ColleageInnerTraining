$(function() {
	change_background();
	//header slide
	$(".table_info .structure").click(function(){
		if($(this).parent().attr("Date_structure")=="date_structure"){
			$(this).find("i").removeClass("icon_up").addClass("icon_down");
			$(this).parent().nextUntil(".structure_cols").hide();
			$(this).parent().attr("Date_structure","")
		}
		else{
			$(this).find("i").removeClass("icon_down").addClass("icon_up");
			$(this).parent().nextUntil(".structure_cols").show();
			$(this).parent().attr("Date_structure","date_structure")
		}
		
	});
	//
	$(".tan_btn").click(function(){
		$(".Popup").show();
	});
	$(".btn_complete,.btn_cancel,.close_btn").click(function(){
		$(".Popup").hide();
	});
	
	//知识点增加
	$(".knowledge-tab a").click(function(){
		$(this).addClass("cur");
		$(this).siblings("a").removeClass("cur");
	});
	
	$("#add-btn").click(function(){
		var i=1;
		$("#needuser").append($("#needusers"+i).clone());
		i++;
	});
	
//	//logobar region
//	$(".region").hover(function(){
//		$(".region_select").stop().slideToggle(500)
//	});
//	//banner
//	if(jQuery(".BanerSlider").length) {
//		$(".BanerSlider").slide({
//			titCell: ".hd ul",
//			mainCell: ".bd ul",
//			effect: "left",
//			autoPage: true,
//			effect: "leftLoop",
//			autoPlay: true,
//			delayTime: 700
//		});
//	}
//	//index nav
//	$(".ailsa-nav-ul li").hover(function(){
//      $(this).find(".Anav-title").attr("class","Anav-title ailse-thisnav");
//      $(this).find(".Anav-title").find("em").css({"background-position-x":'-40px'},200);
//      $(this).find(".Anav-sub").show();
//      $(this).find(".Anav-subB").show();
//      },
//      function(){
//      $(this).find(".Anav-title").attr("class","Anav-title");
//      $(this).find(".Anav-title").find("em").animate({"background-position-x":'-4px'},200);
//      $(this).find(".Anav-sub").hide();
//      $(this).find(".Anav-subB").hide();
//  });
//  //index 公告
//  $(".forum_nav span").click(function() {
//			var $click = $('.forum_nav span');
//			var $tabcon = $('.forum_mian .forum_con');
//			var $this = $(this);
//			var $t = $this.index();
//			$click.removeClass("cur");
//			$this.addClass('cur');
//			$tabcon.hide();
//			$tabcon.eq($t).show();
//	})
//	//index 右侧导航
//	$(".fix-btn-group li").hover(function(){
//  	$(this).find("span").stop(false,true).animate({"width":"85px"},200);
//  },function(){
//  	$(this).find("span").stop(false,true).animate({"width":"0"},200);
//  });
//  
//	$(".icon-qrcode").hover(function() {
//		$(".fix-erweima").stop().show(500)
//	}, function() {
//		$(".fix-erweima").stop().hide(500)
//	})
//	//
//	$(".details-main-m .peisong dt").click(function(){
//		$(".m-select dd.ps_region").slideToggle(500)
//	});
//	
//	//
//	$(".opera_select").hover(function(){
//		$(".opera_select div").slideToggle(500)
//	});
//	
//	//
//	$(".goods_category .btn_huise").click(function(){
//		$(".goods_spec").slideToggle(500)
//	});
//
//	
//	$(".icon-arrow-up").click(function() {
//
//		$('html,body').animate({
//			scrollTop: '0px'
//		}, 800)
//	})
//	$(".panel-heading").click(function(){
//		$(this).next(".panel-collapse").slideToggle(500);
//	});
//	$(".gw2_addrcon_top").click(function(){
//		$("#address_form").slideToggle(500);
//	});
//	
//	$(".see_icon").hover(function(){
//		$(".see_Evm").slideToggle(500)
//	});
//	$(".fl_tab li").click(function() {
//			var $click = $('.fl_tab li');
//			var $tabcon = $('.ss_f1_phb .fl_tab_div');
//			var $this = $(this);
//			var $t = $this.index();
//			$click.removeClass("fl_current");
//			$this.addClass('fl_current');
//			$tabcon.hide();
//			$tabcon.eq($t).show();
//	});
//	$(".News_con .title span").click(function() {
//			var $click = $('.News_con .title span');
//			var $tabcon = $('.News_con .News_detial');
//			var $this = $(this);
//			var $t = $this.index();
//			$click.removeClass("cur");
//			$this.addClass('cur');
//			$tabcon.hide();
//			$tabcon.eq($t).show();
//	});
//	//
//	$(".HotShop_head .HotShop_tab").click(function() {
//			var $click = $('.HotShop_head .HotShop_tab');
//			var $tabcon = $('.HotShop_box ul');
//			var $this = $(this);
//			var $t = $this.index();
//			$click.removeClass("active");
//			$this.addClass('active');
//			$tabcon.hide();
//			$tabcon.eq($t).show();
//	});
	
//	$(".add").click(function(){
//		$(this).prev().val(parseInt($(this).prev().val())+1);
//	});
//	$(".min").click(function(){
//		
//		if(parseInt($(this).next().val())<2){
//			alert("数量不能为零！");
//		}else{
//			$(this).next().val(parseInt($(this).next().val())-1);
//		}
//		
//	});
});


function selectAll(){  
    if ($("#SelectAll").attr("checked")) {  
        $(":checkbox").attr("checked", true);  
    } else {  
        $(":checkbox").attr("checked", false);  
    }  
}  
//子复选框的事件  
function setSelectAll(){  
    //当没有选中某个子复选框时，SelectAll取消选中  
    if (!$("#subcheck").checked) {  
        $("#SelectAll").attr("checked", false);  
    }  
    var chsub = $("input[type='checkbox'][id='subcheck']").length; //获取subcheck的个数  
    var checkedsub = $("input[type='checkbox'][id='subcheck']:checked").length; //获取选中的subcheck的个数  
    if (checkedsub == chsub) {  
        $("#SelectAll").attr("checked", true);  
    }  
} 




		//图片上传预览    IE是用了滤镜。
        function previewImage(file)
        {
          var MAXWIDTH  = 260; 
          var MAXHEIGHT = 180;
          var div = document.getElementById('preview');
          if (file.files && file.files[0])
          {
              div.innerHTML ='<img id="imghead">';
              var img = document.getElementById('imghead');
              img.onload = function(){
                var rect = clacImgZoomParam(MAXWIDTH, MAXHEIGHT, img.offsetWidth, img.offsetHeight);
                img.width  =  rect.width;
                img.height =  rect.height;
//              img.style.marginLeft = rect.left+'px';
//              img.style.marginTop = rect.top+'px';
              }
              var reader = new FileReader();
              reader.onload = function(evt){img.src = evt.target.result;}
              reader.readAsDataURL(file.files[0]);
          }
          else //兼容IE
          {
            var sFilter='filter:progid:DXImageTransform.Microsoft.AlphaImageLoader(sizingMethod=scale,src="';
            file.select();
            var src = document.selection.createRange().text;
            div.innerHTML = '<img id=imghead>';
            var img = document.getElementById('imghead');
            img.filters.item('DXImageTransform.Microsoft.AlphaImageLoader').src = src;
            var rect = clacImgZoomParam(MAXWIDTH, MAXHEIGHT, img.offsetWidth, img.offsetHeight);
            status =('rect:'+rect.top+','+rect.left+','+rect.width+','+rect.height);
            div.innerHTML = "<div id=divhead style='width:"+rect.width+"px;height:"+rect.height+"px;margin-top:"+rect.top+"px;"+sFilter+src+"\"'></div>";
          }
        }
        function clacImgZoomParam( maxWidth, maxHeight, width, height ){
            var param = {top:0, left:0, width:width, height:height};
            if( width>maxWidth || height>maxHeight )
            {
                rateWidth = width / maxWidth;
                rateHeight = height / maxHeight;
                 
                if( rateWidth > rateHeight )
                {
                    param.width =  maxWidth;
                    param.height = Math.round(height / rateWidth);
                }else
                {
                    param.width = Math.round(width / rateHeight);
                    param.height = maxHeight;
                }
            }
            param.left = Math.round((maxWidth - param.width) / 2);
            param.top = Math.round((maxHeight - param.height) / 2);
            return param;
        }
        




function change_background()
{
    $(".distinction tr").hover(
    function()
    {
        $(this).css({background:"#ffe6e6"});
    },
    function()
    {
        $(this).css({background:"#fff"});
    });
}

function flex(obj)
{
    var status = obj.attr('status');
    var id = obj.attr('fieldid');
    var pid = obj.parent('td').parent('tr').attr("class");
    var src = $(obj).attr('src');
    if(status == 'open')
    {
        var pr = $(obj).parent('td').parent('tr');
        var selfurl = window.location.href;
        var sr  = pr.clone();
        var td2 = sr.find("td:eq(1)");
        td2.prepend("<img class='preimg' src='templates/style/images/treetable/vertline.gif'/>")
                        .find("img[xmtype=flex]")
                        .remove()
                        .end()
                        .find('span')
                        .remove();
        var img_count = td2.children("img").length;
        var td2html = td2.html();
         $.get(selfurl + "&act=ajax_cate", {id: id}, function(data){
             if(data)
             {
                 var str = '';
                 var add_child = '';
                 var res = eval('(' + data + ')');
                 for(var i = 0; i < res.length; i++)
                 {
                     if(res[i].switchs)
                     {
                         src =  "<img src='templates/style/images/treetable/tv-expandable.gif' xmtype='flex' status='open' fieldid="+res[i].region_id+
                           " onclick='flex($(this))'><span class='node_name'>" + res[i].region_name + "</span>";
                     }
                     else
                     {
                         src = "<img src='templates/style/images/treetable/tv-item.gif'><span class='node_name'>" + res[i].region_name + "</span>";
                     }
                     if(img_count < (4 -1))
                     {
                         add_child = "<a href='index.php?app=region&amp;act=add&amp;pid="+res[i].region_id+"'>新增下级</a>";
                     }
                     var itd2 = td2html + src;
                     str+="<tr class='"+pid+" row"+id+"'><td class='align_center w30'><input type='checkbox' class='checkitem' value='" + res[i].region_id + "'></td>"+
                        "<td class='node' width='50%'>" + itd2 + "</td>"+
                        "<td class='align_center'><span xmtype='inline_edit' fieldname='sort_order' fieldid='" + res[i].region_id + "' datatype='pint' maxvalue='255' title='单击可以编辑' class='editable'>" + res[i].sort_order + "</span></td>"+
                        "<td class='handler'><span><a href='index.php?app=region&act=edit&id=" + res[i].region_id + "'>编辑</a> | <a href='javascript:if(confirm(\""+lang.confirm_delete+"\"))window.location=\"index.php?app=region&act=drop&id=" + res[i].region_id + "\";'>删除</a> | " + add_child + "</span></td>";
                 }
                pr.after(str);
                change_background();
                $('span[xmtype="inline_edit"]').unbind('click');
                $.getScript(SITE_URL+"/includes/libraries/javascript/inline_edit.js");
             }
         });
         obj.attr('src',src.replace("tv-expandable","tv-collapsable"));
         obj.attr('status','close');
    }
    if(status == 'close')
    {
        $('.row' + id).hide();
        obj.attr('src',src.replace("tv-collapsable","tv-expandable"));
        obj.attr('status','open');
    }
}

function Format(now, mask) {
    var d = now;
    var zeroize = function (value, length) {
        if (!length) length = 2;
        value = String(value);
        for (var i = 0, zeros = ''; i < (length - value.length) ; i++) {
            zeros += '0';
        }
        return zeros + value;
    };

    return mask.replace(/"[^"]*"|'[^']*'|\b(?:d{1,4}|m{1,4}|yy(?:yy)?|([hHMstT])\1?|[lLZ])\b/g, function ($0) {
        switch ($0) {
            case 'd': return d.getDate();
            case 'dd': return zeroize(d.getDate());
            case 'ddd': return ['Sun', 'Mon', 'Tue', 'Wed', 'Thr', 'Fri', 'Sat'][d.getDay()];
            case 'dddd': return ['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'][d.getDay()];
            case 'M': return d.getMonth() + 1;
            case 'MM': return zeroize(d.getMonth() + 1);
            case 'MMM': return ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'][d.getMonth()];
            case 'MMMM': return ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'][d.getMonth()];
            case 'yy': return String(d.getFullYear()).substr(2);
            case 'yyyy': return d.getFullYear();
            case 'h': return d.getHours() % 12 || 12;
            case 'hh': return zeroize(d.getHours() % 12 || 12);
            case 'H': return d.getHours();
            case 'HH': return zeroize(d.getHours());
            case 'm': return d.getMinutes();
            case 'mm': return zeroize(d.getMinutes());
            case 's': return d.getSeconds();
            case 'ss': return zeroize(d.getSeconds());
            case 'l': return zeroize(d.getMilliseconds(), 3);
            case 'L': var m = d.getMilliseconds();
                if (m > 99) m = Math.round(m / 10);
                return zeroize(m);
            case 'tt': return d.getHours() < 12 ? 'am' : 'pm';
            case 'TT': return d.getHours() < 12 ? 'AM' : 'PM';
            case 'Z': return d.toUTCString().match(/[A-Z]+$/);
                // Return quoted strings with the surrounding quotes removed
            default: return $0.substr(1, $0.length - 2);
        }
    });
};