namespace Junior.Core.ServiceModel.Response
{
    public class StatusModel
    {
        public bool IsSuccess { get; set; }
        public int ErrCode { get; set; }
        public string ErrMsg { get; set; }
    }
}
