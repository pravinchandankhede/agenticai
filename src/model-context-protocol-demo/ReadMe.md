This contains the outline and description of design for this demo application.

# Model Context Protocol
This is a demo project that demonstrate the concept of using [model context protocol](https://modelcontextprotocol.io/introduction) with C#. It uses the core Nuget packages [ModelContextProtocol](https://packages.nuget.org/packages/ModelContextProtocol/0.1.0-preview.10)
MCP helps you connect with variety of sources and expose them in a way which is similar to other MCP servers. This way the client code is simplified and it can focus on implementing the core lgoic rather than trying to integrate the Agent with all varied data sources.

**In this demo, I will show how to expose your organizations APIs as Tools using MCP server technique.**

## Banking Service
This is  simple banking service which provides 2 operations
 - **Get Balances**: This return a list of balances for various accounts.
 - **Get Balance**: This returns balance for a given customer.

*Note*: For simplicity the banking service implements a in memory list of names and amount.

## Banking MCP Server
This is a MCP server exposed for the banking service. It demonstrates how to implement a MCP server for your organizations APIs. This project how you can build a MCP server around your APIs and then expose them using a ASP.NET Core runtime making it available over a endpoint. This endpoint can then be utilized by a client to list down all the tools available with MCP server.



