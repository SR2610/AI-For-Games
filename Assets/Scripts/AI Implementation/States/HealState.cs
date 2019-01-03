using StateMachines;
using UnityEngine;

public class HealState : State<AI>
{

    #region State Instance
    private static HealState instance; //Static instance of the state

    private HealState() //Constructor for the state
    {
        if (instance != null) //If we already have an instance of this state, we don't need another one
            return;
        instance = this;
    }

    public static HealState Instance //Public acsessor of the state, which will return the instance
    {
        get
        {
            if (instance == null)
                new HealState();  //Constructs the state if we don't yet have an instance
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
        if (owner.GetAgentInventory().GetItem(Names.HealthKit)) //If they have a health kit in their inventory
        {
            owner.GetAgentActions().UseItem(owner.GetAgentInventory().GetItem(Names.HealthKit)); //Use the health kit
            owner.stateMachine.ChangeState(ChaseEnemyState.Instance); //Go back to attacking
        }
        else
        {
            if (owner.GetAgentSenses().GetEnemiesInView().Count > 0) //If they can currently seen an enemy
            {
                GameObject enemy = owner.GetAgentSenses().GetEnemiesInView()[0];

                if (Random.value < AIConstants.FleeChance)
                    owner.stateMachine.ChangeState(GoHomeState.Instance);  //Try to flee the enemy
                else
                    owner.stateMachine.ChangeState(GotoEnemyBaseState.Instance); //If they can't go back to normal logic
            }
            else
            {
                owner.stateMachine.ChangeState(GotoEnemyBaseState.Instance); //If they can't go back to normal logic
            }
        }
    }
}