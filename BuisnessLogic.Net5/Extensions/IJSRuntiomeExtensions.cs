using System;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace BlazorBuisnessLogic.Net5.Extensions
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