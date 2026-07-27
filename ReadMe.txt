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
dotnet new console -n DemoMCP
cd DemoMCP
dotnet add package ModelContextProtocol
dotnet add package Microsoft.Extensions.Hosting

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

The core startup code is in Program.cs file

How to define tools
-------------------
Tools are discovered from classes marked with [McpServerToolType]. Each public static method that is decorated with [McpServerTool] becomes an MCP tool.
McpServerTool has a Description which will be fed into any client connecting
to the server. This description helps the client determine which tool to call.


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
From the project folder, right click the csproj file and hit Build or
build and run with:

```bash
dotnet build
dotnet run
```

Use it from VS Code
-------------------
To test the server from Visual Studio Code, add a server entry to your .vscode/mcp.json file. Then start the mcp server. The mcp server should be now discoverable in github copliot tools window

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
