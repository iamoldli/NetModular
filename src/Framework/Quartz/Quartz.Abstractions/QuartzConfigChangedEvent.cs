using NetModular.Lib.Config.Abstractions;

namespace NetModular.Lib.Quartz.Abstractions
{
    public class QuartzConfigChangedEvent : IConfigChangeEvent<QuartzConfig>
    {
        private readonly IQuartzServer _quartzServer;

        public QuartzConfigChangedEvent(IQuartzServer quartzServer)
        {
            _quartzServer = quartzServer;
        }

        public void OnChanged(QuartzConfig newConfig, QuartzConfig oldConfig)
        {
            _quartzServer.Stop();
            if (newConfig.Enabled)
                _quartzServer.Restart();
        }
    }
}
