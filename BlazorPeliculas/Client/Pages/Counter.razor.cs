using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Threading.Tasks;
using static BlazorPeliculas.Client.Shared.MainLayout;
using MathNet.Numerics.Statistics;

namespace BlazorPeliculas.Client.Pages
{
    public partial class Counter
    {
        [Inject] protected IJSRuntime JS { get; set; }

        IJSObjectReference modulo;

        protected int currentCount = 0;

        [JSInvokable]
        public async Task IncrementCount()
        {
            var arreglo = new double[] { 1, 2, 3, 4, 5, 6, 7, };
            var max = arreglo.Maximum();
            var min = arreglo.Minimum();

            modulo = await JS.InvokeAsync<IJSObjectReference>("import", "./js/Counter.js");
            await modulo.InvokeVoidAsync("mostrarAlerta",$"el maximo es{max} y el minimo {min}");

            currentCount++;
           
        }
       
    }
}
