using UnityEngine;
using StateMachines;


public class GotoEnemyBaseState : State<AI>
{
    #region State Instance
    private static GotoEnemyBaseState instance; //Static instance of the state

    private GotoEnemyBaseState() //Constructor for the state
    {
        if (instance != null) //If we already have an instance of this state, we don't need another one
            return;
        instance = this;
    }

    public static GotoEnemyBaseState Instance //Public acsessor of the state, which will return the instance
    {
        get
        {
            if (instance == null)
                new GotoEnemyBaseState();  //Constructs the state if we don't yet have an instance
            return instance;
        }
    }
    #endregion


    public override void EnterState(AI owner)
    {

    }

    public override void ExitState(AI owner)
    {
    }

    public override void UpdateState(AI owner)
    {
        if (owner.GetAgentInventory().GetItem(Names.HealthKit)&&owner.GetAgentData().CurrentHitPoints / owner.GetAgentData().MaxHitPoints * 100 < AIConstants.HealThreshold) //If their health is low, they should try to save themselves
            owner.StateMachine.ChangeState(HealState.Instance);
        else if (owner.GetAgentData().HasEnemyFlag || owner.GetAgentData().HasFriendlyFlag)
            owner.StateMachine.ChangeState(GoHomeState.Instance);
        else if (owner.GetAgentSenses().GetEnemiesInView().Count > 0 && (Random.value < AIConstants.ChaseEnemyChance)) //If they see an enemy and the chase chance triggers
            owner.StateMachine.ChangeState(ChaseEnemyState.Instance); //Chase the enemy
        else if (owner.GetAgentSenses().GetObjectInViewByName(owner.GetAgentData().EnemyFlagName) && !owner.GetAgentData().FriendlyBase.GetComponent<SetScore>().IsEnemyFlagInBase()) //If they are moving toward the enemy base and can see the enemy flag, they should try to get it
        {
            owner.StateMachine.ChangeState(GotoEnemyFlagState.Instance); //Chase the enemy flag
        }
        else if (owner.GetAgentSenses().GetObjectInViewByName(owner.GetAgentData().FriendlyFlagName) && !owner.GetAgentData().FriendlyBase.GetComponent<SetScore>().IsFriendlyFlagInBase())
        {
            owner.StateMachine.ChangeState(GotoFriendlyFlagState.Instance);
        }
        else if (!owner.GetAgentData().EnemyBase.GetComponent<SetScore>().IsFriendlyFlagInBase() && owner.GetAgentData().FriendlyBase.GetComponent<SetScore>().IsFriendlyFlagInBase()) //If the enemy flag is not in the enemy base anymore and our flag is at home
        {
            owner.StateMachine.ChangeState(GoHomeState.Instance);
        }
        else if (owner.GetAgentSenses().GetObjectInViewByName(Names.PowerUp) || owner.GetAgentSenses().GetObjectInViewByName(Names.HealthKit)) //If they see a pickup that tehy can grab, they should go for it
            owner.StateMachine.ChangeState(GrabPickupState.Instance);
        else
        {
            owner.GetAgentActions().MoveTo(owner.GetAgentData().EnemyBase); //Moves the agent towards the enemy base
            if (Vector3.Distance(owner.transform.position, owner.GetAgentData().EnemyBase.transform.position) <= AIConstants.BaseDistanceThreshold)
                owner.GetAgentActions().MoveToRandomLocation(); //Moves around, so that they can see something to enter the next state
        }
    }
}