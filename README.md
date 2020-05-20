# liff-client-csharp
C# wrapper of LIFF(v2) client API for use in Blazor applications.

## Supported Version
- LINE Front-end Framework v2
- .NET Core 3.1 SDK
- Blazor WebAssembly 3.2.0

## Demo Site 
Published on Github Pages  
https://line-developer-community.github.io/liff-client-csharp/

To check on the LINE app, use the following LIFF URL.  
[line://app/1653926279-Q4lOAB98](line://app/1653926279-Q4lOAB98)


## Usage
Install from the NuGet Gallery.
https://www.nuget.org/packages/LineDC.Liff/


Add the following script reference to the body of wwwroot/index.html.

```html
<script src="https://static.line-scdn.net/liff/edge/2.1/sdk.js"></script>
```


The following interfaces are supported. (LINE things Device APIs are not supported.)
```cs
using LineDC.Liff.Data;
using Microsoft.JSInterop;
using System.Threading.Tasks;


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
```

Register as a singleton service at Main method of Program.cs.
```cs
public static async Task Main(string[] args)
{
    var builder = WebAssemblyHostBuilder.CreateDefault(args);
    builder.RootComponents.Add<App>("app");
    builder.Services.AddBaseAddressHttpClient();
    
    var liffId    = "1653926279-Q4lOAB98";
    builder.Services.AddSingleton<ILiffClient>(new LiffClient(liffId));
    
    var host = builder.Build();
    await host.RunAsync();
}
```

On each page, add the @inject directive and inject ILiffClient.

```cshtml
@page "/"
@inject ILiffClient Liff
@inject IJSRuntime JSRuntime

<div class="card" style="width: 20rem;">
    @if (Profile != null)
    {
        <img class="card-img" src="@Profile?.PictureUrl" alt="Loading image..." />
        <div class="card-body">
            <h5 class="card-title">@Profile?.DisplayName</h5>
            <p class="card-text">@Profile?.StatusMessage</p>
        </div>
    }
<ul class="list-group">
    <li class="list-group-item">LIFF Ver.: @Version</li>
    <li class="list-group-item">LINE Ver: @LineVersion</li>
    <li class="list-group-item">OS: @OS</li>
    <li class="list-group-item">Language: @Language</li>
    <li class="list-group-item">TokenId: @TokenId</li>
    <li class="list-group-item">Type: @Context?.Type</li>
    <li class="list-group-item">ViewType: @Context?.ViewType</li>
    <li class="list-group-item">UserId: @Context?.UserId</li>
    <li class="list-group-item">IDToken: @IDToken?.Substring(0, 10)xxxxxxxx...</li>

    @if (@Context?.Type == ContextType.Utou)
    {
        <li class="list-group-item">UtouId: @Context?.UtouId</li>
    }
    else if (@Context?.Type == ContextType.Room)
    {
        <li class="list-group-item">RoomId: @Context?.RoomId</li>
    }
    else if (@Context?.Type == ContextType.Group)
    {
        <li class="list-group-item">GroupId: @Context?.GroupId</li>
    }
</ul>
</div>

@code{

    protected Profile Profile { get; set; }
    protected LiffContext Context { get; set; }
    protected string TokenId { get; set; }
    protected string OS { get; set; }
    protected string Language { get; set; }
    protected string Version { get; set; }
    protected string IDToken { get; set; }
    protected string LineVersion { get; set; }
    protected Friendship Friendship { get; set; }

    protected override async Task OnInitializedAsync()
    {
        try
        {
            if (!Liff.Initialized)
            {
                await Liff.Init(JSRuntime);
                if (!await Liff.IsLoggedIn())
                {
                    await Liff.Login();
                    return;
                }
                Liff.Initialized = true;
            }
            Profile = await Liff.GetProfile();
            if (await Liff.IsInClient())
            {
                Context = await Liff.GetContext();
            }
            var idtoken = await Liff.GetDecodedIDToken();
            TokenId = idtoken.Sub;
            OS = await Liff.GetOS();
            Language = await Liff.GetLanguage();
            Version = await Liff.GetVersion();
            LineVersion = await Liff.GetLineVersion();
            //Friendship = await Liff.GetFriendship();
            IDToken = await Liff.GetIDToken();
            StateHasChanged();
        }
        catch (Exception e)
        {
            await JSRuntime.InvokeAsync<object>("alert", e.ToString());
        }
    }

}


```
