using UnityEngine;

public class PlayerFallingState : PlayerBaseState
{
    private readonly int FallHash = Animator.StringToHash("Fall");

    private Vector3 momentum;

    public PlayerFallingState(Player_SM stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        momentum = stateMachine.Controller.velocity;
        momentum.y = 0f;

        stateMachine.Animator.CrossFadeInFixedTime(FallHash, 0.1f);

    }

    public override void Exit()
    {
        return;
    }

    public override void Tick(float deltaTime)
    {
       Move(momentum, deltaTime);


        if (stateMachine.GroundedChecker.isGrounded)
        {
            stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
        }


    }
}