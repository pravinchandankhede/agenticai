# Agentic AI Samples

[![Build Status](https://github.com/pravinchandankhede/agenticai/actions/workflows/dotnet.yml/badge.svg)](https://github.com/pravinchandankhede/agenticai/actions/workflows/dotnet.yml)

This repository contains demo samples showcasing various agentic AI implementations using C#, TypeScript, Angular, and other technologies. The samples demonstrate building AI agents, orchestration patterns, multi-agent systems, and integration with different frameworks.

## Table of Contents

1. [Agent Framework Sample](#agent-framework-sample)
   - [Simple Agent](#simple-agent)
   - [Image Agent](#image-agent)
2. [Agent Orchestration](#agent-orchestration)
   - [Sequential Orchestration](#sequential-orchestration)
   - [Concurrent Orchestration](#concurrent-orchestration)
   - [Group Chat Orchestration](#group-chat-orchestration)
   - [Handoff Orchestration](#handoff-orchestration)
3. [Semantic Kernel](#semantic-kernel)
4. [Banking Multi-Agent System (MAS)](#banking-multi-agent-system-mas)
5. [Customer Service Agent](#customer-service-agent)
6. [Model Context Protocol (MCP)](#model-context-protocol-mcp)
   - [Basic Protocol Working Sample](#basic-protocol-working-sample)
   - [Azure OpenAI Chat Completion Integration](#azure-openai-chat-completion-integration)
   - [Semantic Kernel Plugin Integration](#semantic-kernel-plugin-integration)

---

## Agent Framework Sample

Demonstrates the usage of Microsoft Agents AI framework for building AI agents with Azure OpenAI integration. This sample includes multiple agent implementations and a shared helper library for common agent creation patterns.

**[View Code](https://github.com/pravinchandankhede/agenticai/tree/main/src/AgentFrameworkSample)**

### Simple Agent

Basic agent implementation demonstrating fundamental agent creation patterns with joke-telling capabilities.

**Key Features:**
- Agent creation using `Microsoft.Agents.AI` framework
- Azure OpenAI integration with AzureKeyCredential authentication
- Configurable agent instructions, names, and descriptions
- Multimodal content processing (text and images via URI)
- Simple conversational interactions

**[View Code](https://github.com/pravinchandankhede/agenticai/tree/main/src/AgentFrameworkSample/SimpleAgent)**

### Image Agent

Specialized agent for image analysis and description using multimodal AI capabilities.

**Key Features:**
- Image understanding and description
- Multimodal content processing with vision capabilities
- Utilizes shared `AgentHelper` library for consistent agent creation
- Azure OpenAI vision model integration

**[View Code](https://github.com/pravinchandankhede/agenticai/tree/main/src/AgentFrameworkSample/ImageAgent)**

---

## Agent Orchestration

Demonstrates various agent orchestration patterns using Microsoft Semantic Kernel. These samples show how to coordinate multiple agents to work together effectively.

### Sequential Orchestration
Shows how to run multiple agents in sequence, where each agent processes the output of the previous agent.

**[View Code](https://github.com/pravinchandankhede/agenticai/tree/main/src/AgentOrchestration/SequentialOrchestration)**

### Concurrent Orchestration
Demonstrates running multiple agents in parallel to process tasks simultaneously and improve performance.

**[View Code](https://github.com/pravinchandankhede/agenticai/tree/main/src/AgentOrchestration/ConcurrentOrchestration)**

### Group Chat Orchestration
Implements a group chat pattern where multiple agents collaborate and communicate with each other to solve complex tasks.

**[View Code](https://github.com/pravinchandankhede/agenticai/tree/main/src/AgentOrchestration/GroupChatOrchestration)**

### Handoff Orchestration
Shows how to implement agent handoff patterns where one agent can transfer control to another specialized agent based on context.

**[View Code](https://github.com/pravinchandankhede/agenticai/tree/main/src/AgentOrchestration/HandoffOrchestration)**

---

## Semantic Kernel

Demonstrates the usage of Microsoft Semantic Kernel framework for building AI agents with Azure OpenAI service integration.

**The samples are based on the [MS learning path](https://learn.microsoft.com/en-us/credentials/applied-skills/develop-ai-agents-using-microsoft-azure-openai-and-semantic-kernel/)**

**Key Features:**
1. Simple Call - Basic LLM invocation
2. Plugin Call - Using plugins to extend agent capabilities
3. Prompt Template - Working with prompt templates
4. Custom Prompt - Creating custom prompts
5. Persona Prompt - Implementing persona-based agents
6. Save to File - Persisting results

**[View Code](https://github.com/pravinchandankhede/agenticai/tree/main/src/SemanticKernel)**

**[Read More](https://github.com/pravinchandankhede/agenticai/tree/main/src/SemanticKernel/ReadMe.md)**

---

## Banking Multi-Agent System (MAS)

A comprehensive demo of multi-agent system implementation for a banking scenario. This implements a loan application scenario demonstrating how various agents collaborate for credit checks, policy validation, and account verification.

**Key Features:**
- Distributed multi-agent system architecture
- Azure Service Bus Topic-based communication
- Specialized agents: Credit Agent, Policy Agent, Accounting Agent, Invoice Agent, Payment Agent, Approval Agent
- RAG (Retrieval-Augmented Generation) engine integration
- Banking service integration

**Architecture Components:**
- Agent Registry
- AI Core Services
- Banking Plugins
- Common Agent Framework
- Core Banking Services

**[View Code](https://github.com/pravinchandankhede/agenticai/tree/main/src/Banking-Multi-Agent-System-Demo)**

---

## Customer Service Agent

Demonstrates a banking customer service agent implementation using Semantic Kernel with document-based knowledge retrieval and vector search capabilities.

**Key Features:**
- Banking domain-specific agent
- Document record processing
- Vector database integration
- Custom plugins for banking operations
- Knowledge retrieval services

**[View Code](https://github.com/pravinchandankhede/agenticai/tree/main/src/CustomerService)**

---

## Model Context Protocol (MCP)

Demonstrates the implementation of [Model Context Protocol](https://modelcontextprotocol.io/introduction) with C# using the [ModelContextProtocol NuGet package](https://packages.nuget.org/packages/ModelContextProtocol/0.1.0-preview.10). MCP enables connecting with variety of data sources and exposing them as standardized tools for AI agents.

### Basic Protocol Working Sample

A foundational demo showing how to create a Banking service and expose its operations as tools through an MCP Server.

**Key Features:**
- Banking service with REST API operations
- MCP Server hosted on ASP.NET Core
- .NET Console Client using ModelContextProtocol library
- Tool discovery and invocation
- Server-client communication

**[View Code](https://github.com/pravinchandankhede/agenticai/tree/main/src/model-context-protocol-demo)**

**[Read More](https://github.com/pravinchandankhede/agenticai/tree/main/src/model-context-protocol-demo/ReadMe.md)**

### Azure OpenAI Chat Completion Integration

Shows how to integrate MCP tools with Azure OpenAI's ChatCompletion APIs for LLM-based tool calling.

**Key Features:**
- MCP tool integration with Azure OpenAI
- Tool calling feature of LLM
- Chat completion with function calling
- Automatic tool invocation based on user queries

**[View Code](https://github.com/pravinchandankhede/agenticai/tree/main/src/model-context-protocol-demo/Clients/MCPAzureOpenAIClient)**

### Semantic Kernel Plugin Integration

Demonstrates how to convert MCP tools into Semantic Kernel plugins for seamless integration with the SK framework.

**Key Features:**
- MCP tool discovery and registration
- Dynamic Semantic Kernel plugin creation
- Azure OpenAI LLM integration via Semantic Kernel
- Unified plugin architecture for MCP tools

**[View Code](https://github.com/pravinchandankhede/agenticai/tree/main/src/model-context-protocol-demo/Clients/MCPSemanticKernelClient)**

---

## Getting Started

### Prerequisites
- .NET 8.0 SDK or later
- Azure OpenAI Service access
- Azure subscription (for some samples)
- Visual Studio 2022 or VS Code

### Configuration
Most samples require configuration settings in `AppSetting.cs` or `appsettings.json`:
- Azure OpenAI endpoint
- API key or Azure credentials
- Deployment name
- Other service-specific settings

### Running the Samples
Each sample project includes a solution file (`.sln`) and can be run independently:

```bash
# Navigate to the project directory
cd src/<ProjectName>

# Build the solution
dotnet build

# Run the project
dotnet run --project <ProjectName>
```

---

## Contributing

Contributions are welcome! Please feel free to submit issues and pull requests.

---

## License

See the [LICENSE](LICENSE) file for details.
