namespace FFMS.Application
{
    /// <summary>
    /// 返回的参数模型
    /// </summary>
    public class ReturnValueModel
    {
        /// <summary>
        /// 方法是否执行成功
        /// </summary>
        public bool IfSuccess { get; set; }

        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; }

        public dynamic Model { get; set; }
    }
}
