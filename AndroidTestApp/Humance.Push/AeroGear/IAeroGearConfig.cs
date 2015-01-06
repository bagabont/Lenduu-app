using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Humance.Push.AeroGear
{
    /// <summary>
    /// Represents configuration for the <see cref="AerogearClient"/> class.
    /// </summary>
    public interface IAeroGearConfig
    {
        /// <summary>
        /// Gets the variant identifier.
        /// </summary>
        /// <value>The variant identifier.</value>
        [JsonIgnore]
        string VariantId { get; }

        /// <summary>
        /// Gets the variant secret.
        /// </summary>
        /// <value>The variant secret.</value>
        [JsonIgnore]
        string VariantSecret { get; }

        /// <summary>
        /// Gets the categories.
        /// </summary>
        /// <value>The categories.</value>
        [JsonProperty("categories")]
        IList<string> Categories { get; }

        /// <summary>
        /// Gets the alias.
        /// </summary>
        /// <value>The alias.</value>
        [JsonProperty("alias")]
        string Alias { get; }

        /// <summary>
        /// Gets the device token.
        /// </summary>
        /// <value>The device token.</value>
        [JsonProperty("deviceToken")]
        string DeviceToken { get; }

        /// <summary>
        /// Gets the type of the device.
        /// </summary>
        /// <value>The type of the device.</value>
        [JsonProperty("deviceType")]
        string DeviceType { get; }

        /// <summary>
        /// Gets the platform.
        /// </summary>
        /// <value>The platform.</value>
        [JsonProperty("operatingSystem")]
        string Platform { get; }

        /// <summary>
        /// Gets the os version.
        /// </summary>
        /// <value>The os version.</value>
        [JsonProperty("osVersion")]
        string OsVersion { get; }
    }
}