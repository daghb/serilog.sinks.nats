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
using System.Security.Cryptography;
using MyNatsClient;
using MyNatsClient.Encodings.Json;
using Serilog.Sinks.Nats.Sinks.Nats;

namespace Serilog.Sinks.Nats
{
    /// <summary>
    /// NatsLogClient - this class is the engine that lets you send messages to Nats
    /// </summary>
    public class NatsLogClient : IDisposable
    {
        // configuration member
        private readonly NatsConfiguration _config;
        private INatsClient _natsClient;
        private readonly string _subject;

        /// <summary>
        /// Constructor for NatsLogClient
        /// </summary>
        /// <param name="configuration">mandatory</param>
        public NatsLogClient(NatsConfiguration configuration)
        {
            // load configuration
            _config = configuration;
            _subject = configuration.Subject;

            // initialize 
            InitializeEndpoint();
        }

        /// <summary>
        /// Private method, that must be run for the client to work.
        /// <remarks>See constructor</remarks>
        /// </summary>
        private void InitializeEndpoint()
        {
            _natsClient = new NatsClient(_config.ClientId, new ConnectionInfo(_config.Host, _config.Port)
            {
                AutoReconnectOnFailure = _config.AutoReconnectOnFailure,
                AutoRespondToPing = _config.AutoRespondToPing,
                Credentials = _config.Credentials,
                PubFlushMode = _config.PubFlushMode,
                RequestTimeoutMs = _config.RequestTimeoutMs,
                SocketOptions = _config.SocketOptions,
                Verbose = _config.Verbose
            });
        }

        /// <summary>
        /// Publishes a message to Nats
        /// </summary>
        /// <param name="message"></param>
        public void Publish(string message)
        {
            // push message to exchange
            _natsClient.PubAsJsonAsync(_subject, message).ConfigureAwait(false);
        }

        public void Dispose()
        {
            if (_natsClient.IsConnected)
                _natsClient.Disconnect();
        }
    }
}