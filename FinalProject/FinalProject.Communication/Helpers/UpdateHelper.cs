using FinalProject.Data.Enums;

namespace FinalProject.Communication.Helpers
{
    internal class UpdateHelper
    {
        private long _lastUpdated;
        private long _updateSpeed;

        public UpdateHelper()
        {
            _updateSpeed = UpdateSpeedHelper.GetUpdateSpeedInSeconds();
        }

        public bool CanUpdate()
        {
            var now = DateTimeOffset.Now.ToUnixTimeSeconds();

            if (_lastUpdated + _updateSpeed >= now)
            {
                return false;
            }

            _lastUpdated = now;
            _updateSpeed = UpdateSpeedHelper.GetUpdateSpeedInSeconds();
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
