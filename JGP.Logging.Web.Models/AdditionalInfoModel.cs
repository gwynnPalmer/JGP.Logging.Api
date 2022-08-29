// ***********************************************************************
// Assembly         : JGP.Logging.Web.Models
// Author           : Joshua Gwynn-Palmer
// Created          : 08-29-2022
//
// Last Modified By : Joshua Gwynn-Palmer
// Last Modified On : 08-29-2022
// ***********************************************************************
// <copyright file="AdditionalInfoModel.cs" company="Joshua Gwynn-Palmer">
//     Joshua Gwynn-Palmer
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace JGP.Logging.Web.Models
{
    using System.Text.Json.Serialization;
    using Core;
    using Core.Commands;

    /// <summary>
    ///     Class AdditionalInfoModel.
    /// </summary>
    public class AdditionalInfoModel
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="AdditionalInfoModel" /> class.
        /// </summary>
        public AdditionalInfoModel()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="AdditionalInfoModel" /> class.
        /// </summary>
        /// <param name="additionalInfo">The additional information.</param>
        public AdditionalInfoModel(AdditionalInfo additionalInfo)
        {
            LogId = additionalInfo.LogId;
            Architecture = additionalInfo.Architecture;
            IpAddress = additionalInfo.IpAddress;
            MachineName = additionalInfo.MachineName;
            OperatingSystem = additionalInfo.OperatingSystem;
            OperatingSystemVersion = additionalInfo.OperatingSystemVersion;
        }

        /// <summary>
        ///     Gets or sets the log identifier.
        /// </summary>
        /// <value>The log identifier.</value>
        [JsonPropertyName("logId")]
        public Guid LogId { get; set; }

        /// <summary>
        ///     Gets or sets the architecture.
        /// </summary>
        /// <value>The architecture.</value>
        [JsonPropertyName("architecture")]
        public string Architecture { get; set; }

        /// <summary>
        ///     Gets or sets the ip address.
        /// </summary>
        /// <value>The ip address.</value>
        [JsonPropertyName("ipAddress")]
        public string IpAddress { get; set; }

        /// <summary>
        ///     Gets or sets the name of the machine.
        /// </summary>
        /// <value>The name of the machine.</value>
        [JsonPropertyName("machineName")]
        public string MachineName { get; set; }

        /// <summary>
        ///     Gets or sets the operating system.
        /// </summary>
        /// <value>The operating system.</value>
        [JsonPropertyName("operatingSystem")]
        public string OperatingSystem { get; set; }

        /// <summary>
        ///     Gets or sets the operating system version.
        /// </summary>
        /// <value>The operating system version.</value>
        [JsonPropertyName("OperatingSystemVersion")]
        public string OperatingSystemVersion { get; set; }

        /// <summary>
        ///     Gets the command.
        /// </summary>
        /// <returns>AdditionalInfoCommand.</returns>
        public AdditionalInfoCommand GetCommand()
        {
            return new AdditionalInfoCommand
            {
                Architecture = Architecture,
                IpAddress = IpAddress,
                LogId = LogId,
                MachineName = MachineName,
                OperatingSystem = OperatingSystem,
                OperatingSystemVersion = OperatingSystemVersion
            };
        }
    }
}