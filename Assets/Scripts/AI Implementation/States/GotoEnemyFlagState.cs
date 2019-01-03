using StateMachines;
using UnityEngine;

public class GotoEnemyFlagState : State<AI>//State for traveling towards the enemy base
{
    #region State Instance
    private static GotoEnemyFlagState instance; //Static instance of the state

    private GotoEnemyFlagState() //Constructor for the state
    {
        if (instance != null) //If we already have an instance of this state, we don't need another one
            return;
        instance = this;
    }

    public static GotoEnemyFlagState Instance //Public acsessor of the state, which will return the instance
    {
        get
        {
            if (instance == null)
                new GotoEnemyFlagState();  //Constructs the state if we don't yet have an instance
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
            owner.stateMachine.ChangeState(HealState.Instance);

        if (!owner.GetAgentData().EnemyBase.GetComponent<SetScore>().IsFriendlyFlagInBase()) //If the enemy flag is not in the enemy base anymore
        {
            owner.stateMachine.ChangeState(GoHomeState.Instance);
        }
        else
        {

            GameObject flag = owner.GetAgentSenses().GetObjectInViewByName(owner.GetAgentData().EnemyFlagName);
            if (flag != null) //If they can see the flag
            {
                owner.GetAgentActions().MoveTo(flag); //Moves the agent towards the enemy flag
                owner.GetAgentActions().CollectItem(flag); //Attempts to collect the flag if it is in range
                if (owner.GetAgentInventory().HasItem(owner.GetAgentData().EnemyFlagName))
                    owner.stateMachine.ChangeState(GoHomeState.Instance);

            }
            else
                owner.stateMachine.ChangeState(GotoEnemyBaseState.Instance);


        }
    }
}
