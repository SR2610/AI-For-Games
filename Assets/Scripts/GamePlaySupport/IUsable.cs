/// <summary>
/// This interface represents any item usable by the AI agent. The 'use' method will apply any changes to the
/// agent data through the 'agentData' parameter
/// </summary>
public interface IUsable
{
    // apply whatever effect the item has on the agent
    void Use(AgentData agentData);
}
