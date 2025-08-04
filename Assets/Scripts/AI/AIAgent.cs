using UnityEngine;
using UnityEngine.AI;

public class AIAgent : MonoBehaviour
{
    public AIAgentConfig config;
    public AIStateMachine stateMachine;
    public AIStateID initialState;
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
        weaponClassManager.UpdateWeapon();

        ragdollManager = GetComponent<RagdollManager>();
        animator = GetComponent<Animator>();

        detectionStateManager = GetComponent<DetectionManager>();

        playerTransform = GameManager.Instance.player.transform;

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
        // Add other states as needed
    }
}
