using UnityEngine;

public class AIAttackPlayerState : AIState
{
    public AIStateID GetStateID()
    {
        return AIStateID.Attack;
    }

    public void EnterState(AIAgent agent)
    {
        agent.animator.SetBool("Aiming", true); // Set the aiming animation
        agent.navMeshAgent.stoppingDistance = agent.config.minAttackDistance; // Set the stopping distance to the attack range
    }

    public void ExitState(AIAgent agent)
    {
        agent.animator.SetBool("Aiming", false); // Reset the aiming animation
    }

    public void UpdateState(AIAgent agent)
    {
        agent.navMeshAgent.destination = agent.playerTransform.position;

        float distanceToPlayer = Vector3.Distance(agent.transform.position, agent.playerTransform.position);
        if (distanceToPlayer > agent.config.maxAttackDistance)
        {
            agent.stateMachine.ChangeState(AIStateID.ChasePlayer); // If too far, switch to chase state
            return;
        }

        if (distanceToPlayer <= agent.config.maxAttackDistance)
        {
            AttackPlayer(agent); // If within attack range, attack the player
        }
    }

    private void AttackPlayer(AIAgent agent)
    {
        Debug.Log(agent.weaponClassManager.weaponManager);
        if (agent.weaponClassManager.weaponManager.ShouldFire())
        {
            agent.weaponClassManager.weaponManager.Fire(); // Fire at the player
        }

    }
}