using UnityEngine;

public class ReloadState : ActionBaseState
{
    public override void EnterState(ActionStateManager actions)
    {
        actions.rHandAnim.weight = 0f;
        actions.lHandIK.weight = 0f;
        actions.animator.SetTrigger("Reload");
    }
    
    public override void UpdateState(ActionStateManager actions)
    {
        
    }
}