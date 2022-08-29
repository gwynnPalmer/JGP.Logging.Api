// ***********************************************************************
// Assembly         : JGP.Logging.Core
// Author           : Joshua Gwynn-Palmer
// Created          : 08-29-2022
//
// Last Modified By : Joshua Gwynn-Palmer
// Last Modified On : 08-29-2022
// ***********************************************************************
// <copyright file="AdditionalInfo.cs" company="Joshua Gwynn-Palmer">
//     Joshua Gwynn-Palmer
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace JGP.Logging.Core
{
    using System.Text.Json;
    using System.Text.Json.Serialization;
    using Commands;

    /// <summary>
    ///     Class AdditionalInfo.
    /// </summary>
    public class AdditionalInfo
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="AdditionalInfo" /> class.
        /// </summary>
        protected AdditionalInfo()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="AdditionalInfo" /> class.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <exception cref="System.ArgumentNullException">command</exception>
        public AdditionalInfo(AdditionalInfoCommand? command)
        {
            if (command == null) throw new ArgumentNullException(nameof(command));
            LogId = command.LogId;
            MachineName = command.MachineName;
            IpAddress = command.IpAddress;
            OperatingSystem = command.OperatingSystem;
            OperatingSystemVersion = command.OperatingSystemVersion;
            Architecture = command.Architecture;
        }

        /// <summary>
        ///     Gets or sets the log identifier.
        /// </summary>
        /// <value>The log identifier.</value>
        public Guid LogId { get; protected set; }

        /// <summary>
        ///     Gets or sets the name of the machine.
        /// </summary>
        /// <value>The name of the machine.</value>
        public string MachineName { get; protected set; }

        /// <summary>
        ///     Gets or sets the ip address.
        /// </summary>
        /// <value>The ip address.</value>
        public string IpAddress { get; protected set; }

        /// <summary>
        ///     Gets or sets the operating system.
        /// </summary>
        /// <value>The operating system.</value>
        public string OperatingSystem { get; protected set; }

        /// <summary>
        ///     Gets or sets the operating system version.
        /// </summary>
        /// <value>The operating system version.</value>
        public string OperatingSystemVersion { get; protected set; }

        /// <summary>
        ///     Gets or sets the architecture.
        /// </summary>
        /// <value>The architecture.</value>
        public string Architecture { get; protected set; }

        /// <summary>
        ///     Gets or sets the log item.
        /// </summary>
        /// <value>The log item.</value>
        public LogItem LogItem { get; set; }

        #region OVERRIDES & ESSENTIALS

        /// <summary>
        ///     Equalses the specified additional information.
        /// </summary>
        /// <param name="additionalInfo">The additional information.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool Equals(AdditionalInfo? additionalInfo)
        {
            if (additionalInfo is null) return false;
            return LogId == additionalInfo.LogId
                   && MachineName == additionalInfo.MachineName
                   && IpAddress == additionalInfo.IpAddress
                   && OperatingSystem == additionalInfo.OperatingSystem
                   && OperatingSystemVersion == additionalInfo.OperatingSystemVersion
                   && Architecture == additionalInfo.Architecture;
        }

        /// <summary>
        ///     Determines whether the specified <see cref="System.Object" /> is equal to this instance.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns><c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.</returns>
        public override bool Equals(object? obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;

            return obj is AdditionalInfo additionalInfo
                   && Equals(additionalInfo);
        }

        /// <summary>
        ///     Returns a hash code for this instance.
        /// </summary>
        /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
        public override int GetHashCode()
        {
            return LogId.GetHashCode();
        }

        /// <summary>
        ///     Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public override string ToString()
        {
            var options = new JsonSerializerOptions
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };
            return JsonSerializer.Serialize(this, options);
        }

        #endregion
    }
}