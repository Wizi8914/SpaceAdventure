using UnityEngine;
using UnityEngine.Animations.Rigging;
//using System.Collections.Generic;

public class DetectionStateManager : MonoBehaviour
{
    [SerializeField] float lookDistance = 30f, fov = 120f;
    [SerializeField] Transform enemyEyes;
    Transform playerHead;
    public Transform aimPosition;

    public MultiAimConstraint HeadAim;
    public MultiAimConstraint BodyAim;
    private float HeadAimWeight;
    private float BodyAimWeight;

    [SerializeField] float transitionSpeed = 5f; // Ajout du paramètre

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerHead = GameManager.Instance.playerHead;
        HeadAimWeight = HeadAim.weight;
        BodyAimWeight = BodyAim.weight;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (PlayerSeen())
        {
            // Transition vers le joueur vu
            HeadAim.weight = Mathf.Lerp(HeadAim.weight, HeadAimWeight, Time.fixedDeltaTime * transitionSpeed);
            BodyAim.weight = Mathf.Lerp(BodyAim.weight, BodyAimWeight, Time.fixedDeltaTime * transitionSpeed);
            aimPosition.position = Vector3.Lerp(aimPosition.position, playerHead.position, Time.fixedDeltaTime * transitionSpeed);
        }
        else
        {
            // Transition vers le joueur non vu (regarde devant)
            HeadAim.weight = Mathf.Lerp(HeadAim.weight, 0f, Time.fixedDeltaTime * transitionSpeed);
            BodyAim.weight = Mathf.Lerp(BodyAim.weight, 0f, Time.fixedDeltaTime * transitionSpeed);
            Vector3 forwardPos = enemyEyes.parent.position + enemyEyes.parent.forward * lookDistance;
            aimPosition.position = Vector3.Lerp(aimPosition.position, forwardPos, Time.fixedDeltaTime * transitionSpeed);

            Debug.DrawLine(enemyEyes.position, aimPosition.position, Color.green);
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

            if (hit.transform.CompareTag("Player"))
            {
                Debug.DrawLine(enemyEyes.position, hit.point, Color.blue);
                return true;
            }
        }

        return false;
    }
}
