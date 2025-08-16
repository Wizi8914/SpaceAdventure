using UnityEngine;

public class AIAttackPlayerState : AIState
{
    public AIStateID GetStateID()
    {
        return AIStateID.Attack;
    }

    public void EnterState(AIAgent agent)
    {
        agent.animator.SetBool("Aiming", true);
        agent.navMeshAgent.stoppingDistance = agent.config.minAttackDistance;
        agent.navMeshAgent.speed = agent.config.speed;
    }

    public void ExitState(AIAgent agent)
    {
        agent.animator.SetBool("Aiming", false);
    }

    public void UpdateState(AIAgent agent)
    {
        agent.navMeshAgent.destination = agent.playerTransform.position;

        float distanceToPlayer = Vector3.Distance(agent.transform.position, agent.playerTransform.position);
        if (distanceToPlayer > agent.config.maxAttackDistance)
        {
            agent.stateMachine.ChangeState(AIStateID.ChasePlayer);
            return;
        }

        if (distanceToPlayer <= agent.config.maxAttackDistance)
        {
            AttackPlayer(agent); // If within attack range, attack the player
        }
    }

    private void AttackPlayer(AIAgent agent)
    {
        if (agent.weaponClassManager.weaponManager.ShouldFire())
        {
            if (agent.weaponClassManager.weaponManager.isBurstFire && !agent.weaponClassManager.weaponManager.isBursting)
            {
                agent.StartCoroutine(agent.weaponClassManager.weaponManager.BurstFire());
            }
            else
            {
                agent.weaponClassManager.weaponManager.Fire();
            }
        }

    }
}