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
using System.IO;
using Serilog.Events;
using Serilog.Formatting;
using Serilog.Formatting.Compact;
using Serilog.Sinks.PeriodicBatching;
using System.Collections.Generic;
using Serilog.Sinks.Nats.Sinks.Nats;
using System.Threading.Tasks;

namespace Serilog.Sinks.Nats
{
    /// <inheritdoc />
    /// <summary>
    /// Serilog Nats Sink - Lets you log to Nats using Serilog
    /// </summary>
    public class NatsSink : PeriodicBatchingSink
    {
        private readonly ITextFormatter _formatter;
        private readonly NatsLogClient _client;

        public NatsSink(
            NatsConfiguration configuration,
            ITextFormatter formatter) : base(configuration.BatchPostingLimit, configuration.Period)
        {
            _formatter = formatter ?? new CompactJsonFormatter();
            _client = new NatsLogClient(configuration);
        }

        protected override void EmitBatch(IEnumerable<LogEvent> events)
        {
            foreach (var logEvent in events)
            {
                var sw = new StringWriter();
                _formatter.Format(logEvent, sw);
                _client.Publish(sw.ToString());
            }
        }
    }
}
