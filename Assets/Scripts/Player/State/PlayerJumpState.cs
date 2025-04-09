using UnityEngine;

public class PlayerJumpState : IPlayerState
{
    public void Enter(PlayerStateMachine player)
    {
        player.animator.CrossFade("JUMP", 0.1f, 1);
        player.velocity.y = 5f;
    }

    public void Execute(PlayerStateMachine player)
    {
        Vector3 move = new Vector3(player.moveInput.x, 0, player.moveInput.y);
        if (move.magnitude > 0.1f)
        {
            player.controller.Move(move * (player.moveSpeed * Time.deltaTime));
            player.RotateTowardsCameraDirection();
        }

        if (player.controller.isGrounded)
        {
            if (player.moveInput.magnitude > 0.1f)
                player.ChangeState(new PlayerMoveState());
            else if (player.runPressed)
                player.ChangeState(new PlayerRunState());
            else
                player.ChangeState(new PlayerIdleState());
        }
    }

    public void Exit(PlayerStateMachine player) { }
}