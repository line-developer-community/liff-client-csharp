using LineDC.Liff.Data;
using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace LineDC.Liff
{
    public interface ILiffClient
    {
        bool Initialized { get; set; }
        ValueTask CloseWindow();
        ValueTask<string> GetAccessToken();
        ValueTask<LiffContext> GetContext();
        ValueTask<IdTokenPayload> GetDecodedIDToken();
        ValueTask<string> GetLanguage();
        ValueTask<string> GetOS();
        ValueTask<Profile> GetProfile();
        ValueTask<string> GetVersion();
        ValueTask Init(IJSRuntime jSRuntime);
        ValueTask<bool> IsInClient();
        ValueTask<bool> IsLoggedIn();
        ValueTask Login(string redirectUri = null);
        ValueTask Logout();
        ValueTask OpenWindow(string url, bool external = false);
        ValueTask<string> ScanCode();
        ValueTask SendMessages(object[] messages);
        ValueTask ShareTargetPicker(object[] messages);
    }
}