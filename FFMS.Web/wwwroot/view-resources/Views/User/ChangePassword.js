(function () {
    layui.use(['form', 'laydate'], function () {
        var form = layui.form;

        form.render(null, 'component-form-group');


        /* 自定义验证规则 */
        form.verify({
            NewPassWord: [/(.+){6,12}$/, '密码必须6到12位']
            , OldPassWord: [/(.+){6,12}$/, '密码必须6到12位']
        });

        /* 监听提交 */
        form.on('submit(component-form-commit)', function (data) {
            var entity = data.field
            if (entity.OldPassWord == entity.NewPassWord) {
                layer.msg('原始密码不能与新密码相同', {
                    offset: '15px'
                    , icon: 1
                    , time: 1000
                });
                return false;
            }

            $.ajax({
                type: "post",
                dataType: "json",
                async: true,
                url: '/User/ChangeUserPassword',
                data: entity,
                success: function (data) {
                    var result = data;
                    if (result.ifSuccess == true) {
                        layer.msg('修改成功，即将退出重新登录', {
                            offset: '15px'
                            , icon: 1
                            , time: 1000
                        }, function () {
                            var index = parent.layer.getFrameIndex(window.name); //获取窗口索引
                            parent.layer.close(index);
                                window.parent.location.href = '/Account/Logout'; //后台主页
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