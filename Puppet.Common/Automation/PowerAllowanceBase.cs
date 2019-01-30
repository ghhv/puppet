﻿using Puppet.Common.Devices;
using Puppet.Common.Events;
using Puppet.Common.Automation;
using Puppet.Common.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Puppet.Common.Automation
{
    /// <summary>
    /// IAutomation handler for turning off switch devices
    /// after a certain number of minutes.
    /// </summary>
    public abstract class PowerAllowanceBase : AutomationBase
    {
        public int Minutes { get; set; }

        public PowerAllowanceBase(HomeAutomationPlatform hub, HubEvent evt) : base(hub, evt)
        {

        }

        public override async void Handle(CancellationToken token)
        {
            if (_evt.value == "on")
            {
                await Task.Delay(TimeSpan.FromMinutes(this.Minutes));
                if (token.IsCancellationRequested)
                {
                    Console.Write($"{DateTime.Now} Instance of {this.GetType()} resumed after {this.Minutes} minutes, but task was cancelled.");
                    return;
                }
                SwitchRelay relay = _hub.GetDeviceById<SwitchRelay>(_evt.deviceId) as SwitchRelay;
                relay.Off();
            }
        }
    }
}