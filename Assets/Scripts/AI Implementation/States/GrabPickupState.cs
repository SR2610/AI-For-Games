using StateMachines;
using UnityEngine;

public class GrabPickupState : State<AI> //Trys to grab a pickup when it was seen
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

    State<AI> previousState;

    public override void EnterState(AI owner)
    {
        previousState = owner.StateMachine.lastState; //Sets the last state that the main state machine was in, so it can be resumed
    }

    public override void ExitState(AI owner)
    {

    }

    public override void UpdateState(AI owner)
    {
        GameObject pickup = owner.GetAgentSenses().GetObjectInViewByName(Names.PowerUp); //Checks if there is a power up in view
        if (pickup == null) //If there is not a power up in view, it checks for a health kit instead
            pickup = owner.GetAgentSenses().GetObjectInViewByName(Names.HealthKit);
        if (pickup != null) //If they can see the pickup
        {
            owner.GetAgentActions().MoveTo(pickup); //Moves the agent towards the pickup
            owner.GetAgentActions().CollectItem(pickup); //Attempts to collect the pickup if it is in range
            if (owner.GetAgentInventory().HasItem(pickup.name))
            {
                //If we now have the pickup   
                owner.StateMachine.ChangeState(previousState);
            }
        }
        else
        {
            //no pickup, fall back into a state
            owner.StateMachine.ChangeState(previousState);
        }
    }
}