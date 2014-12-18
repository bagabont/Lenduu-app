using System;
using System.Collections.Generic;

namespace Humance.Push.AeroGear
{
    /// <summary>
    /// Aero gear Windows config.
    /// </summary>
    public class AeroGearWindowsConfig : IAeroGearConfig
    {
        public string VariantId { get; private set; }

        public string VariantSecret { get; private set; }

        public IList<string> Categories { get; private set; }

        public string Alias { get; private set; }

        public string DeviceToken { get; private set; }

        public string DeviceType { get; private set; }

        public string Platform { get; private set; }

        public string OsVersion { get; private set; }

        public AeroGearWindowsConfig()
        {
            Platform = "windows";
            //TODO - Fill the rest of the fields
        }
    }
}

