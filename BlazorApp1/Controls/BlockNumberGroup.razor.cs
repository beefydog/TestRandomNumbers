using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
using Microsoft.JSInterop;
using BlazorApp1;
using BlazorApp1.Shared;
using BlazorApp1.Controls;
using BlazorApp1.Models;

namespace BlazorApp1.Controls
{
    public partial class BlockNumberGroup
    {
        [Parameter]
        public NumberGroup ng { get; set; }

        protected override void OnInitialized()
        {
        }

        [Parameter]
        public EventCallback<NumberGroup> OnItemChange { get; set; }

        private async Task HandleChange()
        {
            await OnItemChange.InvokeAsync(ng);
        }
    }
}