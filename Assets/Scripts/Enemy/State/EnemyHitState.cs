public class EnemyHitState : IEnemyState
{
    public void Enter(EnemyStateMachine enemy)
    {
        enemy.animator.CrossFade("HIT", 0.1f, 0);
    }

    public void Execute(EnemyStateMachine enemy)
    {
        // 히트 애니메이션 끝나면 Idle로
        if (enemy.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
            enemy.ChangeState(new EnemyIdleState());
    }

    public void Exit(EnemyStateMachine enemy) { }
}