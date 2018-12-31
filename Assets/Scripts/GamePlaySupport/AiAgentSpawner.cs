using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This component is responsible for respawning AI agents when they've died
/// The AI agents will respawn 5 seconsd after they've died
/// </summary>
public class AiAgentSpawner : MonoBehaviour
{
    // Track which AI we've spawned
    public enum AiAgentNumber
    {
        TeamMemberOne,
        TeamMemberTwo,
        TeamMemberThree
    }

    public AiAgentNumber ThisAiAgentNumber;

    // The prefab we're spawning from
    private GameObject AiAgentPrefabToSpawn;
    private int RespawnDelay = 5;

    // The new GameObject
    private GameObject _newAiAgent;
    private string _aiAgentName;

    // Used to control the coroutine which actually does the spawning
    private bool _isSpawnScheduled = false;

    private TeamData teamData;

    // Use this for initialization
    public void Start()
    {
        teamData = transform.parent.GetComponent<TeamData>();
        AiAgentPrefabToSpawn = teamData.AiAgentPrefab;
        RespawnDelay = teamData.RespawnDelay;

        SetAiAgentName();
        SpawnObject();
    }

    // Update is called once per frame
    void Update()
    {
        // Start to spawn a new AI if it's null
        if (_newAiAgent == null && !_isSpawnScheduled)
        {
            StartCoroutine(SpawnDelay());
            _isSpawnScheduled = true;
        }
    }

    /// <summary>
    /// The AI agents name is composed of its team name and number
    /// </summary>
    public void SetAiAgentName()
    {
        _aiAgentName = AiAgentPrefabToSpawn.name;

        switch (ThisAiAgentNumber)
        {
            case AiAgentNumber.TeamMemberOne:
                _aiAgentName += " 1";
                break;
            case AiAgentNumber.TeamMemberTwo:
                _aiAgentName += " 2";
                break;
            case AiAgentNumber.TeamMemberThree:
                _aiAgentName += " 3";
                break;
        }
    }

    /// <summary>
    /// Delay the spawn for a few seconds
    /// </summary>
    /// <returns></returns>
    protected IEnumerator SpawnDelay()
    {
        yield return new WaitForSeconds(RespawnDelay);
        SpawnObject();
        _isSpawnScheduled = false;
        yield return null;
    }

    /// <summary>
    /// Spawn the new AI agent
    /// </summary>
    protected void SpawnObject()
    {
        _newAiAgent = Instantiate(AiAgentPrefabToSpawn, gameObject.transform.position, gameObject.transform.localRotation);
        _newAiAgent.name = _aiAgentName;
        _newAiAgent.GetComponent<AgentData>().FriendlyBase = gameObject.transform.parent.gameObject;
    }
}
