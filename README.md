# Agentic AI Samples

[![Build Status](https://github.com/pravinchandankhede/agenticai/actions/workflows/dotnet.yml/badge.svg)](https://github.com/pravinchandankhede/agenticai/actions/workflows/dotnet.yml)

This repository contains demo samples showcasing various agentic AI implementations using C#, TypeScript, Angular, and other technologies. The samples demonstrate building AI agents, orchestration patterns, multi-agent systems, and integration with different frameworks.

## Table of Contents


1. [Agent Framework Sample](#agent-framework-sample)
   - [Simple Agent](#simple-agent)
   - [Image Agent](#image-agent)
   - [Conversation Agent](#conversation-agent)
   - [Tooling Agent](#tooling-agent)
   - [Tooling with Human-in-the-Loop Agent](#tooling-with-human-in-the-loop-agent)
   - [Agent as Tool Sample](#agent-as-tool-sample)
   - [Observability Enabled Agent](#observability-enabled-agent)
   - [Structured Response Agent](#structured-response-agent)
2. [Agent Orchestration](#agent-orchestration)
   - [Sequential Orchestration](#sequential-orchestration)
   - [Concurrent Orchestration](#concurrent-orchestration)
   - [Group Chat Orchestration](#group-chat-orchestration)
   - [Handoff Orchestration](#handoff-orchestration)
3. [Semantic Kernel](#semantic-kernel)
4. [Banking Multi-Agent System (MAS)](#banking-multi-agent-system-mas)
5. [Customer Service Agent](#customer-service-agent)
6. [GitHub Copilot SDK](#github-copilot-sdk)
7. [Model Context Protocol (MCP)](#model-context-protocol-mcp)
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

### Conversation Agent

Demonstrates multi-turn conversation capabilities with thread management for maintaining conversation context across multiple interactions.

**Key Features:**
- Multi-turn dialogue support with conversation threads
- Thread management using `GetNewThread()` for maintaining conversation context
- Contextual responses based on previous conversation history
- Utilizes shared `AgentHelper` library for agent creation
- Support for complex, sequential interactions (e.g., recipe creation followed by modifications)

**[View Code](https://github.com/pravinchandankhede/agenticai/tree/main/src/AgentFrameworkSample/ConversationAgent)**

### Tooling Agent

Demonstrates function calling and tool integration capabilities, showing how agents can invoke custom functions to extend their capabilities beyond language generation.

**Key Features:**
- Function calling using `AIFunctionFactory` to register custom tools
- Tool integration with agent via function descriptors
- Custom tool implementation (WeatherTool example)
- Component model attribute-based function descriptions for LLM understanding
- Automatic tool invocation based on user queries
- Utilizes shared `AgentHelper` library with tool support

**[View Code](https://github.com/pravinchandankhede/agenticai/tree/main/src/AgentFrameworkSample/ToolingAgent)**

### Tooling with Human-in-the-Loop Agent

Demonstrates function calling with human approval before executing tools, implementing a human-in-the-loop pattern for sensitive or important operations.

**Key Features:**
- Human approval workflow for function execution
- `ApprovalRequiredAIFunction` wrapper for tool safety
- Function approval request handling
- Support for both `FunctionApprovalRequestContent` and `UserInputRequestContent`
- Interactive approval prompts with user confirmation
- Safe fallback handling when no approval is requested
- Reflection-based approval response creation for flexibility

**[View Code](https://github.com/pravinchandankhede/agenticai/tree/main/src/AgentFrameworkSample/ToolingWithHumanInLoopAgent)**

### Agent as Tool Sample

Demonstrates how to use an agent as a tool for another agent, creating agent composition and delegation patterns. This sample shows how one agent can invoke another agent as a function/tool to extend its capabilities.

**Key Features:**
- Agent composition using `AsAIFunction()` extension method
- Agent delegation patterns where one agent calls another
- Nested agent invocation and result processing
- Tool integration with agents as callable functions
- Example: Main agent with a specialized agent as a tool that responds in Hindi
- Utilizes shared `AgentHelper` library for consistent agent creation
- Demonstrates language-specific agent delegation

**[View Code](https://github.com/pravinchandankhede/agenticai/tree/main/src/AgentFrameworkSample/AgentAsToolSample)**

### Observability Enabled Agent

Demonstrates how to enable observability and telemetry in agents using OpenTelemetry for monitoring, tracing, and debugging agent behavior.

**Key Features:**
- OpenTelemetry integration for agent monitoring
- Console-based trace exporter for real-time observability
- TracerProvider configuration with custom source names
- Agent builder pattern with `UseOpenTelemetry()` middleware
- Detailed logs and metrics for agent actions
- Tracing of agent execution flow and performance
- Support for distributed tracing in multi-agent scenarios
- Production-ready observability patterns

**Technology Stack:**
- OpenTelemetry (v1.15.0)
- OpenTelemetry.Exporter.Console (v1.15.0)
- Microsoft.Agents.AI framework

**[View Code](https://github.com/pravinchandankhede/agenticai/tree/main/src/AgentFrameworkSample/ObservabilityEnabledAgent)**

### Structured Response Agent

Demonstrates generating structured JSON responses from agents using JSON schema validation and deserialization.

**Key Features:**
- JSON schema generation using `AIJsonUtilities.CreateJsonSchema()`
- Structured output with `ChatResponseFormat.ForJsonSchema()`
- Schema-based response validation
- Type-safe deserialization of agent responses
- Support for complex types (Employee model with Name, Age, Position, Level)
- Robust JSON extraction from multiple content formats (JsonElement, JsonDocument, string)
- Error handling for malformed JSON responses

**[View Code](https://github.com/pravinchandankhede/agenticai/tree/main/src/AgentFrameworkSample/StructuredResponseAgent)**

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

## GitHub Copilot SDK

Demonstrates the usage of the GitHub Copilot SDK for building AI agents using the official GitHub Copilot SDK package. This sample shows how to create a simple agent that interacts with GitHub Copilot's language models.

**Key Features:**
- GitHub Copilot SDK integration using `GitHub.Copilot.SDK` NuGet package
- Simple agent creation with `CopilotClient`
- Session-based conversation management
- Event-driven message handling
- Support for GPT-4.1 and other models
- Asynchronous message processing

**Technology Stack:**
- .NET 10.0
- GitHub.Copilot.SDK (v0.1.18)

**[View Code](https://github.com/pravinchandankhede/agenticai/tree/main/src/GitHubCopilot)**

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
