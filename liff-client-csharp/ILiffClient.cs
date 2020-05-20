using LineDC.Liff.Data;
using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace LineDC.Liff
{
    public interface ILiffClient
    {
        bool Initialized { get; set; }
        ValueTask Init(IJSRuntime jSRuntime);
        ValueTask CloseWindow();
        ValueTask<string> GetAccessToken();
        ValueTask<string> GetIDToken();
        ValueTask<LiffContext> GetContext();
        ValueTask<IdTokenPayload> GetDecodedIDToken();
        ValueTask<string> GetLanguage();
        ValueTask<string> GetOS();
        ValueTask<Profile> GetProfile();
        ValueTask<string> GetVersion();
        ValueTask<string> GetLineVersion();
        ValueTask<bool> IsInClient();
        ValueTask<bool> IsLoggedIn();
        ValueTask Login(string redirectUri = null);
        ValueTask Logout();
        ValueTask OpenWindow(string url, bool external = false);
        ValueTask<string> ScanCode();
        ValueTask SendMessages(params object[] messages);
        ValueTask ShareTargetPicker(params object[] messages);
        ValueTask<bool> IsApiAvailable(string apiName);
        ValueTask<Friendship> GetFriendship();
    }
}