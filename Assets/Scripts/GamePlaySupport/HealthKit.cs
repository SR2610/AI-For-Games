/// <summary>
/// The health kit will heal the agent by the amount specified by the '_healingAmount' member variable
/// up to the agents maximum health
/// </summary>
public class HealthKit : Collectable, IUsable
{
    public int HealingAmount = 50;

    public void Use(AgentData agentData)
    {
        agentData.Heal(HealingAmount);
        Destroy(gameObject);
    }
}
