# Table of Contents

1. [Agentic AI Samples](#agentic-ai-samples)
2. [Semantic Kernel](#semantic-kernel)
   - [Code](https://github.com/pravinchandankhede/agenticai/tree/main/src/SemanticKernel)
3. [Banking MAS](#banking-mas)
   - [Code](https://github.com/pravinchandankhede/agenticai/tree/main/src/Banking-Multi-Agent-System-Demo)
4. [Model Context Protocol](#model-context-protocol)
   - **Basic Protocol Working Sample**
     - [Code](https://github.com/pravinchandankhede/agenticai/tree/main/src/model-context-protocol-demo)
   - **Basic Client with Azure OpenAI Chat Completion Integration**
     - [Code](https://github.com/pravinchandankhede/agenticai/tree/main/src/model-context-protocol-demo/Clients/MCPAzureOpenAIClient)
   - **Client using Semantic Kernel SDK, Plugin Integration with MCP Tools**
     - [Code](https://github.com/pravinchandankhede/agenticai/tree/main/src/model-context-protocol-demo/Clients/MCPSemanticKernelClient)

# Agentic AI Samples
This repo contains demo samples on variety of topics using languages like C#, Typescript, Angular, XML, JSON

## Semantic Kernel
This shows the usage of Microsoft Semantic Kernel framework for building the AI Agents.
[Code](https://github.com/pravinchandankhede/agenticai/tree/main/src/SemanticKernel)

## Banking MAS
This is a demo of multi agent system implementation for a banking scenario. This implements a loan application scenario and how varios agents gets involved in doing credit, policy, account checks. This is a distributed multi agent system and uses Azure Service Bus Topic based communication.
[Code](https://github.com/pravinchandankhede/agenticai/tree/main/src/Banking-Multi-Agent-System-Demo)

## Model Context Protocol

### Basic protocol working sample
In this demo, I have created a Banking service and the operations are exposed as Tools throgh a MCP Server. This server is hosted using ASP.NET Core setup. The MCP server is then accessed through a .NET Console Client that uses ModelContextProtocol nuget library. This client lists down all the tools available with server and then call one of the tool, pasting response on console window.
[Code](https://github.com/pravinchandankhede/agenticai/tree/main/src/model-context-protocol-demo)

### Basic client that uses Azure OpenAI ChatCompletion classes and consumes Tools exposed using MCP library.
This demo shows how to create a .NET Console client that provides an MCP tool integration with Azure OpenAI model using ChatCompletion APIs. This server is hosted using ASP.NET Core setup. The MCP server is then accessed through a .NET Console Client that uses ModelContextProtocol and Azure OpenAI nuget library. This client lists down all the tools available with server and then uses a Tool calling feature of LLM.
[Code](https://github.com/pravinchandankhede/agenticai/tree/main/src/model-context-protocol-demo/Clients/MCPAzureOpenAIClient).

### Client using Semantic Kernel SDK, Plugin Integration with MCP Tools
This demo shows how to get a list of MCP tools from a MCP server and then setup the Semantic Kernel Plugins using these tools. It also integrates with Azure OpenAI LLM and performs a user query operation. It uses the Banking service, MCP server created in above demo projects. 
[Code](https://github.com/pravinchandankhede/agenticai/tree/main/src/model-context-protocol-demo/Clients/MCPSemanticKernelClient)
