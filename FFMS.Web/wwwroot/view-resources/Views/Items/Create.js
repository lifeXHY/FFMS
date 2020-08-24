(function () {
    layui.use(['form'], function () {
        var form = layui.form
        ,layer = layui.layer

        form.render(null, 'component-form-group');


        /* 自定义验证规则 */
        form.verify({
            ItemType: function (value) {
                if (value.length > 100) {
                    return '收支项目长度不能超过100';
                }
            }
            , Memo: function (value) {
                if (value.length > 500) {
                    return '备注长度不能超过500';
                }
            }
            , content: function (value) {
                layedit.sync(editIndex);
            }
        });

        /* 监听提交 */
        form.on('submit(component-form-commit)', function (data) {
            var entity = data.field
            
            var Url = null;
            if ($('#Hidden_Id').val() == "0") {
                Url = '/Items/CreateNewItem';
            } else {
                Url = '/Items/UpdateItem';
                entity.Id = $('#Hidden_Id').val();
                entity.OldItemType = $('#ItemType').val();
            }
            $.ajax({
                type: "post",
                dataType: "json",
                async: true,
                url: Url,
                data: entity,
                success: function (data) {
                    var result = data;
                    if (result.ifSuccess == true) {
                        layer.msg('保存成功', {
                            offset: '15px'
                            , icon: 1
                            , time: 1000
                        }, function () {
                            var index = parent.layer.getFrameIndex(window.name); //获取窗口索引
                            parent.layer.close(index);
                            window.parent.location.reload();
                        });
                    } else {
                        layer.alert(result.message);
                    }
                },
                err: function (data) {
                    console.log(data);
                    layer.alert("发生了些异常！");
                }
            });
            //禁止submit 之后 表单提交 会导致页面的刷新 
            return false
        });
    });
})(jQuery)