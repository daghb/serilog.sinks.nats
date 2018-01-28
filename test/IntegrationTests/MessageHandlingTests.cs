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
using System.Threading;
using MyNatsClient;
using Serilog;
using Serilog.Formatting.Json;
using Serilog.Sinks.Nats;
using Xunit;

namespace IntegrationTests
{
    public class MessageHandlingTests
    {
        /// <summary>
        /// To run, start docker container:
        /// docker run -ti -p 4222:4222 nats -DV --user test --pass test
        /// </summary>
        [Fact (Skip = "Must be run manually with docker image running. See comments for instructions.")]
        public void IsMessageReceivedAfterLoggingIsDone()
        {
            var config = new NatsConfiguration
            {
                Host = "localhost",
                Port = 4222,
                ClientId = "Serilog.Sinks.Nats.IntegrationTests.Publisher",
                Subject = "IntegrationTest.TestSubject",
                RequestTimeoutMs = 1000,
                AutoReconnectOnFailure = true,
                AutoRespondToPing = true,
                BatchPostingLimit = 1,
                Credentials = new Credentials("test", "test"),
                Period = TimeSpan.FromMilliseconds(100),
                PubFlushMode = PubFlushMode.Auto,
                SocketOptions = new SocketOptions {ConnectTimeoutMs = 1000, ReceiveBufferSize = 50000, ReceiveTimeoutMs = 5000, SendBufferSize = 50000, SendTimeoutMs = 5000},
                Verbose = false
            };

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .Enrich.FromLogContext()
                .WriteTo.Nats(config, new JsonFormatter())
                .CreateLogger();

            var cnInfo = new ConnectionInfo("localhost") {Credentials = new Credentials("test", "test"), RequestTimeoutMs = 30000};
            var client = new NatsClient("Serilog.Sinks.Nats.IntegrationTests.Subscriber", cnInfo);
            client.Connect();

            var foundMessage = false;

            client.SubWithHandlerAsync(config.Subject, msg => { foundMessage = true; }).ConfigureAwait(false);
            Log.Debug("Test message");

            Thread.Sleep(1000);
            client.Disconnect();

            Assert.True(foundMessage, "No log message received within timeout period");
        }
    }
}
