﻿using Despro.Blazor.Base.Components;
using Despro.Blazor.Message.MessageGenerals;
using Despro.Blazor.Message.MessageRepository.Services;
using Microsoft.AspNetCore.Components;

namespace Despro.Blazor.Message.Components.Toast
{
    public partial class ToastView : BaseComponent, IDisposable
    {
        [Inject] private ToastService ToastService { get; set; }
        [Parameter] public ToastModel Toast { get; set; }

        private CountdownTimer _countdownTimer;
        private int _progress = 100;

        protected override void OnInitialized()
        {
            if (Toast.Options.AutoClose)
            {
                _countdownTimer = new CountdownTimer(Toast.Options.Delay * 1000);
                _countdownTimer.OnTick += CalculateProgress;
                _countdownTimer.Start();
            }
        }

        private async void CalculateProgress(int percentComplete)
        {
            _progress = 100 - percentComplete;
            if (percentComplete >= 100)
            {
                await Close();
            }
            await InvokeAsync(StateHasChanged);
        }

        public async Task Close()
        {
            await ToastService.RemoveToastAsync(Toast);
        }

        public void Dispose()
        {
            _countdownTimer?.Dispose();
            _countdownTimer = null;
        }
    }

}

