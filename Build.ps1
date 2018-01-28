if(Test-Path .\artifacts) { Remove-Item .\artifacts -Force -Recurse }

dotnet restore .\src\Serilog.Sinks.Nats\Serilog.Sinks.Nats.csproj
dotnet build .\src\Serilog.Sinks.Nats\Serilog.Sinks.Nats.csproj

echo "RUNNING dotnet pack .\src\Serilog.Sinks.Nats\Serilog.Sinks.Nats.csproj -c Release -o .\artifacts"
dotnet pack .\src\Serilog.Sinks.Nats\Serilog.Sinks.Nats.csproj -c Release -o .\artifacts
