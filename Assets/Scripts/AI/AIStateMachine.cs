using UnityEngine;

public class AIStateMachine
{
    public AIState[] states;
    public AIAgent agent;
    public AIStateID currentState;

    public AIStateMachine(AIAgent agent)
    {
        this.agent = agent;
        int numStates = System.Enum.GetNames(typeof(AIStateID)).Length;
        states = new AIState[numStates];
    }

    public void RegisterState(AIState state)
    {
        int index = (int)state.GetStateID();
        states[index] = state;
    }

    public AIState GetCurrentState(AIStateID stateID)
    {
        int index = (int)stateID;
        return states[index];
    }

    public void Update()
    {
        GetCurrentState(currentState)?.UpdateState(agent);
    }

    public void ChangeState(AIStateID newState)
    {
        if (currentState == newState) return;
        
        GetCurrentState(currentState)?.ExitState(agent);
        currentState = newState;
        GetCurrentState(currentState)?.EnterState(agent);
    }
}
