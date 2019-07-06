# liff-app-on-blazor
C# wrapper of LIFF client API for use in Blazor applications.

## Demo Site 
Published on Github Pages
https://pierre3.github.io/liff-app-on-blazor/

## Usage

```cs
public interface ILiff
{
    bool Initialized { get; }
    LiffData Data { get; }
    string Error { get; }
    Profile Profile { get; }

    event EventHandler<InitErrorEventArgs> InitError;
    event EventHandler<InitSuccessEventArgs> InitSuccess;

    Task CloseWindowAsync();
    Task<string> GetAccessTokenAsync();
    Task InitializeAsync(IJSRuntime jSRuntime);
    Task LoadProfileAsync();
    Task OpenWindowAsync(string url, bool external);
    void Reset();
    Task SendMessagesAsync(object messages);
}
```

```cs
public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<ILiff, Liff>();
    }

    public void Configure(IComponentsApplicationBuilder app)
    {
        app.AddComponent<App>("app");
    }
}
```

```cshtml
@page "/"
@inject ILiff Liff
@inject IJSRuntime JSRuntime

<div class="card" style="width: 20rem;">
    @if (Liff.Profile != null)
    {
    <img class="card-img" src="@Liff.Profile?.PictureUrl" alt="Loading image..." onload="@StateHasChanged" />
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
        await InitializeAsync();
        await LoadProfileAsync();
    }

    private async Task InitializeAsync()
    {
        try
        {
            await Liff.InitializeAsync(JSRuntime);
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
