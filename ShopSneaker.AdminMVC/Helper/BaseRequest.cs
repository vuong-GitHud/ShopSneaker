namespace ShopSneaker.AdminMVC.Helper
{
    public class BaseRequest<TRequest>
    {
        public BaseRequest()
        {

        }
        public BaseRequest(TRequest _requestData)
        {
            RequestData = _requestData;
        }
        public TRequest RequestData { get; set; }
    }
}
