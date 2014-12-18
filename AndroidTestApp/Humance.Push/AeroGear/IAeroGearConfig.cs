using System;
using System.Threading.Tasks;
using System.Collections.Generic;

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
        string VariantId { get; }

        /// <summary>
        /// Gets the variant secret.
        /// </summary>
        /// <value>The variant secret.</value>
        string VariantSecret { get; }

        /// <summary>
        /// Gets the categories.
        /// </summary>
        /// <value>The categories.</value>
        IList<string> Categories { get; }

        /// <summary>
        /// Gets the alias.
        /// </summary>
        /// <value>The alias.</value>
        string Alias { get; }

        /// <summary>
        /// Gets the device token.
        /// </summary>
        /// <value>The device token.</value>
        string DeviceToken { get; }

        /// <summary>
        /// Gets the type of the device.
        /// </summary>
        /// <value>The type of the device.</value>
        string DeviceType { get; }

        /// <summary>
        /// Gets the platform.
        /// </summary>
        /// <value>The platform.</value>
        string Platform { get; }

        /// <summary>
        /// Gets the os version.
        /// </summary>
        /// <value>The os version.</value>
        string OsVersion { get; }
    }
}