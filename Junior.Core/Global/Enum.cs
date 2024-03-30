namespace Junior.Core.Global
{
    public static class Enum
    {
        public enum YesNoType
        {
            No,
            Yes
        };
        public enum DisableType
        {
            Enable,
            Disable
        };
        public enum PayType
        {
            Alipay,
            WeChatPay
        };
        public enum ParamType
        {
            String,
            Bool,
            Int,
            Decimal,
            Double,
            Float,
            Long
        };
        public enum HeaderType
        {
            Default,
            Authorization
        };
        public enum AuthorizationType
        {
            Default,
            BearerToken,
            SaasToken
        };
    }
}
