using StateMachines;
using UnityEngine;

public class DefendBaseState : State<AI> //State for gaurding flags that are in the base
{
    #region State Instance
    private static DefendBaseState instance; //Static instance of the state

    private DefendBaseState() //Constructor for the state
    {
        if (instance != null) //If we already have an instance of this state, we don't need another one
            return;
        instance = this;
    }

    public static DefendBaseState Instance //Public acsessor of the state, which will return the instance
    {
        get
        {
            if (instance == null)
                new DefendBaseState();  //Constructs the state if we don't yet have an instance
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
        if (owner.GetAgentInventory().GetItem(Names.HealthKit)&&(owner.GetAgentData().CurrentHitPoints / owner.GetAgentData().MaxHitPoints) * 100 < AIConstants.HealThreshold) //If their health is low, they should try to save themselves
            owner.StateMachine.ChangeState(HealState.Instance);
       else if (owner.GetAgentSenses().GetEnemiesInView().Count > 1 && (Random.value < AIConstants.ChaseEnemyChance)) //If they see an enemy and the chase chance triggers
            owner.StateMachine.ChangeState(ChaseEnemyState.Instance); //Chase the enemy
        else if(!owner.GetAgentData().FriendlyBase.GetComponent<SetScore>().IsFriendlyFlagInBase())
            owner.StateMachine.ChangeState(GotoEnemyBaseState.Instance); //Try to reclaim our flag if we don't have it
        else if (Vector3.Distance(owner.transform.position, owner.GetAgentData().FriendlyBase.transform.position) <= AIConstants.BaseDistanceThreshold) //If nothing is happening, wander around and look for something to do
            owner.GetAgentActions().MoveToRandomLocation();
    }
}