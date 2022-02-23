using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace BlazorPeliculas.Client.Helpers
{
    public static class IJSRuntimeExtensionMethods
    {
        public static async ValueTask<bool> confirm(this IJSRuntime js, string mensage)
        {
            await js.InvokeVoidAsync("console.log", "prueba de console.log");
            return await js.InvokeAsync<bool>("confirm", mensage);
        }
    }
}
