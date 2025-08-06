using UnityEngine;
using UnityEngine.AI;

public class AIAgent : MonoBehaviour
{
    public AIAgentConfig config;
    public AIStateMachine stateMachine;
    public AIStateID initialState;

    public bool isPatrolling = false;
    public PatrolPath patrolPath;
    [HideInInspector] public Transform playerTransform;
    [HideInInspector] public NavMeshAgent navMeshAgent;
    [HideInInspector] public RagdollManager ragdollManager;
    [HideInInspector] public Animator animator;
    [HideInInspector] public EnemyWeaponClassManager weaponClassManager;
    [HideInInspector] public DetectionManager detectionStateManager;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        stateMachine = new AIStateMachine(this);

        // Init weapon class manager
        weaponClassManager = GetComponent<EnemyWeaponClassManager>();
        weaponClassManager.weaponPrefab = config.weaponPrefab;
        weaponClassManager.inaccuracy = config.inaccuracy; // Set inaccuracy from config
        weaponClassManager.UpdateWeapon();

        ragdollManager = GetComponent<RagdollManager>();
        animator = GetComponent<Animator>();

        detectionStateManager = GetComponent<DetectionManager>();

        playerTransform = GameManager.Instance.player.transform;

        navMeshAgent.speed = config.speed; // Set the speed from the config

        // Register all states
        RegisterAllStates();

        stateMachine.ChangeState(initialState);
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.Update();
    }

    private void RegisterAllStates()
    {
        stateMachine.RegisterState(new AIDeathState());
        stateMachine.RegisterState(new AIChasePlayerState());
        stateMachine.RegisterState(new AIIdleState());
        stateMachine.RegisterState(new AIAttackPlayerState());
        stateMachine.RegisterState(new AIPatrolState());
        // Add other states as needed
    }
}
