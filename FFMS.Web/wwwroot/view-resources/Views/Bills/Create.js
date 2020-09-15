(function () {
    layui.use(['form', 'laydate'], function () {
        var form = layui.form
            , layer = layui.layer
            , laydate = layui.laydate
            
        form.render();

        laydate.render({
            elem: '#AccountDate',
            value: new Date()
            , trigger: 'click'//呼出事件改成click
        });

        form.on('select(ItemType)', function (data) {
            $("#Hidden_ItemType").val(data.elem[data.elem.selectedIndex].text);
            //$("#ItemsId").val(data.value);
        });    

        /* 监听提交 */
        form.on('submit(component-form-commit)', function (data) {
            var entity = data.field
            entity.ItemType = $("#Hidden_ItemType").val();
            var Url = null;
            if ($('#Hidden_Id').val() == "0") {
                Url = '/Bill/CreateNewBill';
            } else {
                Url = '/Bill/UpdateBill';
                entity.Id = $('#Hidden_Id').val();
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
