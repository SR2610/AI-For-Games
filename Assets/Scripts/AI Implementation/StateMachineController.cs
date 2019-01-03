namespace StateMachines
{


    public class StateMachineController<AI>
    {
        public State<AI> currentState;
        public AI stateOwner;

        public StateMachineController(AI owner) //Constructor to initilise the state, setting up the owner and ensuring it isn't already in a state
        {
            stateOwner = owner;
            currentState = null;
        }

        public void ChangeState(State<AI> nextState) //Exits the current state and enters the new one
        {
            if (currentState!=null) //Check that we are already in a state before attempting to exit it
                currentState.ExitState(stateOwner);
            currentState = nextState;
            currentState.EnterState(stateOwner);
        }

        public void UpdateState() //Update ticks the current state
        {
            if(currentState!=null) //If the state is not null, we can update it
                currentState.UpdateState(stateOwner);

        }


    }


    public abstract class State<AI> //The abstract of a state that all states need to be based off of
    {

        public abstract void EnterState(AI owner);
        public abstract void ExitState(AI owner);
        public abstract void UpdateState(AI owner);

    }
}
