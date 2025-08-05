using Unity.Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public CinemachineCamera killCam;
    public CinemachineTargetGroup targetGroup;

    private void Start()
    {
        killCam = GetComponentInChildren<CinemachineCamera>();
        targetGroup = GetComponentInChildren<CinemachineTargetGroup>();
        targetGroup.AddMember(GameManager.Instance.player.transform.Find("PlayerModel").transform, 1, 2);
    }

    public void EnableKillCam(Transform target)
    {
        if (killCam == null) return;

        targetGroup.AddMember(target, 0.25f, 0);
        killCam.Priority = 10;

        // Slow down the game time for the kill cam effect
        Time.timeScale = 0.5f;
    }
}
