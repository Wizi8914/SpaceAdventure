using UnityEngine;
using UnityEngine.AI;
public class AIChasePlayerState : AIState
{
    float timer;
    float chaseTime;
    float noVisionTimer;

    public AIStateID GetStateID()
    {
        return AIStateID.ChasePlayer;
    }

    public void EnterState(AIAgent agent)
    {
        chaseTime = agent.config.maxChaseTime;
        noVisionTimer = agent.config.maxChaseDurationWithoutVision;
    }

    public void UpdateState(AIAgent agent)
    {
        if (!agent.enabled) return;

        timer -= Time.deltaTime;
        chaseTime -= Time.deltaTime;

        UpdateDestination(agent);
        CheckAttackTransition(agent);
        CheckIdleTransition(agent);
        CheckChaseTimeout(agent);
        CheckNoVisionTimeout(agent);
    }

    private void UpdateDestination(AIAgent agent)
    {
        if (!agent.navMeshAgent.hasPath)
        {
            agent.navMeshAgent.destination = agent.playerTransform.position;
        }

        if (timer < 0f)
        {
            Vector3 direction = (agent.playerTransform.position - agent.navMeshAgent.destination);
            direction.y = 0f;

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

    private void CheckAttackTransition(AIAgent agent)
    {
        float distanceToPlayer = Vector3.Distance(agent.transform.position, agent.playerTransform.position);
        if (distanceToPlayer < agent.config.maxAttackDistance)
        {
            agent.stateMachine.ChangeState(AIStateID.Attack);
        }
    }

    private void CheckIdleTransition(AIAgent agent)
    {
        float distanceToPlayer = Vector3.Distance(agent.transform.position, agent.playerTransform.position);
        if (distanceToPlayer > agent.config.maxChaseDistance)
        {
            agent.stateMachine.ChangeState(AIStateID.Idle);
        }
    }

    private void CheckChaseTimeout(AIAgent agent)
    {
        if (chaseTime <= 0f)
        {
            agent.stateMachine.ChangeState(AIStateID.Idle);
        }
    }

    private void CheckNoVisionTimeout(AIAgent agent)
    {
        if (agent.detectionStateManager.PlayerSeen())
        {
            noVisionTimer = agent.config.maxChaseDurationWithoutVision;
        }
        else
        {
            noVisionTimer -= Time.deltaTime;
            if (noVisionTimer <= 0f)
            {
                agent.stateMachine.ChangeState(AIStateID.Idle);
            }
        }
    }

    public void ExitState(AIAgent agent)
    {
        // Code to execute when exiting the chase state
    }
}