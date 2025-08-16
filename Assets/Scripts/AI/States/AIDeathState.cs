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
        agent.ragdollManager.EnableRagdoll();
        agent.animator.enabled = false;
        agent.weaponClassManager.DropWeapon(agent.config.TimeToDie);

    }

    public void ExitState(AIAgent agent)
    {
    }


    public void UpdateState(AIAgent agent)
    {
    }
}
