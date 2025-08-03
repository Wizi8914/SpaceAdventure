using UnityEngine;
using UnityEngine.AI;

public class AIIdleState : AIState
{
    public AIStateID GetStateID()
    {
        return AIStateID.Idle;
    }
    public void EnterState(AIAgent agent)
    {
        
    }
    public void UpdateState(AIAgent agent)
    {
        Vector3 playerDirection = agent.playerTransform.position - agent.transform.position;
        if (playerDirection.magnitude > agent.config.maxSightDistance) return;

        Vector3 agentDirection = agent.transform.forward;

        playerDirection.Normalize();

        float dotProduction = Vector3.Dot(agentDirection, playerDirection);
        if (dotProduction > 0f)
        {
            agent.stateMachine.ChangeState(AIStateID.ChasePlayer);
        }
    }

    public void ExitState(AIAgent agent)
    {
    }


}