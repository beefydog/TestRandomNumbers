using Microsoft.AspNetCore.Components;
using BlazorWASM.Models;

namespace BlazorWASM.Controls
{
    public partial class BlockNumberGroup
    {
        [Parameter]
        public NumberGroup? ng { get; set; }

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