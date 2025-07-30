using System.Threading;
using UnityEngine;
using UnityEngine.AI;

public class AILocomotion : MonoBehaviour
{
    Transform playerTransform;
    public float maxTime = 1f;
    public float maxDistance = 1f;

    NavMeshAgent agent;
    Animator animator;
    float timer;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        playerTransform = GameManager.Instance.player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0f)
        {
            float sqdistance = (playerTransform.position - agent.destination).sqrMagnitude;
            if (sqdistance > maxDistance * maxDistance) agent.destination = playerTransform.position;
            timer = maxTime;

        }
        agent.destination = playerTransform.position;
        animator.SetFloat("Speed", agent.velocity.magnitude);
    }
}
