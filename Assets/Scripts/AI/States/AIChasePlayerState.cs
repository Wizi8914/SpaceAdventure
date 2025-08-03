using UnityEngine;
using UnityEngine.AI;
public class AIChasePlayerState : AIState
{
    float timer;

    public AIStateID GetStateID()
    {
        return AIStateID.ChasePlayer;
    }

    public void EnterState(AIAgent agent)
    {
    }

    public void UpdateState(AIAgent agent)
    {
        if (!agent.enabled) return;

        timer -= Time.deltaTime;
        if (!agent.navMeshAgent.hasPath)
        {
            agent.navMeshAgent.destination = agent.playerTransform.position;
        }

        if (timer < 0f)
        {
            Vector3 direction = (agent.playerTransform.position - agent.navMeshAgent.destination);
            direction.y = 0f; // Ignore vertical distance

            if (direction.sqrMagnitude > agent.config.maxDistance * agent.config.maxDistance)
            {
                if (agent.navMeshAgent.pathStatus != NavMeshPathStatus.PathPartial)
                {
                    agent.navMeshAgent.destination = agent.playerTransform.position;
                }
            }
            timer = agent.config.maxTime;
        }

        
    }

    public void ExitState(AIAgent agent)
    {
        // Code to execute when exiting the chase state
    }
}
