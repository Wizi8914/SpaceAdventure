using UnityEngine;

public enum AIStateID
{
    Idle,
    Patrol,
    ChasePlayer,
    Attack,
    Death,
}

public interface AIState
{
    AIStateID GetStateID();
    void EnterState(AIAgent agent);
    void UpdateState(AIAgent agent);
    void ExitState(AIAgent agent);
}
