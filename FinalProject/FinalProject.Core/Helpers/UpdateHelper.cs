namespace FinalProject.Core.Helpers
{
    internal class UpdateHelper
    {
        private long _lastUpdate;
        private long _updateSpeed;

        public UpdateHelper()
        {
            _updateSpeed = SettingsHelper.GetUpdateEnviromentSpeedsSetting();
        }

        public bool CanUpdate()
        {
            var now = DateTimeOffset.Now.ToUnixTimeSeconds();

            if (_lastUpdate + _updateSpeed >= now)
            {
                return false;
            }

            _lastUpdate = now;
            _updateSpeed = SettingsHelper.GetUpdateEnviromentSpeedsSetting();
            return true;
        }
    }
}
