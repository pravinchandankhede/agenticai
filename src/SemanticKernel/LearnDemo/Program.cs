using LearnDemo.Plugins;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Plugins.Core;

class Program
{
    static string yourDeploymentName = "gpt-4-32k";
    static string yourEndpoint = "https://pctesopenaicentral.openai.azure.com/";
    static string yourKey = "0bf5d78b38a5487a9a999c9bea8e4f72";
    
    public static async Task Main()
    {
        //await SimpleCall();
        //await PluginCall();
        //await PromptTemplateCall();
        //await CustomPromptCall();
        //await PersonaCall();
        //await SavePrompt();
        await SkillCall();
    }

    static async Task SimpleCall()
    {
        var builder = Kernel.CreateBuilder();
        builder.AddAzureOpenAIChatCompletion(
            yourDeploymentName,
            yourEndpoint,
            yourKey,
            "gpt-4-32k");

        var kernel = builder.Build();

        var result = await kernel.InvokePromptAsync("Give me a list of breakfast foods with eggs and cheese");
        Console.WriteLine(result);
    }

    static async Task PluginCall()
    {
        var builder = Kernel.CreateBuilder();
        builder.AddAzureOpenAIChatCompletion(
            yourDeploymentName,
            yourEndpoint,
            yourKey,
            "gpt-4-32k");

#pragma warning disable SKEXP0050 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
        builder.Plugins.AddFromType<ConversationSummaryPlugin>();
#pragma warning restore SKEXP0050 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
        var kernel = builder.Build();

        string input = @"I'm a vegan in search of new recipes. I love spicy food! Can you give me a list of breakfast recipes that are vegan friendly?";

        var result = await kernel.InvokeAsync("ConversationSummaryPlugin", "GetConversationActionItems", new() { { "input", input } });

        Console.WriteLine(result);
    }

    static async Task PromptTemplateCall()
    {
        var builder = Kernel.CreateBuilder();
        builder.AddAzureOpenAIChatCompletion(
            yourDeploymentName,
            yourEndpoint,
            yourKey,
            "gpt-4-32k");

#pragma warning disable SKEXP0050 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
        builder.Plugins.AddFromType<ConversationSummaryPlugin>();
#pragma warning restore SKEXP0050 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
        var kernel = builder.Build();

        string history = @"In the heart of my bustling kitchen, I have embraced 
    the challenge of satisfying my family's diverse taste buds and 
    navigating their unique tastes. With a mix of picky eaters and 
    allergies, my culinary journey revolves around exploring a plethora 
    of vegetarian recipes.

    One of my kids is a picky eater with an aversion to anything green, 
    while another has a peanut allergy that adds an extra layer of complexity 
    to meal planning. Armed with creativity and a passion for wholesome 
    cooking, I've embarked on a flavorful adventure, discovering plant-based 
    dishes that not only please the picky palates but are also heathy and 
    delicious.";

        string prompt = @"This is some information about the user's background: 
    {{$history}}

    Given this user's background, provide a list of relevant recipes.";

        var result = await kernel.InvokePromptAsync(prompt,
            new KernelArguments() { { "history", history } });

        Console.WriteLine(result);
    }

    static async Task CustomPromptCall()
    {
        var builder = Kernel.CreateBuilder();
        builder.AddAzureOpenAIChatCompletion(
            yourDeploymentName,
            yourEndpoint,
            yourKey,
            "gpt-4-32k");

        var kernel = builder.Build();

        string language = "hindi";
        string history = @"I'm traveling with my kids and one of them 
    has a peanut allergy.";

        string prompt = @$"Consider the traveler's background:
    ${history}

    Create a list of helpful phrases and words in 
    ${language} a traveler would find useful.

    Group phrases by category. Include common direction 
    words. Display the phrases in the following format: 
    Hello - Ciao [chow]";

        var result = await kernel.InvokePromptAsync(prompt,
            new KernelArguments() { { "history", history },
                { "language", language } });
        Console.WriteLine(result);
    }

    static async Task PersonaCall()
    {
        var builder = Kernel.CreateBuilder();
        builder.AddAzureOpenAIChatCompletion(
            yourDeploymentName,
            yourEndpoint,
            yourKey,
            "gpt-4-32k");

        var kernel = builder.Build();

        string language = "marathi";
        string history = @"I'm traveling with my kids and one of them has a peanut allergy.";
        string input = @"I have a vacation from June 1 to July 22. I want to go to Greece. 
    I live in Chicago.";

        // Assign a persona to the prompt
        string prompt = @$"
<message role=""system"">Instructions: Identify the from and to destinations 
and dates from the user's request</message>

<message role=""user"">Can you give me a list of flights from Seattle to Tokyo? 
I want to travel from March 11 to March 18.</message>

<message role=""assistant"">Seattle|Tokyo|03/11/2024|03/18/2024</message>

<message role=""user"">${input}</message>";

        var result = await kernel.InvokePromptAsync(prompt,
            new KernelArguments() { { "history", history },
                { "language", language } });
        Console.WriteLine(result);
    }

    static async Task SavePrompt()
    {
        var builder = Kernel.CreateBuilder();
        builder.AddAzureOpenAIChatCompletion(
            yourDeploymentName,
            yourEndpoint,
            yourKey,
            "gpt-4-32k");

        var kernel = builder.Build();

#pragma warning disable SKEXP0050 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
        kernel.ImportPluginFromType<ConversationSummaryPlugin>();
#pragma warning restore SKEXP0050 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
        var prompts = kernel.ImportPluginFromPromptDirectory("Prompts/TravelPlugins");

        ChatHistory history = [];
        string input = @"I'm planning an anniversary trip with my spouse. We like hiking, 
    mountains, and beaches. Our travel budget is $15000";

        var result = await kernel.InvokeAsync<string>(prompts["SuggestDestinations"],
            new() { { "input", input } });

        Console.WriteLine(result);
        history.AddUserMessage(input);
        history.AddAssistantMessage(result);

        Console.WriteLine("Where would you like to go?");
        input = Console.ReadLine();

        result = await kernel.InvokeAsync<string>(prompts["SuggestActivities"],
            new() {
        { "history", history },
        { "destination", input },
            }
        );
        Console.WriteLine(result);
    }

    static async Task SkillCall()
    {
        var builder = Kernel.CreateBuilder();
        builder.AddAzureOpenAIChatCompletion(
            yourDeploymentName,
            yourEndpoint,
            yourKey,
            "gpt-4-32k");

        var kernel = builder.Build();

        kernel.ImportPluginFromType<MusicLibraryPlugin>();

        var result = await kernel.InvokeAsync(
            "MusicLibraryPlugin",
            "AddToRecentlyPlayed",
            new()
            {
                ["artist"] = "Tiara",
                ["song"] = "Danse",
                ["genre"] = "French pop, electropop, pop"
            }
        );

        Console.WriteLine(result);
    }
}