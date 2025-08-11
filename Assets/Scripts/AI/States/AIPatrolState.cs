using System.Collections;
using UnityEngine;

public class AIPatrolState : AIState
{
    private int currentPatrolPointIndex;
    private float seenTimer;


    public AIStateID GetStateID()
    {
        return AIStateID.Patrol;
    }

    public void EnterState(AIAgent agent)
    {
        currentPatrolPointIndex = 0;
        agent.navMeshAgent.stoppingDistance = 0; // Set the stopping distance for patrol
        agent.navMeshAgent.speed = agent.config.patrolSpeed;
        agent.navMeshAgent.SetDestination(agent.patrolPath.patrolPoints[currentPatrolPointIndex].position);
    }

    public void UpdateState(AIAgent agent)
    {
        Vector3 nextDestination = agent.patrolPath.patrolPoints[currentPatrolPointIndex].position;
        float distanceToNextPoint = Vector3.Distance(agent.transform.position, nextDestination);

        if (distanceToNextPoint < agent.config.patrolDistanceThreshold)
        {
            currentPatrolPointIndex++;
            if (currentPatrolPointIndex >= agent.patrolPath.patrolPoints.Length) currentPatrolPointIndex = 0; // Loop back to the first point

            if (agent.config.patrolWaitTime > 0f)
            {
                agent.StartCoroutine(WaitAtPatrolPoint(agent)); // Wait at the patrol point before moving to the next
            }
            else
            {
                agent.navMeshAgent.SetDestination(agent.patrolPath.patrolPoints[currentPatrolPointIndex].position);
            }
        }

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
    
    private IEnumerator WaitAtPatrolPoint(AIAgent agent)
    {
        yield return new WaitForSeconds(agent.config.patrolWaitTime);
        agent.navMeshAgent.SetDestination(agent.patrolPath.patrolPoints[currentPatrolPointIndex].position);

        UpdateState(agent); // Resume patrolling after waiting
    }
}