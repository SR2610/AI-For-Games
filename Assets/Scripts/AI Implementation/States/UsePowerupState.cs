﻿using StateMachines;

public class UsePowerupState : State<AI> //Simple state to use the power up and then go onto the attacking state
{

    #region State Instance
    private static UsePowerupState instance; //Static instance of the state

    private UsePowerupState() //Constructor for the state
    {
        if (instance != null) //If we already have an instance of this state, we don't need another one
            return;
        instance = this;
    }

    public static UsePowerupState Instance //Public acsessor of the state, which will return the instance
    {
        get
        {
            if (instance == null)
                new UsePowerupState();  //Constructs the state if we don't yet have an instance
            return instance;
        }
    }
    #endregion


    public override void EnterState(AI owner)
    {
        owner.GetAgentActions().UseItem(owner.GetAgentInventory().GetItem(Names.PowerUp)); //Uses the power up item that is in the inventory
        owner.StateMachine.ChangeState(AttackEnemyState.Instance); //Goes to the attacking state which is the only connection for this state
    }

    public override void ExitState(AI owner) //Not Used
    {
    }

    public override void UpdateState(AI owner) //Not used as there is no continous logic in this state
    {
    }
}