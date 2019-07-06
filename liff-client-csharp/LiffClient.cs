using LineDC.Liff.Data;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace LineDC.Liff
{
    public class LiffClient : ILiffClient
    {
        public bool Initialized { get; protected set; }
        protected IJSRuntime JSRuntime { get; set; }

        public LiffData Data { get; protected set; }

        public Profile Profile { get; protected set; }

        public LiffClient()
        { }

        public void Reset()
        {
            Data = null;
            Profile = null;
            Initialized = false;
        }

        public async Task InitializeAsync(IJSRuntime jSRuntime)
        {
            JSRuntime = jSRuntime;
            if (Initialized) { return; }
            var json = await JSRuntime.InvokeAsync<string>("liffInterop.init");
            Data = JsonConvert.DeserializeObject<LiffData>(json);
            Initialized = true;
        }

        public async Task LoadProfileAsync()
            => Profile = await JSRuntime.InvokeAsync<Profile>("liff.getProfile");

        public async Task SendMessagesAsync(object messages)
            => await JSRuntime.InvokeAsync<object>("liff.sendMessages", messages);

        public async Task OpenWindowAsync(string url, bool external)
            => await JSRuntime.InvokeAsync<object>("liff.openWindow", new { url, external });

        public async Task CloseWindowAsync()
            => await JSRuntime.InvokeAsync<object>("liff.closeWindow");

        public Task<string> GetAccessTokenAsync()
            => JSRuntime.InvokeAsync<string>("liff.getAccessToken");

    }
}
