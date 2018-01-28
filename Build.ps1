if(Test-Path .\artifacts) { Remove-Item .\artifacts -Force -Recurse }

dotnet restore .\src\Serilog.Sinks.Nats\Serilog.Sinks.Nats.csproj
dotnet build .\src\Serilog.Sinks.Nats\Serilog.Sinks.Nats.csproj

echo "Running dotnet pack"
dotnet pack .\src\Serilog.Sinks.Nats\Serilog.Sinks.Nats.csproj -c Release -o .\artifacts
