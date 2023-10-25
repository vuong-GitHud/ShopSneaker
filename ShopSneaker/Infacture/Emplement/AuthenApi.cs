using Newtonsoft.Json;
using ShopSneaker.Infacture.Helper;
using ShopSneaker.Infacture.interfaces;
using ShopSneaker.Models;

namespace ShopSneaker.Infacture.Emplement;

public class AuthenApi : IAuthenApi
{
    protected APIExcute _aPIExcute;
    public AuthenApi()
    {
        _aPIExcute = new APIExcute();
    }
    public async Task<AuthenViewModel> Authenticate(AuthenVm request)
    {
        string url = "https://localhost:7105/api/authen";

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