using System;
using System.IO;
using ElectronNET.API.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Nm.Lib.WebHost.Core;

namespace Nm.Module.Blog.WebHost.Electron
{
    public class Startup : StartupAbstract
    {
        public Startup(IHostingEnvironment env) : base(env)
        {
        }

        public override void Configure(IApplicationBuilder app)
        {
            base.Configure(app);

            Bootstrap();
        }

        public async void Bootstrap()
        {
            ElectronNET.API.Electron.App.ReleaseSingleInstanceLock();
            var options = new BrowserWindowOptions
            {
                Icon = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "wwwroot/images/favicon.ico"),
                Title = "文档管理系统",
                Show = false
            };
            var win = await ElectronNET.API.Electron.WindowManager.CreateWindowAsync(options);
            win.OnReadyToShow += () =>
            {
                win.Maximize();
                win.Show();
#if DEBUG
                win.WebContents.OpenDevTools();
#endif
            };
        }
    }
}
