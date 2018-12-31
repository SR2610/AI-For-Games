using System.Collections;
using UnityEngine;

/// <summary>
/// This class represents the power up for use by the AI agent, the powerup has an attack multiplier and
/// a duration in seconds. The powerup will remain in effect after use for the full duration and then the effect will
/// stop
/// </summary>
public class PowerUp : Collectable, IUsable
{
    public int PowerUpAmount = 2;
    public int PowerUpDuration = 10;

    /// <summary>
    /// Apply the effect of the powerup while it is active
    /// </summary>
    /// <param name="agentData"></param>
    public void Use(AgentData agentData)
    {
        agentData.PowerUp(PowerUpAmount);
        StartCoroutine(PowerUpTimer(agentData));
    }

    /// <summary>
    /// A coroutine to track the duration of the powerup and stop it when its dureation is up
    /// </summary>
    /// <returns>yields a short wait (one second) if the powerup is still active, null otherwise</returns>
    IEnumerator PowerUpTimer(AgentData agentData)
    {
        yield return new WaitForSeconds(PowerUpDuration);

        // the powerup effect has finished so reset
        agentData.PowerUp(0);

        // Removed from game after use
        Destroy(gameObject);
        yield return null;
    }
}
