using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{
    public IEnemyState CurrentState { get; private set; }
    public Transform player;        // 추적할 대상
    public float chaseRange = 5f;
    public float attackRange = 1.5f;
    public int health = 100;
    
    [HideInInspector] public Animator animator;
    [HideInInspector] public UnityEngine.AI.NavMeshAgent agent;

    void Start()
    {
        player = GameManager.Instance.playerTransform;
        animator = GetComponent<Animator>();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        ChangeState(new EnemyIdleState());
    }

    void Update()
    {
        CurrentState.Execute(this);
    }

    public void ChangeState(IEnemyState newState)
    {
        if (CurrentState != null)
            CurrentState.Exit(this);
        CurrentState = newState;
        CurrentState.Enter(this);
    }

    // 데미지 입었을 때 호출
    public void OnHit(int damage)
    {
        health -= damage;
        ChangeState(new EnemyHitState());
    }
}