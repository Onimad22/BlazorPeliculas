using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Threading.Tasks;
using static BlazorPeliculas.Client.Shared.MainLayout;

namespace BlazorPeliculas.Client.Pages
{
    public partial class Counter
    {
        [Inject] protected IJSRuntime JS { get; set; }

        IJSObjectReference modulo;

        protected int currentCount = 0;
        static int currentCountStatic = 0;

        [JSInvokable]
        public async Task IncrementCount()
        {
            modulo = await JS.InvokeAsync<IJSObjectReference>("import", "./js/Counter.js");
            await modulo.InvokeVoidAsync("mostrarAlerta","hola mundo");

            currentCount++;
            currentCountStatic++;
            await JS.InvokeVoidAsync("pruebaPuntoNetStatic");
        }

        

        protected async Task IncrementCountJavascript()
        {
            await JS.InvokeVoidAsync("pruebaPuntoNetInstancia",
                DotNetObjectReference.Create(this));
        }
    }
}
