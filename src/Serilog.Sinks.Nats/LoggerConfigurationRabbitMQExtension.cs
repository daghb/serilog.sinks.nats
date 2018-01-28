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
using Serilog.Configuration;
using Serilog.Formatting;
using Serilog.Sinks.Nats;
using Serilog.Sinks.Nats.Sinks.Nats;

namespace Serilog
{
    public static class LoggerConfigurationNatsExtension
    {
        public static LoggerConfiguration Nats(
            this LoggerSinkConfiguration loggerConfiguration,
            NatsConfiguration natsConfiguration,
            ITextFormatter formatter,
            IFormatProvider formatProvider = null)
        {
            if (loggerConfiguration == null) throw new ArgumentNullException(nameof(loggerConfiguration));
            if (natsConfiguration == null) throw new ArgumentNullException(nameof(natsConfiguration));

            // calls overloaded extension method
            return loggerConfiguration.Nats(
                natsConfiguration.ClientId,
                natsConfiguration.Subject,
                natsConfiguration.Host,
                natsConfiguration.Port,
                natsConfiguration.AutoRespondToPing,
                natsConfiguration.AutoReconnectOnFailure,
                natsConfiguration.Credentials,
                natsConfiguration.PubFlushMode,
                natsConfiguration.RequestTimeoutMs,
                natsConfiguration.SocketOptions,
                natsConfiguration.Verbose,
                natsConfiguration.BatchPostingLimit,
                (int)natsConfiguration.Period.TotalMilliseconds,
                formatter,
                formatProvider);
        }

        private static LoggerConfiguration Nats(
            this LoggerSinkConfiguration loggerConfiguration,
            string clientId,
            string subject,
            string host,
            int port = 4222,
            bool autoRespondToPing = true,
            bool autoReconnectOnFailure = true,
            Credentials credentials = null,
            PubFlushMode pubFlushMode = PubFlushMode.Auto,
            int requestTimeoutMs = 10000,
            SocketOptions socketOptions = null,
            bool verbose = false,
            int batchSizeLimit = 50,
            int periodLimitInMs = 2000,
            ITextFormatter formatter = null,
            IFormatProvider formatProvider = null)
        {
            // guards
            if (loggerConfiguration == null) throw new ArgumentNullException(nameof(loggerConfiguration));
            if (string.IsNullOrEmpty(host)) throw new ArgumentOutOfRangeException(nameof(host), "host cannot be 'null' or and empty string.");
            if (string.IsNullOrEmpty(clientId)) throw new ArgumentOutOfRangeException(nameof(clientId), "clientId cannot be 'null' or and empty string.");
            if (string.IsNullOrEmpty(subject)) throw new ArgumentOutOfRangeException(nameof(subject), "subject cannot be 'null' or and empty string.");
            if (port <= 0 || port > 65535) throw new ArgumentOutOfRangeException(nameof(port), "port must be in a valid range (1 and 65535)");

            // setup configuration
            var config = new NatsConfiguration
            {
                Subject = subject,
                ClientId = clientId,
                Host = host,
                Port = port,
                AutoReconnectOnFailure = autoReconnectOnFailure,
                AutoRespondToPing = autoRespondToPing,
                Credentials = credentials,
                PubFlushMode = pubFlushMode,
                RequestTimeoutMs = requestTimeoutMs,
                SocketOptions = socketOptions,
                Verbose = verbose,
                BatchPostingLimit = batchSizeLimit,
                Period = TimeSpan.FromMilliseconds(periodLimitInMs)
            };
            
            return
                loggerConfiguration
                    .Sink(new NatsSink(config, formatter, formatProvider));
        }
    }
}
