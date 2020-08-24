(function () {
    layui.use(['form'], function () {
        var form = layui.form;

        form.render(null, 'component-form-group');

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
        form.on('submit(LAY-user-login-submit)', function (data) {
            var entity = data.field;
            $.ajax({
                type: "post",
                dataType: "json",
                async: true,
                url: '/Account/Login',
                data: entity,
                success: function (data) {
                    var result = data;
                    if (result.ifSuccess == true) {
                        layer.alert('登陆成功！', { icon: 1, closeBtn: 0 }, function (index) {
                            //关闭弹窗
                            parent.layer.close(index);
                            setTimeout(function () {
                                window.location.href = "/Home"
                            }, 500)
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