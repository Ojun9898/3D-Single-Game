using UnityEngine;

public class PlayerMoveState : IPlayerState
{
    public void Enter(PlayerStateMachine player)
    {
        player.animator.CrossFade("WALK", 0.1f, 0);
    }

    public void Execute(PlayerStateMachine player)
    {
        if (player.jumpPressed && player.controller.isGrounded)
        {
            player.jumpPressed = false;
            player.ChangeState(new PlayerJumpState());
            return;
        }

        if (player.attackPressed)
        {
            player.attackPressed = false;
            player.ChangeState(new PlayerAttackState());
            return;
        }

        Vector3 move = new Vector3(player.moveInput.x, 0, player.moveInput.y);

        if (move.magnitude > 0.1f)
        {
            player.controller.Move(move * (player.moveSpeed * Time.deltaTime));
            Quaternion targetRotation = Quaternion.LookRotation(move);
            player.transform.rotation = Quaternion.Slerp(player.transform.rotation, targetRotation, player.rotationSpeed * Time.deltaTime);
        }
        else
        {
            player.ChangeState(new PlayerIdleState());
        }
    }

    public void Exit(PlayerStateMachine player) { }
}