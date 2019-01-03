using StateMachines;
using UnityEngine;

public class GoHomeState : State<AI>
{
    #region State Instance
    private static GoHomeState instance; //Static instance of the state

    private GoHomeState() //Constructor for the state
    {
        if (instance != null) //If we already have an instance of this state, we don't need another one
            return;
        instance = this;
    }

    public static GoHomeState Instance //Public acsessor of the state, which will return the instance
    {
        get
        {
            if (instance == null)
                new GoHomeState();  //Constructs the state if we don't yet have an instance
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


        owner.GetAgentActions().MoveTo(owner.GetAgentData().FriendlyBase); //Moves the agent back to their own base
        if (owner.GetAgentData().HasEnemyFlag || owner.GetAgentData().HasFriendlyFlag)
        {
            #region Has Enemy Flag
            if (owner.GetAgentData().HasEnemyFlag && Vector3.Distance(owner.transform.position, owner.GetAgentData().FriendlyBase.transform.position) <= AIConstants.BaseDistanceThreshold) //If they have the enemy flag, try to drop it at the base if they are tehere
            {
                RaycastHit hit;
                if (Physics.Raycast(owner.transform.position, Vector3.down, out hit, 2F))        //Raycast below to check if the agent is above their own base to drop their flag
                {
                    if (hit.transform.gameObject == owner.GetAgentData().FriendlyBase)
                    {
                        owner.GetAgentActions().DropItem(owner.GetAgentInventory().GetItem(owner.GetAgentData().EnemyFlagName)); //Drop the flag if they are above their base in order to gain points
                        owner.stateMachine.ChangeState(DefendBaseState.Instance);
                    }
                }
            }
            #endregion

            #region Has Friendly Flag
            if (owner.GetAgentData().HasFriendlyFlag && Vector3.Distance(owner.transform.position, owner.GetAgentData().FriendlyBase.transform.position) <= AIConstants.BaseDistanceThreshold) //If they have the enemy flag, try to drop it at the base if they are tehere
            {
                RaycastHit hit;
                if (Physics.Raycast(owner.transform.position, Vector3.down, out hit, 2F))        //Raycast below to check if the agent is above their own base to drop their flag
                {
                    if (hit.transform.gameObject == owner.GetAgentData().FriendlyBase)
                    {
                        owner.GetAgentActions().DropItem(owner.GetAgentInventory().GetItem(owner.GetAgentData().FriendlyFlagName)); //Drop the flag if they are above their base in order to gain points
                        owner.stateMachine.ChangeState(DefendBaseState.Instance);
                    }
                }
            }
            #endregion

        }
        else if (owner.GetAgentSenses().GetEnemiesInView().Count > 0 && (Random.value < AIConstants.ChaseEnemyChance)) //If they see an enemy and the chase chance triggers
            owner.stateMachine.ChangeState(ChaseEnemyState.Instance); //Chase the enemy
        else if (owner.GetAgentSenses().GetObjectInViewByName(Names.PowerUp) || owner.GetAgentSenses().GetObjectInViewByName(Names.HealthKit)) //If they see a pickup that tehy can grab, they should go for it
            owner.stateMachine.ChangeState(GrabPickupState.Instance);
        else if (owner.GetAgentData().FriendlyBase.GetComponent<SetScore>().IsEnemyFlagInBase()) //If the enemy flag is in the friendly base
            owner.stateMachine.ChangeState(DefendBaseState.Instance);
        else if (owner.GetAgentData().CurrentHitPoints / owner.GetAgentData().MaxHitPoints * 100 < AIConstants.HealThreshold) //If their health is low, they should try to save themselves
            owner.stateMachine.ChangeState(HealState.Instance);
        else
            owner.stateMachine.ChangeState(GotoEnemyBaseState.Instance);



    }
}