using UnityEngine;

public class EnemyDeadState : IEnemyState
{
    public void Enter(EnemyStateMachine enemy)
    {
        enemy.agent.isStopped = true;
        enemy.animator.CrossFade("DEAD", 0.1f, 0);
        Object.Destroy(enemy.gameObject, 2f);
    }

    public void Execute(EnemyStateMachine enemy) { }
    public void Exit(EnemyStateMachine enemy) { }
}