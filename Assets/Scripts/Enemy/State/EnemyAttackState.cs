using UnityEngine;

public class EnemyAttackState : IEnemyState
{
    private float attackTimer;
    private float attackCooldown = 1.5f;

    public void Enter(EnemyStateMachine enemy)
    {
        attackTimer = 0f;
        enemy.agent.isStopped = true;
        enemy.animator.CrossFade("ATTACK", 0.1f, 0);
    }

    public void Execute(EnemyStateMachine enemy)
    {
        attackTimer += Time.deltaTime;
        if (attackTimer >= attackCooldown)
        {
            // 공격 데미지 처리
            // if (Vector3.Distance(...) < ...) ...
            enemy.ChangeState(new EnemyIdleState());
        }
    }

    public void Exit(EnemyStateMachine enemy) { }
}