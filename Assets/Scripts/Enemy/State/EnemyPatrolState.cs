using UnityEngine;

public class EnemyPatrolState : IEnemyState
{
    private Vector3 targetPoint;

    public void Enter(EnemyStateMachine enemy)
    {
        enemy.animator.CrossFade("PATROL", 0.1f, 0);
        enemy.agent.isStopped = false;
        PickRandomPoint(enemy);
        enemy.agent.SetDestination(targetPoint);
    }

    public void Execute(EnemyStateMachine enemy)
    {
        float distToPlayer = Vector3.Distance(enemy.transform.position, enemy.player.position);
        if (enemy.health <= 0)
            enemy.ChangeState(new EnemyDeadState());
        else if (distToPlayer <= enemy.attackRange)
            enemy.ChangeState(new EnemyAttackState());
        else if (distToPlayer <= enemy.chaseRange)
            enemy.ChangeState(new EnemyTraceState());
        else if (Vector3.Distance(enemy.transform.position, targetPoint) < 0.2f)
            Enter(enemy); // 새로운 순찰 지점
    }

    public void Exit(EnemyStateMachine enemy) { }

    private void PickRandomPoint(EnemyStateMachine enemy)
    {
        // 예: 반경 10 내에서 랜덤
        Vector3 rand = Random.insideUnitSphere * 10f;
        rand += enemy.transform.position;
        UnityEngine.AI.NavMeshHit hit;
        if (UnityEngine.AI.NavMesh.SamplePosition(rand, out hit, 10f, UnityEngine.AI.NavMesh.AllAreas))
            targetPoint = hit.position;
        else
            targetPoint = enemy.transform.position;
    }
}