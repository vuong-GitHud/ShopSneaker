using Newtonsoft.Json;
using ShopSneaker.AdminMVC.Helper;
using ShopSneaker.AdminMVC.Model;

namespace ShopSneaker.AdminMVC.Infrastructure
{
    public class AuthenAPI : IAuthenAPI
    {
        protected APIExcute _aPIExcute;
        public AuthenAPI()
        {
            _aPIExcute = new APIExcute();
        }
        public async Task<AuthenViewModel> Authenticate(AuthenModel request)
        {
            string url = "https://localhost:7105/api/authen/login";

            var req = new BaseRequest<object>(new
            {
                Email = request.Email,
                request.Password
            });
            var res = await _aPIExcute.PostData<AuthenViewModel, object>(url: $"{url}", req);
            var result = JsonConvert.DeserializeObject<AuthenViewModel>(res.FullResponseString);
            return result;
        }
    }
}
