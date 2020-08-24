(function () {
    layui.use(['form', 'laydate'], function () {
        var laydate = layui.laydate
            , form = layui.form;

        form.render(null, 'component-form-group');

        laydate.render({
            elem: '#LAY-component-form-group-date'
        });


        /* 自定义验证规则 */
        form.verify({
            UserName: function (value) {
                if (value.length > 50) {
                    return '用户名长度不能超过50';
                }
            }
            , PassWord: [/(.+){6,12}$/, '密码必须6到12位']
            , content: function (value) {
                layedit.sync(editIndex);
            }
        });

        /* 监听提交 */
        form.on('submit(component-form-commit)', function (data) {
            //parent.layer.alert(JSON.stringify(data.field), {
            //    title: '最终的提交信息'
            //})
            //return false;
            var entity = data.field
            entity.Sex = $("input[name='sex']:checked").val();
            entity.IsActive = $("input[name='IsActive']:checked").val();
            var Url = null;
            if ($('#Hidden_Id').val() == "0") {
                Url = '/User/CreateNewUser';
            } else {
                Url = '/User/UpdateUserData';
                entity.Id = $('#Hidden_Id').val();
            }
            console.log(entity);
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
                                window.parent.location.reload(); //后台主页
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