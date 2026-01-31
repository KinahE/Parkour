using UnityEngine;
public class PlayerFreeLookState : PlayerBaseState
{
    private readonly int movementSpeedHash = Animator.StringToHash("movementSpeed");
    private readonly int LocomotionHash = Animator.StringToHash("Locomotion");
    public PlayerFreeLookState(Player_SM stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(LocomotionHash, 0.1f);
        stateMachine.InputReader.JumpEvent += OnJump;
    }

    public override void Tick(float deltaTime)
    {
        if (!stateMachine.HasControl)
        {
            return;
        }
        HandleMovement(deltaTime);
    }

    public override void Exit()
    {
        stateMachine.InputReader.JumpEvent -= OnJump;
    }

    void HandleMovement(float deltaTime)
    {
        if (stateMachine.InputReader.IsMovementInput)
        {
            Vector3 direction = CalculateMovementDirection();
            float speed = stateMachine.InputReader.IsSprinting ? stateMachine.SprintSpeed : stateMachine.MoveSpeed;
            Move(direction * speed, deltaTime);
            FaceMovementDirection(direction);
            UpdateAnimations(direction);
        }
        else
        {
            Move(deltaTime);
            UpdateAnimations(Vector3.zero);
        }
    }

    public Vector3 CalculateMovementDirection()
    {
        Vector3 forward = stateMachine.MainCameraTransform.forward;
        Vector3 right = stateMachine.MainCameraTransform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        return forward * stateMachine.InputReader.MovementInput.y +
            right * stateMachine.InputReader.MovementInput.x;
    }

    private void FaceMovementDirection(Vector3 movement)
    {
        if (stateMachine.InputReader.MovementInput == Vector2.zero || stateMachine.InputReader.MovementInput.magnitude < 0.1f)
        {
            return;
        }
        stateMachine.transform.rotation = Quaternion.Lerp(
            stateMachine.transform.rotation,
            Quaternion.LookRotation(movement),
            Time.deltaTime * stateMachine.RotationSpeed
        );
    }

    void UpdateAnimations(Vector3 direction)
    {
        if (direction == Vector3.zero)
        {
            stateMachine.PlayerAnimationManager.UpdateAnimationMovementParameters(0, 0f);
        }
        else if (stateMachine.InputReader.IsSprinting)
        {
            stateMachine.PlayerAnimationManager.UpdateAnimationMovementParameters(0, 1f);
        }
        else
        {
            stateMachine.PlayerAnimationManager.UpdateAnimationMovementParameters(0, 0.5f);
        }
    }


    void OnJump()
    {

        if (stateMachine.GroundedChecker.isGrounded && !stateMachine.InAction)
        {
            var hitData = stateMachine.EnvironmentChecker.ObstacleCheck();
            if (hitData.forwardHitFound)
            {
                foreach (var action in stateMachine.ParkourActions)
                {
                    if (action.CheckIfPossible(hitData, stateMachine.transform))
                    {
                        stateMachine.StartCoroutine(DoParkourAction(action));
                        break;
                    }
                }
            }
            else
            {
                stateMachine.SwitchState(new PlayerJumpingState(stateMachine));
            }
        }
    }
}