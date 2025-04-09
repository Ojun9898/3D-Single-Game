using UnityEngine;

public class EnemyIdleState : IEnemyState
{
    private float idleTime;
    private float timer;

    public void Enter(EnemyStateMachine enemy)
    {
        timer = 0;
        idleTime = Random.Range(2f, 5f);
        enemy.animator.CrossFade("IDLE", 0.1f, 0);
        enemy.agent.isStopped = true;
    }

    public void Execute(EnemyStateMachine enemy)
    {
        timer += Time.deltaTime;
        float dist = Vector3.Distance(enemy.transform.position, enemy.player.position);

        if (enemy.health <= 0)
        {
            enemy.ChangeState(new EnemyDeadState());
        }
        else if (dist <= enemy.attackRange)
        {
            enemy.ChangeState(new EnemyAttackState());
        }
        else if (dist <= enemy.chaseRange)
        {
            enemy.ChangeState(new EnemyTraceState());
        }
        else if (timer >= idleTime)
        {
            enemy.ChangeState(new EnemyPatrolState());
        }
    }

    public void Exit(EnemyStateMachine enemy) { }
}