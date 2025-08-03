using System.Threading;
using UnityEngine;
using UnityEngine.AI;

public class AILocomotion : MonoBehaviour
{
    NavMeshAgent agent;
    Animator animator;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.hasPath) {
            animator.SetFloat("Speed", agent.velocity.magnitude);
        } else {
            animator.SetFloat("Speed", 0f);
        }
    }
}
