// ***********************************************************************
// Assembly         : JGP.Logging.Core
// Author           : Joshua Gwynn-Palmer
// Created          : 08-29-2022
//
// Last Modified By : Joshua Gwynn-Palmer
// Last Modified On : 08-29-2022
// ***********************************************************************
// <copyright file="AdditionalInfoCommand.cs" company="Joshua Gwynn-Palmer">
//     Joshua Gwynn-Palmer
// </copyright>
// <summary></summary>
// ***********************************************************************

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
namespace JGP.Logging.Core.Commands
{
    /// <summary>
    ///     Class AdditionalInfoCommand.
    /// </summary>
    public class AdditionalInfoCommand
    {
        /// <summary>
        ///     Gets or sets the log identifier.
        /// </summary>
        /// <value>The log identifier.</value>
        public Guid LogId { get; set; }

        /// <summary>
        ///     Gets or sets the name of the machine.
        /// </summary>
        /// <value>The name of the machine.</value>
        public string MachineName { get; set; }

        /// <summary>
        ///     Gets or sets the ip address.
        /// </summary>
        /// <value>The ip address.</value>
        public string IpAddress { get; set; }

        /// <summary>
        ///     Gets or sets the operating system.
        /// </summary>
        /// <value>The operating system.</value>
        public string OperatingSystem { get; set; }

        /// <summary>
        ///     Gets or sets the operating system version.
        /// </summary>
        /// <value>The operating system version.</value>
        public string OperatingSystemVersion { get; set; }

        /// <summary>
        ///     Gets or sets the architecture.
        /// </summary>
        /// <value>The architecture.</value>
        public string Architecture { get; set; }
    }
}
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.