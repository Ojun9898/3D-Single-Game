using UnityEngine;

public class EnemyTraceState : IEnemyState
{
    public void Enter(EnemyStateMachine enemy)
    {
        enemy.animator.CrossFade("TRACE", 0.1f, 0);
        enemy.agent.isStopped = false;
    }

    public void Execute(EnemyStateMachine enemy)
    {
        float dist = Vector3.Distance(enemy.transform.position, enemy.player.position);
        if (enemy.health <= 0)
            enemy.ChangeState(new EnemyDeadState());
        else if (dist <= enemy.attackRange)
            enemy.ChangeState(new EnemyAttackState());
        else if (dist > enemy.chaseRange)
            enemy.ChangeState(new EnemyIdleState());
        else
            enemy.agent.SetDestination(enemy.player.position);
    }

    public void Exit(EnemyStateMachine enemy) { }
}