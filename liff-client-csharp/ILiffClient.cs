using System;
using System.Threading.Tasks;
using LineDC.Liff.Data;
using Microsoft.JSInterop;

namespace LineDC.Liff
{
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
}