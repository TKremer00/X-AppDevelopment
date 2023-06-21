using FinalProject.Core.Extensions;
using FinalProject.Data.Enums;

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

        public static IReadOnlyDictionary<Characteristics, UpdateHelper> GenerateAllHelpers()
        {
            var updateHelpers = new Dictionary<Characteristics, UpdateHelper>();
            foreach (var characteristic in Enum.GetValues<Characteristics>())
            {
                updateHelpers.Add(characteristic, new UpdateHelper());
            }

            return updateHelpers;
        }
    }
}
