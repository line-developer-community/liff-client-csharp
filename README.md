# liff-client-csharp
C# wrapper of LIFF client API for use in Blazor applications.

## Demo Site 
Published on Github Pages  
https://line-developer-community.github.io/liff-client-csharp/

To check on the LINE app, use the following LIFF URL.  
[line://app/1580263859-XkGzq2Dg](line://app/1580263859-XkGzq2Dg)


## Usage
Install from the NuGet Gallery.
https://www.nuget.org/packages/LineDC.Liff/


Add the following script reference to the body of wwwroot / index.html.

```html
<script src="https://d.line-scdn.net/liff/1.0/sdk.js"></script>
<script src="_content/LineDC.Liff/liffInterop.js"></script>
```


The following interfaces are supported. (LINE things Device APIs are not supported.)
```cs
public interface ILiffClient
{
    bool Initialized { get; }
    LiffData Data { get; }
    Profile Profile { get; }
    string AccessToken { get; }

    Task InitializeAsync(IJSRuntime jSRuntime);
    Task LoadProfileAsync();
    Task<string> GetAccessTokenAsync();
    Task SendMessagesAsync(object messages);
    Task CloseWindowAsync();
    Task OpenWindowAsync(string url, bool external);
    void Reset();
}
```

Register as a singleton service at Startup.cs.
```cs
public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<ILiffClient, LiffClient>();
    }

    public void Configure(IComponentsApplicationBuilder app)
    {
        app.AddComponent<App>("app");
    }
}
```

On each page, add the @inject directive and inject ILiffClient.

```cshtml
@page "/"
@inject ILiffClient Liff
@inject IJSRuntime JSRuntime

<div class="card" style="width: 20rem;">
    @if (Liff.Profile != null)
    {
    <img class="card-img" src="@Liff.Profile?.PictureUrl" alt="Loading image..." />
    <div class="card-body">
        <h5 class="card-title">@Liff.Profile?.DisplayName</h5>
        <p class="card-text">@Liff.Profile?.StatusMessage</p>
    </div>
    }
    else
    {
    <div class="card-body">
        <button class="btn btn-info" onclick="@LoadProfileAsync">プロファイル読み込み</button>
    </div>
    }
    <ul class="list-group">
        <li class="list-group-item">Language: @Liff.Data?.Language</li>
        <li class="list-group-item">Type: @Liff.Data?.Context.Type</li>
        <li class="list-group-item">ViewType: @Liff.Data?.Context.ViewType</li>
        <li class="list-group-item">UserId: @Liff.Data?.Context.UserId</li>
        @if (@Liff.Data?.Context.Type == ContextType.Utou)
        {
        <li class="list-group-item">UtouId: @Liff.Data?.Context.UtouId</li>
        }
        else if (@Liff.Data?.Context.Type == ContextType.Room)
        {
        <li class="list-group-item">RoomId: @Liff.Data?.Context.RoomId</li>
        }
        else if (@Liff.Data?.Context.Type == ContextType.Group)
        {
        <li class="list-group-item">GroupId: @Liff.Data?.Context.GroupId</li>
        }
    </ul>
</div>

@functions{

    protected override async Task OnInitAsync()
    {
        try
        {
            await Liff.InitializeAsync(JSRuntime);
            await Liff.LoadProfileAsync();
            StateHasChanged();
        }
        catch (Exception e)
        {
            await JSRuntime.InvokeAsync<object>("liffInterop.alert", e.ToString());
        }
    }

    private async Task LoadProfileAsync()
    {
        try
        {
            await Liff.LoadProfileAsync();
            StateHasChanged();
        }
        catch (Exception e)
        {
            await JSRuntime.InvokeAsync<object>("liffInterop.alert", e.ToString());
        }
    }

}

```
