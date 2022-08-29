// ***********************************************************************
// Assembly         : JGP.Logging.Api
// Author           : Joshua Gwynn-Palmer
// Created          : 08-28-2022
//
// Last Modified By : Joshua Gwynn-Palmer
// Last Modified On : 08-28-2022
// ***********************************************************************
// <copyright file="Program.cs" company="JGP.Logging.Api">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace JGP.Logging.Api
{
    /// <summary>
    ///     Class Program.
    /// </summary>
    public class Program
    {
        /// <summary>
        ///     Defines the entry point of the application.
        /// </summary>
        /// <param name="args">The arguments.</param>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        ///     Creates the host builder.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns>IHostBuilder.</returns>
        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host
                .CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(builder => { builder.UseStartup<Startup>(); });
        }
    }
}