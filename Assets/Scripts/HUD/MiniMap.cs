using UnityEngine;

public class MiniMap : MonoBehaviour
{
    private float defaultNearClipPlane = 0.1f; // Default near clip plane value

    void Start()
    {
        defaultNearClipPlane = GetComponent<Camera>().nearClipPlane;
    }


    void Update()
    {
        Vector3 playerPosition = GameManager.Instance.player.transform.position;
        transform.position = new Vector3(playerPosition.x, transform.position.y, playerPosition.z);

        GetComponent<Camera>().nearClipPlane = defaultNearClipPlane - playerPosition.y; // Adjust the near clip plane to avoid clipping issues
    }
}
