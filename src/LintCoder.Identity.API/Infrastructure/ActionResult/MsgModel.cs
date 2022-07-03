namespace LintCoder.Identity.API.Infrastructure.ActionResult
{
    public class MsgModel<T> where T : class
    {
        /// <summary>
        /// 请求是否处理成功
        /// </summary>
        public bool isok { get; set; } = true;

        /// <summary>
        /// 请求响应状态码（200、400、500）
        /// </summary>
        public int code { get; set; } = 200;

        /// <summary>
        /// 请求结果描述信息
        /// </summary>
        public string message { get; set; }

        /// <summary>
        /// 请求结果数据（通常用于查询操作）
        /// </summary>
        public List<T> data { get; set; }

        public MsgModel() { }


        /// <summary>
        /// 请求成功的响应，不带查询数据（用于删除、修改、新增接口）
        /// </summary>
        /// <returns></returns>
        public static MsgModel<T> Success()
        {
            var msg = new MsgModel<T>
            {
                isok = true,
                code = 200,
                message = "请求响应成功!"
            };
            return msg;
        }

        /// <summary>
        /// 请求成功的响应，带有查询数据（用于数据查询接口）
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static MsgModel<T> Success(List<T> list)
        {
            var msg = new MsgModel<T>
            {
                isok = true,
                code = 200,
                message = "请求响应成功",
                data = list
            };
            return msg;
        }

        public static MsgModel<T> Success(string message)
        {
            var msg = new MsgModel<T>
            {
                isok = true,
                code = 200,
                message = message
            };
            return msg;
        }

        /// <summary>
        /// 请求成功的响应，带有查询数据（用于数据查询接口）
        /// </summary>
        /// <param name="list"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static MsgModel<T> Success(List<T> list, string message)
        {
            var msg = new MsgModel<T>
            {
                isok = true,
                code = 200,
                message = message,
                data = list
            };
            return msg;
        }

        public static MsgModel<T> Fail(string message)
        {
            var msg = new MsgModel<T>
            {
                isok = false,
                code = 200,
                message = message
            };
            return msg;
        }

        public static MsgModel<T> Fail(int code, string message)
        {
            var msg = new MsgModel<T>
            {
                isok = false,
                code = code,
                message = message
            };
            return msg;
        }
    }
}
