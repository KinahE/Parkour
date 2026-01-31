using UnityEngine;

public class PlayerJumpingState : PlayerBaseState
{
    private Vector3 momentum;

    private readonly int JumpHash = Animator.StringToHash("Jump");
    public PlayerJumpingState(Player_SM stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {

        stateMachine.ForceReceiver.Jump(stateMachine.JumpForce);
        momentum = stateMachine.Controller.velocity;
        momentum.y = 0f;

        stateMachine.Animator.CrossFadeInFixedTime(JumpHash, 0.1f);

    }

    public override void Exit()
    {
        return;
    }

    public override void Tick(float deltaTime)
    {
        Move(momentum, deltaTime);

        if (stateMachine.Controller.velocity.y <= 0)
        {
            stateMachine.SwitchState(new PlayerFallingState(stateMachine));
            return;
        }

    }
}