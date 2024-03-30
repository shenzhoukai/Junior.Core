namespace Junior.Core.ServiceModel.Response
{
    public class BaseResponse<T, X, Y>
    {
        public BaseResponse()
        {
            Data = default(T);
            Status = default(X);
            Request = default(Y);
        }
        public T Data { get; set; }
        public X Status { get; set; }
        public Y Request { get; set; }
    }
}
