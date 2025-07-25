using UnityEngine;
//using System.Collections.Generic;

public class DetectionStateManager : MonoBehaviour
{
    [SerializeField] float lookDistance = 30f, fov = 120f;
    [SerializeField] Transform enemyEyes;
    Transform playerHead;
    GameManager gameManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        playerHead = gameManager.playerHead;
       
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (PlayerSeen())
        {
            Debug.Log("Player seen!");
        }
        else
        {
            Debug.Log("Player not seen.");
        }
    }

    public bool PlayerSeen()
    {

        if (Vector3.Distance(enemyEyes.position, playerHead.position) > lookDistance) return false;

        Vector3 directionToPlayer = (playerHead.position - enemyEyes.position).normalized;

        float angleToPlayer = Vector3.Angle(enemyEyes.parent.forward, directionToPlayer);

        if (angleToPlayer > (fov / 2f)) return false;

        enemyEyes.LookAt(playerHead.position);

        RaycastHit hit;
        if (Physics.Raycast(enemyEyes.position, enemyEyes.forward, out hit, lookDistance))
        {
            if (hit.transform == null) return false;

            Debug.Log($"Hit: {hit.transform.name}");

            if (hit.transform.CompareTag("Player"))
            {
                Debug.DrawLine(enemyEyes.position, hit.point, Color.blue);
                return true;
            }
        }

        return false;
    }
}
