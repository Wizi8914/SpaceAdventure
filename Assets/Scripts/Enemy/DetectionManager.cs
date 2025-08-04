using UnityEngine;
using UnityEngine.Animations.Rigging;
//using System.Collections.Generic;

public class DetectionManager : MonoBehaviour
{
    [SerializeField] float lookDistance = 30f, fov = 120f;
    [SerializeField] Transform enemyEyes;
    Transform playerHead;

    private AIAgent agent;
    public bool isAIAgent;
    public Transform aimPosition;

    public MultiAimConstraint HeadAim;
    public MultiAimConstraint BodyAim;
    private float HeadAimWeight;
    private float BodyAimWeight;
    
    public LayerMask raycastMask;

    [SerializeField] float transitionSpeed = 5f;

    void Start()
    {
        if (isAIAgent)
        {
            agent = GetComponent<AIAgent>();
        }

        playerHead = GameManager.Instance.playerHead;
        HeadAimWeight = HeadAim.weight;
        BodyAimWeight = BodyAim.weight;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }

    private void Update()
    {
        if (PlayerSeen())
        {
            HeadAim.weight = Mathf.Lerp(HeadAim.weight, HeadAimWeight, Time.fixedDeltaTime * transitionSpeed);
            BodyAim.weight = Mathf.Lerp(BodyAim.weight, BodyAimWeight, Time.fixedDeltaTime * transitionSpeed);

            aimPosition.position = playerHead.position;

            Debug.DrawLine(enemyEyes.position, playerHead.position, Color.green);

        }
        else
        {
            HeadAim.weight = Mathf.Lerp(HeadAim.weight, 0f, Time.fixedDeltaTime * transitionSpeed);
            BodyAim.weight = Mathf.Lerp(BodyAim.weight, 0f, Time.fixedDeltaTime * transitionSpeed);
            Vector3 forwardPos = enemyEyes.parent.position + enemyEyes.parent.forward * lookDistance;
            aimPosition.position = forwardPos;


            Debug.DrawLine(enemyEyes.position, aimPosition.position, Color.red);
        }

        if (isAIAgent) ajustAgentRotation();
    }
    
    private void ajustAgentRotation()
    {
        float distanceToPlayer = Vector3.Distance(enemyEyes.position, playerHead.position);
        if (distanceToPlayer > agent.config.minAttackDistance) return;

        Vector3 directionToPlayer = (playerHead.position - enemyEyes.position).normalized;
        float angleToPlayer = Vector3.Angle(enemyEyes.parent.forward, directionToPlayer);

        if (angleToPlayer > (fov / 4f))
        {
            // Rotate the agent to face the player
            Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
            enemyEyes.parent.rotation = Quaternion.Slerp(enemyEyes.parent.rotation, targetRotation, Time.deltaTime * transitionSpeed);
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
        if (Physics.Raycast(enemyEyes.position, enemyEyes.forward, out hit, lookDistance, raycastMask))
        {
            if (hit.transform == null) return false;

            if (hit.transform.root.gameObject.CompareTag("Player"))
            {
                return true;
            }
        }

        return false;
    }
}
