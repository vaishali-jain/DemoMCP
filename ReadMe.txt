Building an MCP Server with the official C# SDK
===============================================

This sample follows the Microsoft walkthrough for creating a Model Context Protocol (MCP) server in C# with the official ModelContextProtocol SDK.

What this project does
----------------------
- Exposes MCP tools over stdio transport.
- Discovers tools automatically from the current assembly.
- Demonstrates simple example tools for waste classification and greener alternatives.

Prerequisites
-------------
- .NET SDK 10.0 or later
- A terminal or command prompt
- Optional: Visual Studio Code with MCP support

Create the project
------------------
If you are starting from scratch, the official walkthrough uses:

```bash
dotnet new console -n MyFirstMCP
cd MyFirstMCP
dotnet add package ModelContextProtocol --prerelease
dotnet add package Microsoft.Extensions.Hosting
```

This project already includes the relevant packages in DemoMCP.csproj.

Project structure
-----------------
- Program.cs: creates the host, adds the MCP server, and starts the stdio transport.
- EcoTool.cs: defines the MCP tools with the ModelContextProtocol attributes.
- DemoMCP.csproj: references the required NuGet packages.

How the server is wired up
--------------------------
The startup code in Program.cs does three important things:

1. Creates a host builder with Host.CreateApplicationBuilder(args).
2. Registers the MCP server and stdio transport with AddMcpServer() and WithStdioServerTransport().
3. Scans the assembly for tool classes with WithToolsFromAssembly().

The core startup code looks like this:

```csharp
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ModelContextProtocol.Server;
using System.ComponentModel;

var builder = Host.CreateApplicationBuilder(args);
builder.Logging.AddConsole(consoleLogOptions =>
{
    consoleLogOptions.LogToStandardErrorThreshold = LogLevel.Trace;
});

builder.Services
    .AddMcpServer()
    .WithStdioServerTransport()
    .WithToolsFromAssembly();

await builder.Build().RunAsync();
```

How to define tools
-------------------
Tools are discovered from classes marked with [McpServerToolType]. Each public static method that is decorated with [McpServerTool] becomes an MCP tool.

Example:

```csharp
[McpServerToolType]
public static class EchoTool
{
    [McpServerTool, Description("Echoes the message back to the client.")]
    public static string Echo(string message) => $"Hello from C#: {message}";
}
```

In this sample, EcoTool.cs defines the available tools:
- ClassifyWaste(string item)
- GreenAlternative(string item)

Run the server
--------------
From the project folder, build and run with:

```bash
dotnet build
dotnet run
```

Use it from VS Code
-------------------
To test the server from Visual Studio Code, add a server entry to your .vscode/mcp.json file:

```json
{
  "inputs": [],
  "servers": {
    "DemoMCP": {
      "type": "stdio",
      "command": "dotnet",
      "args": [
        "run",
        "--project",
        "C:\\path\\to\\DemoMCP\\DemoMCP.csproj"
      ]
    }
  }
}
```

Notes
-----
- The MCP C# SDK is still evolving, so APIs may change over time.
- This sample uses stdio transport, which is the standard transport for local MCP servers.
- To add more functionality, extend EcoTool.cs or create another tool class and keep the same registration pattern.
