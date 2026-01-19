namespace AgentFrameworkSample.StructuredResponseAgent;

using Common;
using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization  ;

internal class Program
{
    class Employee
    {
        public String Name { get; set; }
        public Int32 Age { get; set; }
        public String Position { get; set; }
        public String Level { get; set; }

        public Employee(String name, Int32 age, String position, String level)
        {
            Name = name;
            Age = age;
            Position = position;
            Level = level;
        }
    }

    private static string ExtractJsonText(AIContent content)
    {
        object? raw = content.RawRepresentation;

        return raw switch
        {
            JsonElement je => je.GetRawText(),
            JsonDocument jd => jd.RootElement.GetRawText(),
            string s => s,
            null => string.Empty,
            _ => JsonSerializer.Serialize(raw)
        };
    }

    private static async Task Main()
    {
        JsonElement schema = AIJsonUtilities.CreateJsonSchema(typeof(Employee));

        ChatOptions chatOptions = new()
        {
            ResponseFormat = ChatResponseFormat.ForJsonSchema(
                schema: schema,
                schemaName: "Employee",
                schemaDescription: "Information about a employee including their name, age, and position")
        };

        var agent = AgentHelper.GetAgent(
                    name: "Structured Response Agent",
                    description: "An agent that demonstrates structured response generation.",
                    instructions: "You are an agent that provides information about employees in a structured JSON format.",
                    chatOptions: chatOptions
                );

        AgentThread thread = agent.GetNewThread();
        AgentRunResponse response = await agent.RunAsync("Provide information about an employee named Alice who is 30 years old, works as a Software Engineer, and is at the Mid level.", thread);

        foreach (var message in response.Messages)
        {
            Console.WriteLine(message);

            var jsonContent = message.Contents.OfType<AIContent>().FirstOrDefault();
            if (jsonContent is null)
            {
                continue;
            }

            string jsonText = ExtractJsonText(jsonContent);
            if (string.IsNullOrWhiteSpace(jsonText))
            {
                continue;
            }

            try
            {
                Employee? employee = JsonSerializer.Deserialize<Employee>(jsonText);
                if (employee is not null)
                {
                    Console.WriteLine($"Deserialized Employee: Name={employee.Name}, Age={employee.Age}, Position={employee.Position}, Level={employee.Level}");
                }
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Failed to deserialize Employee JSON. Error: {ex.Message}");
                Console.WriteLine($"JSON payload: {jsonText}");
            }
        }
    }
}