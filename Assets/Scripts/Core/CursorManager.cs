using UnityEngine;


public class CursorManager : MonoBehaviour
{
    public static CursorManager Instance { get; private set; }
    
    [Header("Cursor States")]
    public bool isUIMode = false;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetUIMode()
    {
        isUIMode = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void SetGameplayMode()
    {
        isUIMode = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public bool CanChangeCursorState()
    {
        return !isUIMode;
    }

    public void RefreshCursorState()
    {
        if (isUIMode)
        {
            SetUIMode();
        }
        else
        {
            SetGameplayMode();
        }
    }
}
