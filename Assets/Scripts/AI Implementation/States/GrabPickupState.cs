using StateMachines;
using UnityEngine;

public class GrabPickupState : State<AI>
{

    #region State Instance
    private static GrabPickupState instance; //Static instance of the state

    private GrabPickupState() //Constructor for the state
    {
        if (instance != null) //If we already have an instance of this state, we don't need another one
            return;
        instance = this;
    }

    public static GrabPickupState Instance //Public acsessor of the state, which will return the instance
    {
        get
        {
            if (instance == null)
                new GrabPickupState();  //Constructs the state if we don't yet have an instance
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
        GameObject pickup = owner.GetAgentSenses().GetObjectInViewByName(Names.PowerUp);
        if (pickup == null)
            pickup = owner.GetAgentSenses().GetObjectInViewByName(Names.HealthKit);
        if (pickup != null) //If they can see the pickup
        {
            owner.GetAgentActions().MoveTo(pickup); //Moves the agent towards the pickup
            owner.GetAgentActions().CollectItem(pickup); //Attempts to collect the pickup if it is in range
            if (owner.GetAgentInventory().HasItem(pickup.name))
            {
                //If we don't have the enemy flag
                owner.stateMachine.ChangeState(GotoEnemyBaseState.Instance);

                //If we do
            }
        }
        else
        {
            //no pickup, fall back into a state
            owner.stateMachine.ChangeState(GotoEnemyBaseState.Instance);
        }
    }
}