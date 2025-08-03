using UnityEngine;
using UnityEngine.AI;

public class AIDeathState : AIState
{
    public AIStateID GetStateID()
    {
        return AIStateID.Death;
    }

    public void EnterState(AIAgent agent)
    {
        agent.navMeshAgent.isStopped = true;
        agent.ragdollManager.EnableRagdoll();
        agent.animator.enabled = false; // Disable the animator to stop animations
        agent.weaponClassManager.DropWeapon(agent.config.TimeToDie); // Disable weapons if applicable

    }

    public void ExitState(AIAgent agent)
    {
    }


    public void UpdateState(AIAgent agent)
    {
    }
}
