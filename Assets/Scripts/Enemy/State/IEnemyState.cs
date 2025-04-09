public interface IEnemyState
{
    void Enter(EnemyStateMachine enemy);
    void Execute(EnemyStateMachine enemy);
    void Exit(EnemyStateMachine enemy);
}
