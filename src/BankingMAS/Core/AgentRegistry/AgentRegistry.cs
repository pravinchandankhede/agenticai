namespace BankingMAS.Core.AgentRegistry;

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
            };
    }

    //This has to be a complex matching algorithm
    public static Agent GetAgent(String name)
    {
        return Agents.FirstOrDefault(m=>m.Name==name)!;
    }
}
