using StateMachines;
using UnityEngine;

public class GotoFriendlyFlagState : State<AI>
{
    #region State Instance
    private static GotoFriendlyFlagState instance; //Static instance of the state

    private GotoFriendlyFlagState() //Constructor for the state
    {
        if (instance != null) //If we already have an instance of this state, we don't need another one
            return;
        instance = this;
    }

    public static GotoFriendlyFlagState Instance //Public acsessor of the state, which will return the instance
    {
        get
        {
            if (instance == null)
                new GotoFriendlyFlagState();  //Constructs the state if we don't yet have an instance
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

        GameObject flag = owner.GetAgentSenses().GetObjectInViewByName(owner.GetAgentData().FriendlyFlagName);
        if (flag != null) //If they can see the flag
        {
            owner.GetAgentActions().MoveTo(flag); //Moves the agent towards the friendly flag
            owner.GetAgentActions().CollectItem(flag); //Attempts to collect the flag if it is in range
            if (owner.GetAgentInventory().HasItem(owner.GetAgentData().FriendlyFlagName)) //If they have the flag, it needs to go back to base
                owner.StateMachine.ChangeState(GoHomeState.Instance);

        }

        else if (owner.GetAgentInventory().GetItem(Names.HealthKit) && owner.GetAgentData().CurrentHitPoints / owner.GetAgentData().MaxHitPoints * 100 < AIConstants.HealThreshold) //If their health is low, they should try to save themselves
        {
            owner.StateMachine.ChangeState(HealState.Instance); //Try to heal
        }
        else //If the flag can't be seen anymore, revert back into the Go To Enemy Base State to handle the next action
            owner.StateMachine.ChangeState(GotoEnemyBaseState.Instance);

    }
}

