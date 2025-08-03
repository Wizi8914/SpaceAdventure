using UnityEngine;
using UnityEngine.AI;

public class AIIdleState : AIState
{
    private float seenTimer;

    public AIStateID GetStateID()
    {
        return AIStateID.Idle;
    }
    public void EnterState(AIAgent agent)
    {
        
    }
    public void UpdateState(AIAgent agent)
    {
        // Check if the player is seen
        if (agent.detectionStateManager.PlayerSeen())
        {
            seenTimer += Time.deltaTime;
            if (seenTimer > agent.config.minSeenTime)
            {
                agent.stateMachine.ChangeState(AIStateID.ChasePlayer);
            }
        }
        else
        {
            seenTimer = 0f;
        }

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