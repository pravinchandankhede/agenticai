namespace BankingMAS.AICore.AgentRegistry;

public static class AgentRegistry
{
    static Agent[] Agents { get; set; }

    static AgentRegistry()
    {
        Agents = new Agent[]
            {
                new Agent{ Name="Accounting", QueueName="accounting"},
                new Agent{ Name="Approval", QueueName="approval"},
                new Agent{ Name="Credit", QueueName="credit"},
                new Agent{ Name="Payment", QueueName="payment"},
                new Agent{ Name="Policy", QueueName="policy"},
                new Agent{ Name="BankingMAS", QueueName="bankingmas"},
                new Agent{ Name="Invoice", QueueName="invoice"},
            };
    }

    //This has to be a complex matching algorithm based on name, features, etc.
    //you can use Intent identification using LLM/Cognitive call to check which agent should actually handle the req
    public static Agent GetAgent(String name)
    {
        return Agents.FirstOrDefault(m => String.Compare(m.Name, name, true) == 0)!;
    }
}
