using FinalProject.Core.Extensions;

namespace FinalProject.Core.Helpers
{
    public class UpdateHelper
    {
        private long _lastUpdate;
        private long _updateSpeed;

        public UpdateHelper()
        {
            _updateSpeed = Preferences.Default.GetUpdateEnviromentSpeed();
        }

        public bool CanUpdate()
        {
            var now = DateTimeOffset.Now.ToUnixTimeSeconds();

            if (_lastUpdate + _updateSpeed >= now)
            {
                return false;
            }

            _lastUpdate = now;
            _updateSpeed = Preferences.Default.GetUpdateEnviromentSpeed();
            return true;
        }
    }
}
