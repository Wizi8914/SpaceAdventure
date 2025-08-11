using UnityEngine;
using Unity.Cinemachine;
using UnityEngine.Rendering;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public GameObject player;
    public Transform playerHead;
    public Transform mainCamera;
    public Canvas canvas;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        mainCamera.gameObject.AddComponent<CinemachineBrain>();
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public IEnumerator RestartGame(float delay)
    {
        yield return new WaitForSeconds(delay);
        Time.timeScale = 1f; // Reset time scale after restart
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    public void EnableUIMode()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        
        if (player != null)
        {
            var aimManager = player.GetComponent<AimStateManager>();
            if (aimManager != null) aimManager.enabled = false;
        }
    }

    public void EnableGameplayMode()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        if (player != null)
        {
            var aimManager = player.GetComponent<AimStateManager>();
            if (aimManager != null) aimManager.enabled = true;
        }
    }
}
