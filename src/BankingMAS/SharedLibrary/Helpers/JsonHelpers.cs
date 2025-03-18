namespace BankingMAS.SharedLibrary.Helpers;

using System.Text;
using System.Text.Json;

public static class JsonHelpers
{
    public static  async Task<Dictionary<String, String>> GetDocumentRecords(String filePath)
    {
        using FileStream openStream = File.OpenRead(filePath);
        var document = await JsonDocument.ParseAsync(openStream);
        var root = document.RootElement;

        if (root.TryGetProperty("FastInvestmentsBankingManual", out JsonElement manual))
        {
            Dictionary<String, String> level2Nodes = [];

            foreach (JsonProperty property in manual.EnumerateObject())
            {
                List<String> level2Text = [property.Name];
                AppendChildNodes(property.Value, level2Text);
                level2Nodes.Add(property.Name, String.Join(" ", level2Text));
            }

            return level2Nodes;
        }

        return [];
    }

    public static async Task<Dictionary<String, String>> GetDocumentRecords(String content, String rootElementName)
    {
        var document = JsonDocument.Parse(content);
        var root = document.RootElement;

        if (root.TryGetProperty(rootElementName, out JsonElement manual))
        {
            Dictionary<String, String> level2Nodes = [];

            foreach (JsonElement node in manual.EnumerateArray())
            {
                var name = node.GetProperty("title").GetString();
                var level2Text = node.ToString();
                level2Nodes.Add(name, level2Text);
                //AppendChildNodes(node, level2Text);
                //level2Nodes.Add(property.Name, String.Join(" ", level2Text));
            }

            return level2Nodes;
        }

        return [];
    }

    /// <summary>
    /// Utility Method. Recursively append child nodes to the text list.
    /// </summary>
    /// <param name="element">the current json node.</param>
    /// <param name="textList">the list of literal from earlier node.</param>
    private static void AppendChildNodes(JsonElement element, List<String> textList)
    {
        switch (element.ValueKind)
        {
            case JsonValueKind.Object:
                foreach (JsonProperty property in element.EnumerateObject())
                {
                    textList.Add(property.Name);
                    AppendChildNodes(property.Value, textList);
                }
                break;
            case JsonValueKind.Array:
                foreach (JsonElement item in element.EnumerateArray())
                {
                    AppendChildNodes(item, textList);
                }
                break;
            case JsonValueKind.String:
                textList.Add(element.GetString());
                break;
            case JsonValueKind.Number:
                textList.Add(element.GetRawText());
                break;
            case JsonValueKind.True:
            case JsonValueKind.False:
                textList.Add(element.GetBoolean().ToString());
                break;
            case JsonValueKind.Null:
                break;
        }
    }

    public static async Task<Dictionary<String, String>> ExtractSubsectionsAsync(String content, String rootElementName)
    {
        var keyValuePairs = new Dictionary<String, String>();
        var document = JsonDocument.Parse(content);
        var root = document.RootElement;

        if (root.TryGetProperty(rootElementName, out JsonElement manual))
        {
            ProcessJsonElement(manual, keyValuePairs, new StringBuilder());
        }

        return keyValuePairs;
    }

    private static void ProcessJsonElement(JsonElement element, Dictionary<String, String> keyValuePairs, StringBuilder parentKey)
    {
        switch (element.ValueKind)
        {
            case JsonValueKind.Object:
                foreach (JsonProperty property in element.EnumerateObject())
                {
                    var key = new StringBuilder(parentKey.ToString());
                    if (key.Length > 0)
                    {
                        key.Append(".");
                    }
                    key.Append(property.Name);
                    ProcessJsonElement(property.Value, keyValuePairs, key);
                }
                break;
            case JsonValueKind.Array:
                for (int i = 0; i < element.GetArrayLength(); i++)
                {
                    var key = new StringBuilder(parentKey.ToString());
                    key.Append($"[{i}]");
                    ProcessJsonElement(element[i], keyValuePairs, key);
                }
                break;
            default:
                keyValuePairs[parentKey.ToString()] = element.ToString();
                break;
        }
    }
}
