﻿using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace BlazorPeliculas.Client.Pages
{
    public partial class Counter
    {
        [Inject] ServicioSingleton singleton { get; set; }
        [Inject] ServicioTransient transient { get; set; }
        [Inject] protected IJSRuntime JS { get; set; }

        private int currentCount = 0;
        static int currentCountStatic = 0;

        private async void IncrementCount()
        {
            currentCount++;
            singleton.Valor = currentCount;
            transient.Valor = currentCount;
            currentCountStatic++;
            await JS.InvokeVoidAsync("pruebaPuntoNetStatic");
        }

        [JSInvokable]
        public static Task<int> ObtenerCurrentCount()
        {
            return Task.FromResult(currentCountStatic);
        }
    }
}
