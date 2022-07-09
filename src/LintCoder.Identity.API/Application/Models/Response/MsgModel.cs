using LintCoder.Identity.API.Application.Models.Enum;

namespace LintCoder.Identity.API.Application.Models.Response
{
    public class MsgModel
    {
        public bool IsOk { get => Code == (int)ResponseTypeEnum.Success; }
        public int Code { get; set; } 
        public string Message { get; set; }

        public object Data { get; set; }

        public static MsgModel Success(object data)
        {
            var result = new MsgModel
            {
                Data = data,
                Code = (int)ResponseTypeEnum.Success,
                Message = "Success"
            };
            return result;
        }

        public static MsgModel Fail(ResponseTypeEnum responseTypeEnum, string message)
        {
            var result = new MsgModel
            {
                Code = (int)responseTypeEnum,
                Message = message
            };
            return result;
        }

        public static MsgModel Fail(ResponseTypeEnum responseTypeEnum)
        {
            var result = new MsgModel
            {
                Code = (int)responseTypeEnum,
                Message = responseTypeEnum.ToString()
            };
            return result;
        }

        public static MsgModel Fail(string message)
        {
            var result = new MsgModel
            {
                Code = (int)ResponseTypeEnum.Error,
                Message = message
            };
            return result;
        }
    }
}
