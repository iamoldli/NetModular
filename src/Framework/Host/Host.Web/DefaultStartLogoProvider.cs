using System;
using NetModular.Lib.Host.Web.Options;

namespace NetModular.Lib.Host.Web
{
    public class DefaultStartLogoProvider : IStartLogoProvider
    {
        public void Show(HostOptions options)
        {
            if (options.HideStartLogo)
                return;

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
            Console.WriteLine(@" ***************************************************************************************************************");
            Console.WriteLine(@" *                                                                                                             *");
            Console.WriteLine(@" *      $\    $$\             $$\     $$\      $$\                 $$\           $$\                           *");
            Console.WriteLine(@" *      $$$\  $$ |            $$ |    $$$\    $$$ |                $$ |          $$ |                          *");
            Console.WriteLine(@" *      $$$$\ $$ | $$$$$$\  $$$$$$\   $$$$\  $$$$ | $$$$$$\   $$$$$$$ |$$\   $$\ $$ | $$$$$$\   $$$$$$\        *");
            Console.WriteLine(@" *      $$ $$\$$ |$$  __$$\ \_$$  _|  $$\$$\$$ $$ |$$  __$$\ $$  __$$ |$$ |  $$ |$$ | \____$$\ $$  __$$\       *");
            Console.WriteLine(@" *      $$ \$$$$ |$$$$$$$$ |  $$ |    $$ \$$$  $$ |$$ /  $$ |$$ /  $$ |$$ |  $$ |$$ | $$$$$$$ |$$ |  \__|      *");
            Console.WriteLine(@" *      $$ |\$$$ |$$   ____|  $$ |$$\ $$ |\$  /$$ |$$ |  $$ |$$ |  $$ |$$ |  $$ |$$ |$$  __$$ |$$ |            *");
            Console.WriteLine(@" *      $$ | \$$ |\$$$$$$$\   \$$$$  |$$ | \_/ $$ |\$$$$$$  |\$$$$$$$ |\$$$$$$  |$$ |\$$$$$$$ |$$ |            *");
            Console.WriteLine(@" *      \__|  \__| \_______|   \____/ \__|     \__| \______/  \_______| \______/ \__| \_______|\__|            *");
            Console.WriteLine(@" *                                                                                                             *");
            Console.WriteLine(@" *                                                                                                             *");
            Console.Write(@" *                                   ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(@"启动成功，欢迎使用 NetModular ~");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(@"                                           *");
            Console.WriteLine(@" *                                                                                                             *");
            Console.Write(@" *                                   ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(@"接口地址：" + options.Urls);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(@"                                                   *");
            Console.WriteLine(@" *                                                                                                             *");
            Console.WriteLine(@" ***************************************************************************************************************");
            Console.WriteLine();
        }
    }
}
