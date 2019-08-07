# liff-client-csharp
C# wrapper of LIFF client API for use in Blazor applications.

## Demo Site 
Published on Github Pages
https://line-developer-community.github.io/liff-client-csharp/

## Usage

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
