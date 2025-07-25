using UnityEngine;

public class BillBoard : MonoBehaviour
{
    Transform playerCamera;

    void Start()
    {
        playerCamera = GameManager.Instance.mainCamera.transform;
    }

    void LateUpdate()
    {
        transform.LookAt(transform.position + playerCamera.forward);
    }
}
