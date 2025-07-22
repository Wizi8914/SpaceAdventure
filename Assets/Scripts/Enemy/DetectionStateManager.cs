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
    public bool PlayerSeen()
    {

        if (Vector3.Distance(enemyEyes.position, playerHead.position) > lookDistance) return false;

        Vector3 directionToPlayer = (playerHead.position - enemyEyes.position).normalized;
            
        return false; //
    }
}
