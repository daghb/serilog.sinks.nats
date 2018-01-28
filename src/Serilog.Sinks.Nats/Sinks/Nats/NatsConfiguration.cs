// Copyright 2015 Serilog Contributors
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using MyNatsClient;

namespace Serilog.Sinks.Nats
{
    /// <summary>
    /// Configuration class for Nats
    /// </summary>
    public class NatsConfiguration
    {
        /// <summary>
        /// Gets or sets value indicating Nats subject to post message into
        /// </summary>
        public string Subject { get; set; }
        /// <summary>
        /// Client ID of Nats server
        /// </summary>
        public string ClientId { get; set; }
        /// <summary>
        /// Hostname of Nats server
        /// </summary>
        public string Host { get; set; }
        /// <summary>
        /// Listening port on Nats server
        /// </summary>
        public int Port { get; set; }
        //
        // Summary:
        //     Gets or sets value indicating if client should respond to server pings automatically.
        //     Default is true.
        public bool AutoRespondToPing { get; set; }
        //
        // Summary:
        //     Gets or sets value indicating if client should try and auto reconnect on failure.
        //     Default is false.
        public bool AutoReconnectOnFailure { get; set; }
        //
        // Summary:
        //     Gets or sets the credentials used when connecting against the hosts.
        //
        // Remarks:
        //     You can specify host specific credentials on each host.
        public Credentials Credentials { get; set; }
        //
        // Summary:
        //     Gets or sets value if verbose output should be used. Default is false.
        public bool Verbose { get; set; }
        //
        // Summary:
        //     Gets or sets value determining how the clients flush behavior should be when
        //     sending messages. E.g. when Pub or PubAsync is called. Default is Auto (will
        //     Flush after each Pub or PubAsync).
        public PubFlushMode PubFlushMode { get; set; }
        //
        // Summary:
        //     Gets or sets the default value to use for request timeout.
        public int RequestTimeoutMs { get; set; }
        //
        // Summary:
        //     Gets or sets MyNatsClient.ConnectionInfo.SocketOptions to use when creating the
        //     clients underlying socket via MyNatsClient.ISocketFactory.
        public SocketOptions SocketOptions { get; set; }
        /// <summary>
        /// Maximum bumber of events for each batch size
        /// </summary>
        public int BatchPostingLimit { get; set; }
        /// <summary>
        /// Maximum period to hold a batch
        /// </summary>
        public TimeSpan Period { get; set; }
    }
}
