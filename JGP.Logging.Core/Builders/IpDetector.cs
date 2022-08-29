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

using System.Net;
using System.Net.Sockets;

/// <summary>
///     Class IpDetector.
/// </summary>
internal class IpDetector
{
    /// <summary>
    ///     The default ip
    /// </summary>
    private const string DefaultIp = "127.0.0.1";

    /// <summary>
    ///     The host
    /// </summary>
    private const string Host = "8.8.8.8";

    /// <summary>
    ///     The port
    /// </summary>
    private const int Port = 65530;

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
        try
        {
            using var socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0);
            socket.Connect(Host, Port);
            var endPoint = socket.LocalEndPoint as IPEndPoint;
            return endPoint?.Address.ToString() ?? DefaultIp;
        }
        catch (Exception)
        {
            return DefaultIp;
        }
    }
}