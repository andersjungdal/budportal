using System;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace BlazorBusinessLogic.Extensions
{
    public static class IJSRuntiomeExtensions
    {
        public static ValueTask<object> SaveAs(this IJSRuntime js, string filename, byte[] data)
            => js.InvokeAsync<object>(
                "FileIO.SaveXL",
                filename,
                Convert.ToBase64String(data));
    }
}