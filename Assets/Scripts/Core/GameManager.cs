using UnityEngine;
using Unity.Cinemachine;

public class GameManager : MonoBehaviour
{
    public Transform playerHead;

    void Awake()
    {
        Camera.main.gameObject.AddComponent<CinemachineBrain>();
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
