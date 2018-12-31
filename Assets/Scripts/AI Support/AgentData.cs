using System.Collections.Generic;
using UnityEngine;

// Add new AI moods here as needed
public enum AiMood { Idle, Attacking, Fleeing, Winning, Losing, Dead };

/// <summary>
/// Stores significant adta about the AI agent, this can also be used to evaluate an enemy AI's strength
/// A few utility methods are provided which directly affect the AI's internal state e.g. health
/// </summary>
public class AgentData : MonoBehaviour
{
    // Allow us to select the team in the Inspector
    public enum Teams
    {
        RedTeam,
        BlueTeam,
    }
    public Teams EnemyTeam;
    public Teams FriendlyTeam;

    // A tag identifying members of the enemy team
    private string _enemyTeamTag = "";
    public string EnemyTeamTag
    {
        get
        {
            return _enemyTeamTag;
        }
    }

    // The name identifying the friendly flag
    private string _friendlyFlagName = "";
    public string FriendlyFlagName
    {
        get
        {
            return _friendlyFlagName;
        }
    }

    // The name identifying the enemy flag
    private string _enemyFlagName = "";
    public string EnemyFlagName
    {
        get
        {
            return _enemyFlagName;
        }
    }

    /// <summary>
    /// The GameObject representing the friendly base
    /// </summary>
    private GameObject _friendlyBase;
    public GameObject FriendlyBase
    {
        get { return _friendlyBase; }
        set { _friendlyBase = value; }
    }

    /// <summary>
    /// The GameObject representing the friendly base
    /// </summary>
    private GameObject _enemyBase;
    public GameObject EnemyBase
    {
        get { return _enemyBase; }
        set { _enemyBase = value; }
    }

    /// <summary>
    /// A tag representing members of the friendly team
    /// </summary>
    private string _friendlyTeamTag = "";
    public string FriendlyTeamTag
    {
        get
        {
            return _friendlyTeamTag;
        }
    }

    // Agent stats
    public int MaxHitPoints = 100;
    public float AttackRange = 2.0f;
    public int NormalAttackDamage = 10;
    public float HitProbability = 0.5f;
    public float PickUpRange = 1.0f;
    public int Speed = 200;
    public int ViewRange = 10;

    // Check for collisions with everything when checking for a random location for the wander function
    public const int AgentLayerMask = -1;

    // Our current health, this is public in order to aid debugging
    public int CurrentHitPoints;

    /// <summary>
    /// Get the current scores
    /// </summary>
    private SetScore _friendlyTeamScore;
    public int FriendlyScore
    {
        get { return _friendlyTeamScore.Score; }
    }

    private SetScore _enemyTeamScore;
    public int EnemyScore
    {
        get { return _enemyTeamScore.Score; }
    }


    /// <summary>
    /// Show the current mood of the AI agent
    /// </summary>
    private AiMood _aiMood;
    public AiMood AiMood
    {
        get { return _aiMood; }
        set { _aiMood = value; }
    }

    /// <summary>
    /// Get the powerup multiplier amount
    /// </summary>
    private int _powerUp = 0;
    public int PowerUpAmount
    {
        get { return _powerUp; }
    }

    /// <summary>
    /// Do we have a powerup?
    /// </summary>
    public bool IsPoweredUp
    {
        get { return _powerUp > 0; }
    }

    /// <summary>
    /// Get the flag carrier status from the inventory
    /// If we have the flag in our inventory we are the carrier
    /// </summary>
    public bool HasFriendlyFlag
    {
        get
        {
            if (gameObject.GetComponentInChildren<InventoryController>().HasItem(FriendlyFlagName))
            {
                return true;
            }
            return false;
        }
    }

    public bool HasEnemyFlag
    {
        get
        {
            if (gameObject.GetComponentInChildren<InventoryController>().HasItem(EnemyFlagName))
            {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// We've died, destroy our gameobject
    /// </summary>
    public void Die()
    {
        _aiMood = AiMood.Dead;
        AgentActions actions = gameObject.GetComponent<AgentActions>();

        actions.DropAllItems();
        Destroy(gameObject);
    }

    /// <summary>
    /// We've been hit and taken some damage
    /// If hit points reach zero we've died
    /// </summary>
    /// <param name="damage"></param>
    public void TakeDamage(int damage)
    {
        // Don't go below zero hitopints
        if (CurrentHitPoints + damage > 0)
        {
            CurrentHitPoints -= damage;
        }
        else
        {
            CurrentHitPoints = 0;
            Die();
        }
    }

    /// <summary>
    /// We'ved used a healing potion so heal up
    /// </summary>
    /// <param name="healAmount"></param>
    public void Heal(int healAmount)
    {
        // Don't go above our max hit points
        if (CurrentHitPoints + healAmount > MaxHitPoints)
        {
            CurrentHitPoints = MaxHitPoints;
        }
        else
        {
            CurrentHitPoints += healAmount;
        }
    }

    /// <summary>
    /// Set the powerup multiplier amount
    /// </summary>
    /// <param name="powerAmount">Powerup multiplier</param>
    public void PowerUp(int powerAmount)
    {
        _powerUp = powerAmount;
    }

    // Use this for initialization
    void Start ()
    {        
        CurrentHitPoints = MaxHitPoints;

        // Set all the appropriate parameters for the current team
        switch(EnemyTeam)
        {
            case Teams.BlueTeam:
                _enemyTeamTag = Tags.BlueTeam;
                _enemyFlagName = Names.BlueFlag;
                _friendlyTeamTag = Tags.RedTeam;
                _friendlyFlagName = Names.RedFlag;
                _friendlyBase = GameObject.Find(Names.RedBase);
                _enemyBase = GameObject.Find(Names.BlueBase);

                break;

            case Teams.RedTeam:
                _enemyTeamTag = Tags.RedTeam;
                _enemyFlagName = Names.RedFlag;
                _friendlyTeamTag = Tags.BlueTeam;
                _friendlyFlagName = Names.BlueFlag;
                _friendlyBase = GameObject.Find(Names.BlueBase);
                _enemyBase = GameObject.Find(Names.RedBase);

                break;
        }
        _friendlyTeamScore = _friendlyBase.GetComponent<SetScore>();
        _enemyTeamScore = _enemyBase.GetComponent<SetScore>();
    }

    void Update()
    {
        // TODO: Maybe remove this
        // Show the sadface when hitpoints are low
        if (CurrentHitPoints < MaxHitPoints / 2)
        {
            _aiMood = AiMood.Losing;
        }
        else
        {
            _aiMood = AiMood.Idle;
        }
    }
}