// ***********************************************************************
// Assembly         : JGP.Logging.Core
// Author           : Joshua Gwynn-Palmer
// Created          : 08-29-2022
//
// Last Modified By : Joshua Gwynn-Palmer
// Last Modified On : 08-29-2022
// ***********************************************************************
// <copyright file="IpDetector.cs" company="Joshua Gwynn-Palmer">
//     Joshua Gwynn-Palmer
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace JGP.Logging.Core.Builders;

/// <summary>
///     Class IpDetector.
/// </summary>
internal class IpDetector
{
    /// <summary>
    ///     The check URL
    /// </summary>
    private const string CheckUrl = @"https://checkip.amazonaws.com/";

    /// <summary>
    ///     The default ip
    /// </summary>
    private const string DefaultIp = "127.0.0.1";

    /// <summary>
    ///     The client
    /// </summary>
    private static readonly HttpClient Client = new();

    /// <summary>
    ///     Initializes a new instance of the <see cref="IpDetector" /> class.
    /// </summary>
    public IpDetector()
    {
    }

    /// <summary>
    ///     Gets the ip address.
    /// </summary>
    /// <returns>System.String.</returns>
    public static string GetIpAddress()
    {
        var publicIp = Client.GetStringAsync(CheckUrl)
            .GetAwaiter()
            .GetResult()
            .Replace("\n", string.Empty)
            .Trim();

        return string.IsNullOrWhiteSpace(publicIp)
            ? DefaultIp
            : publicIp;
    }
}