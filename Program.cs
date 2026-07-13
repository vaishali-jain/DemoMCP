using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ModelContextProtocol.Server;
using System.ComponentModel;

// Create a new host builder for the application
var builder = Host.CreateApplicationBuilder(args);
builder.Logging.AddConsole(consoleLogOptions =>
{
    // Configure all logs to go to stderr
    consoleLogOptions.LogToStandardErrorThreshold = LogLevel.Trace;
});

builder.Services
    .AddMcpServer() // Add the MCP server to the DI container
    .WithStdioServerTransport() // Configure the server to use standard input/output for communication
    .WithToolsFromAssembly(); // Scan the current assembly for classes with the McpServerToolType attribute and register their methods with the McpServerTool attribute

await builder.Build().RunAsync(); // Build and run the host, which will start the MCP server and listen for incoming requests.