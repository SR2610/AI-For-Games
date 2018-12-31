using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Used to provide the AI agent spawner with details about the team the AI belongs
/// to based on the spawning base. This component is attached to the base
/// </summary>
public class TeamData : MonoBehaviour
{
    public GameObject AiAgentPrefab;
    public int RespawnDelay = 5;

    private const string BlueTeamName = " Blue";
    private const string RedTeamName = " Red";

    private string _thisTeamName;
    public string ThisTeamName
    {
        get { return _thisTeamName; }
    }

    // Use this for initialization
    void Awake ()
    {
        _thisTeamName = AiAgentPrefab.name;
        AgentData _agentData = AiAgentPrefab.GetComponent<AgentData>();

        _agentData.FriendlyBase = gameObject;

        if (_agentData.FriendlyTeam == AgentData.Teams.BlueTeam)
        {
            _thisTeamName += BlueTeamName;
        }
        else
        {
            _thisTeamName += RedTeamName;
        }
    }
}
